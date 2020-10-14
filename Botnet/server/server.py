import grpc
from concurrent import futures
import time

import c2_pb2
import c2_pb2_grpc

import servermanager

class C2Servicer(c2_pb2_grpc.C2Servicer):
    def __init__(self):
        self.server_manager = servermanager.ServerManager()

    def StartJob(self, request, context):
        response.response = self.server_manager.start_job(request)
        return response

if __name__ == "__main__":
    server = grpc.server(futures.ThreadPoolExecutor(max_workes=10))
    c2_pb2_grpc.add_C2Servicer_to_server(C2Servicer(), server)
    server.add_insecure_port('[::]:4590')
    server.start()
