import requests
import requests_mock

class MockRequestsUtil:

    @requests_mock.Mocker()
    def register_mock_uri(self, request_type, url, headers = None, params = None, body = None):
        Mocker().register_uri
