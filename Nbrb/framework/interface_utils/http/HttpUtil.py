from pip._vendor import requests


def get_request(url, params=None):
    """ Wrapper for requests library. Sends get request

    :param(str) url: url for request
    :param(dict) params: parameters of request
    :return: the response object
    """
    return requests.get(url=url, params=params)
