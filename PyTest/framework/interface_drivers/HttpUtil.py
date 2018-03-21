from pip._vendor import requests

class HttpUtil :

    @staticmethod
    def get_request(url, headers = None, params = None):
        response = requests.get(url, params=params, headers=headers)
        return response

    @staticmethod
    def post_request(url, body, headers = None, params = None):
        response = requests.post(url, data=body, params=params, headers=headers)
        return response

