class MockRequestsUtil:

    class MockResponse:
        def __init__(self, url, status_code, headers=None, params=None, body=None):
            self.url = url
            self.headers = headers
            self.params = params
            self.body = body
            self.status_code = status_code

    @staticmethod
    def mocked_requests(url, status_code, headers, params, body):
        return MockRequestsUtil.MockResponse(url=url, headers=headers, params=params, body=body, status_code=status_code)

