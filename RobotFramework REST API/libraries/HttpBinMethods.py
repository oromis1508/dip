from pip._vendor import requests

from utils.test_utils import ConfigurationReader


class HttpBinMethods:

    def authorization_by_method_base_auth(self, user, password):
        """Call base auth method on the web service

        :param user: username for authorization
        :param password: password for authorization
        :returns: the response of the web service method
        """
        return requests.get('{service_url}basic-auth/user/passwd'
                            .format(service_url=ConfigurationReader.get_setting(key='service_uri')),
                            auth=(user, password))

    def use_method_get_with_args(self, params):
        """Call method get on the web service with parameter

        :param params: parameters of the get method of the web service
        :returns: the response of the web service method
        """
        return requests.get('{service_url}get'
                            .format(service_url=ConfigurationReader.get_setting(key='service_uri')), params=params)

    def use_method_stream_to_create_streams(self, stream_size):
        """Call method stream on the web service

        :param stream_size: number of streams
        :returns: the response of the web service method
        """
        return requests.get('{service_url}stream/{stream_size}'
                            .format(service_url=ConfigurationReader.get_setting(key='service_uri'),
                                    stream_size=stream_size))
