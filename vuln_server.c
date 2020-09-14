#include <stdio.h>
#include <fcntl.h>
#include <sys/stat.h>
#include <arpa/inet.h>
#include <sys/socket.h>
#include <unistd.h>
#include <stdlib.h>
#include <netinet/in.h>
#include <string.h>

#define PORT 80
#define MAX_REQUEST_SIZE 5000
#define MAX_RESOURCE_SIZE 5000
#define WEBROOT "./webroot"
#define HEADER "HTTP/1.0 %s\r\nServer: Robbe webserver\r\n\r\n"

void process_request(int, struct sockaddr_in *);
int recv_request(int, char *);
int send_string(int, char *);
int get_file_size(int);
void handle_response(int, int, int);
void get_request(char *, int, struct sockaddr_in *);
void head_request(char *, int, struct sockaddr_in *);

int main(int argc, char const *argv[])
{
    int listen_sock, accept_sock, yes=1;
    struct sockaddr_in server_addr, client_addr;
    socklen_t addrlen = sizeof(server_addr);

    if ((listen_sock = socket(AF_INET, SOCK_STREAM, 0)) == -1)
    {
        perror("Error while creating socket");
        exit(EXIT_FAILURE);
    }

    if (setsockopt(listen_sock, SOL_SOCKET, SO_REUSEADDR, &yes, sizeof(int)) == -1)
    {
        perror("Error while setting socket options");
        exit(EXIT_FAILURE);
    }

    server_addr.sin_family = AF_INET;
    server_addr.sin_addr.s_addr = INADDR_ANY;
    server_addr.sin_port = htons(PORT);
    memset(server_addr.sin_zero, '\0', 8);

    if (bind(listen_sock, (struct sockaddr *)&server_addr, sizeof(struct sockaddr)) == -1)
    {
        perror("Error while binding socket");
        exit(EXIT_FAILURE);
    }

    if (listen(listen_sock, 10) == -1)
    {
        perror("Error while setting up listen");
        exit(EXIT_FAILURE);
    }

    printf("--------Robbe's webserver is running--------\n");

    while (1)
    {
        accept_sock = accept(listen_sock, (struct sockaddr *)&client_addr, &addrlen);
        if (accept_sock == -1) {
            perror("Error while accepting connection");
            exit(EXIT_FAILURE);
        }

        process_request(accept_sock, &client_addr);
    }
}

void process_request(int accept_sock, struct sockaddr_in *client_addr_ptr) {
    char *ptr, request[MAX_REQUEST_SIZE];
    int response_file, length;

    length = recv_request(accept_sock, request);
    printf("Got request from %s:%d \"%s\"\n", inet_ntoa(client_addr_ptr->sin_addr), ntohs(client_addr_ptr->sin_port), request);

    if (length == -1) {
        printf("The request was too long! Terminating connection...\n");
        shutdown(accept_sock, SHUT_RDWR);
        return;
    }

    ptr = strstr(request, " HTTP/");
    if (ptr == NULL) {
        // TODO: maybe return a real invalid request response?
        printf(" NOT HTTP!");
    } else {
        // When the request is read, it stops after the resource because of the NULL character
        *ptr = 0; 
        ptr = NULL;

        if (strncmp(request, "GET ", 4) == 0) {
            ptr = request+4;
            get_request(ptr, accept_sock, client_addr_ptr);
        }
        if (strncmp(request, "HEAD ", 5) == 0) {
            ptr = request+5;
            head_request(ptr, accept_sock, client_addr_ptr);
        }

        if (ptr == NULL) {
            printf("\tUNKNOWN REQUEST!\n");
            // TODO: maybe return a real invalid request response?
        }
    }
    shutdown(accept_sock, SHUT_RDWR);
}

int recv_request(int accept_sock, char *request) {
#define EOL "\r\n\r\n"
#define EOL_SIZE 4
    char *ptr;
    int eol_matched = 0;

    ptr = request;
    while (recv(accept_sock, ptr, 1, 0) == 1) {
        if (*ptr == EOL[eol_matched]) {
            eol_matched++;
            if (eol_matched == EOL_SIZE) {
                *(ptr+1-EOL_SIZE) = '\0';
                return strlen(request);
            }
        } else {
            eol_matched = 0;
        }
        ptr++;
    }
}

int send_string(int accept_sock, char *buffer) {
    int sent_bytes, bytes_to_send;
    bytes_to_send = strlen(buffer);
    while (bytes_to_send > 0) {
        sent_bytes = send(accept_sock, buffer, bytes_to_send, 0);
        if (sent_bytes == -1)
            return 0; // Return 0 on send error.
        bytes_to_send -= sent_bytes;
        buffer += sent_bytes;
    }
    return 1;
}

void get_request(char *ptr, int accept_sock, struct sockaddr_in *client_addr_ptr) {
    char resource[MAX_RESOURCE_SIZE];
    int response_file, length;

    if (ptr[strlen(ptr) - 1] == '/')
        strcat(ptr, "index.html");
    strcpy(resource, WEBROOT);
    strcat(resource, ptr);
    response_file = open(resource, O_RDONLY, 0);
    printf("\tOpening \'%s\'\t", resource);
    if (response_file == -1) {
        strcpy(resource, WEBROOT);
        strcat(resource, "/NotFound404.html");
        response_file = open(resource, O_RDONLY, 0);
        handle_response(404, response_file, accept_sock);
    } else {
        handle_response(200, response_file, accept_sock);
    }
}

void head_request(char *ptr, int accept_sock, struct sockaddr_in *client_addr_ptr) {
    char resource[MAX_RESOURCE_SIZE];
    int response_file, length;

    if (ptr[strlen(ptr) - 1] == '/')
        strcat(ptr, "index.html");
    strcpy(resource, WEBROOT);
    strcat(resource, ptr);
    response_file = open(resource, O_RDONLY, 0);
    printf("\tOpening \'%s\'\t", resource);
    if (response_file == -1) {
        strcpy(resource, WEBROOT);
        strcat(resource, "/NotFound404.html");
        response_file = open(resource, O_RDONLY, 0);
        close(response_file);
        handle_response(404, -1, accept_sock);
    } else {
        close(response_file);
        handle_response(200, -1, accept_sock);
    }
}

int get_file_size(int fd) {
    struct stat stat_struct;

    if (fstat(fd, &stat_struct) == -1)
        return -1;
    return (int) stat_struct.st_size;
}

void handle_response(int code, int body_file, int accept_sock) {
    char header[100];
    char *body;
    int length;

    switch(code)
    {
        case 404:
            printf(" 404 Not found\n");
            sprintf(header, HEADER, "404 NOT FOUND");
            break;

        case 200:
            printf(" 200 OK\n");
            sprintf(header, HEADER, "200 OK");
            break;

        default:
            // TODO: implement default
            printf("Error!\n");
    }

    // Send header
    send_string(accept_sock, header);

    // Don't send body (for example with HEAD requests)
    if (body_file == -1)
        return;

    // Send body
    if ( (length = get_file_size(body_file)) == -1) {
        perror("Error opening response file");
        exit(EXIT_FAILURE);
    }
    if ( (body = (char *) malloc(length)) == NULL) {
        perror("Error allocating memory for reading resource");
        exit(EXIT_FAILURE);
    }
    read(body_file, body, length);
    send(accept_sock, body, length, 0);
    free(body);
    close(body_file);
}

