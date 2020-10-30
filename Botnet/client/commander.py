import pika
import sys
import json
import threading
import time
import os

def sender():
    connection = pika.BlockingConnection(
        pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()

    botnet_id = "5"
    exchange_name = "C2Commands" + botnet_id
    
    channel.exchange_declare(exchange=exchange_name, exchange_type='fanout')

    message = json.dumps({'jobAction': 'start', 'commandId': 1, 'commandArgs': ['A', 'B']})
    channel.basic_publish(
        exchange=exchange_name, routing_key='', body=message)
    print(" [x] Sent %r" % message)
    connection.close()

def receiver():
    connection = pika.BlockingConnection(pika.ConnectionParameters(host='localhost'))
    channel = connection.channel()

    channel.queue_declare(queue='command_results')

    def callback(ch, method, properties, body):
        print(channel.consumer_tags)
        print(" [x] Received %r" % body)

    channel.basic_consume(queue='command_results', on_message_callback=callback, auto_ack=True)

    print(' [*] Waiting for messages. To exit press CTRL+C')
    channel.start_consuming()

if __name__ == '__main__':
    try:
        sender_thread = threading.Thread(target=sender)
        receiver_thread = threading.Thread(target=receiver)
        sender_thread.start()
        receiver_thread.start()
        time.sleep(5000)
    except KeyboardInterrupt:
        print('Interrupted')
        try:
            sys.exit(0)
        except SystemExit:
            os._exit(0)