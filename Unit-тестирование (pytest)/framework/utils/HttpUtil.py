from pip._vendor import requests

class HttpUtil :

    @staticmethod
    def send_request(request_type, url, headers = None, params = None, body = None):
        if request_type.lower() == 'post':
            response = requests.post(url, data=body, params=params, headers=headers)
        else:
            response = requests.get(url, params=params, headers=headers)
        return response
