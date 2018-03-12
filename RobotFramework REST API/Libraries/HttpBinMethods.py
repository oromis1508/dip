from pip._vendor import requests

class HttpBinMethods:

    response = None
    service_uri = 'http://httpbin.org/'

    """Call 
    """
    def authorization_by_method_base_auth(self, user, password):
        self.response = requests.get('{}basic-auth/user/passwd'.format(self.service_uri), auth=(user, password))
        if  self.get_status_code() == 401:
            return 'unauthorized'
        else:
            return self.response.json()['user']

    def use_method_get_with_args(self, header_name, header_value):
        self.response = requests.get('{}get?{}={}'.format(self.service_uri, header_name, header_value))
        return self.response.json()['args'][header_name]

    def use_method_stream_to_create_streams(self, stream_size):
        self.response = requests.get('{}stream/{}'.format(self.service_uri, stream_size))
        return self.response.text.count('\n')

    def get_status_code(self):
        return self.response.status_code