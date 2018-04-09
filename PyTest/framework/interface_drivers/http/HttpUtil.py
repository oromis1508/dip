from pip._vendor import requests

from framework.interface_drivers.http import StatusCodes
from framework.interface_drivers.logger.Logger import Logger


class HttpUtil:

    @staticmethod
    def get_request(url, headers=None, params=None, status_code=StatusCodes.ok_status_code):
        response = requests.get(url=url,
                                params=params,
                                headers=headers,
                                status_code=status_code)
        Logger.add_log(message='sent get request to url {}'.format(url))
        return response

    @staticmethod
    def post_request(url, body, headers=None, params=None, status_code=StatusCodes.ok_status_code):
        response = requests.post(url=url,
                                 data=body,
                                 params=params,
                                 headers=headers,
                                 status_code=status_code)
        Logger.add_log(message='sent post request to url {}'.format(url))
        return response
