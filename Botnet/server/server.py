import grpc
from concurrent import futures
import time
import threading

import c2_pb2
import c2_pb2_grpc

import servermanager

class C2Servicer(c2_pb2_grpc.C2Servicer):
    def __init__(self, server_manager):
        self.server_manager = server_manager 

    def StartJob(self, request, context):
        return self.server_manager.start_job(request)


server_manager = servermanager.ServerManager()

if __name__ == "__main__":
    job_server = grpc.server(futures.ThreadPoolExecutor(max_workes=10))
    c2_pb2_grpc.add_C2Servicer_to_server(C2Servicer(server_manager), job_server)
    job_server.add_insecure_port('[::]:4590')

    bot_server = threading.Thread(target=server_manager.listen_for_bots, args=(('', 4591)))

    job_server.start()
    bot_server.start()
