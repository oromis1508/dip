from pip._vendor import requests


def get_request(url, params=None):
    return requests.get(url=url, params=params)
