from app import app
from flask import jsonify
import getpass
import socket
from datetime import datetime

# magic for getting IP of specific interface
import fcntl
import struct

# global variables
LOG_FILE = 'err.log'
LOG_FORMAT = '{} {} "{}"\n'

def log_warn(msg):
	log_out(msg, 'WARN')

def log_fatal(msg):
	log_out(msg, 'FATAL')

def log_out(msg, level):
	timestamp = datetime.now().strftime("%d/%m/%Y %H:%M:%S")

	f = open(LOG_FILE, 'w')

	f.write(LOG_FORMAT.format(timestamp, level, msg))

def get_ip_address(ifname):
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    return socket.inet_ntoa(fcntl.ioctl(
        s.fileno(),
        0x8915,  # SIOCGIFADDR
        struct.pack('256s', ifname[:15].encode('utf-8'))
    )[20:24])

def get_node_info():
	try:
		username = getpass.getuser()
		hostname = socket.gethostname()
		ip_addr  = get_ip_address('enp0s3')

		return '{}@{}'.format(username, hostname), ip_addr
	except Exception as ex:
		log_fatal(str(ex))

		return 'error', 'error'

@app.route('/node_info')
def node_info():
	info = get_node_info()
	return jsonify(device_name=info[0],
		               device_addr=info[1])
