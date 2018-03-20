from requests_mock.adapter import Adapter


class MockRequestsUtil:

    @staticmethod
    def mock_response(request_type, url, headers):
        Adapter.register_uri(request_type, url, headers=headers)
