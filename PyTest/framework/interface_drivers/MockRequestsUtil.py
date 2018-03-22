class MockRequestsUtil:

    @staticmethod
    def mocked_requests(*args, **kwargs):
        class MockResponse:
            def __init__(self, url, headers, params, status_code, body):
                self.url = url
                self.status_code = status_code
                self.headers = headers
                self.params = params
                self.body = body

        headers = None
        params = None
        status_code = 200
        body = None
        if 'headers' in kwargs:
            headers = kwargs['headers']
        if 'params' in kwargs:
            params = kwargs['params']
        if 'status_code' in kwargs:
            status_code = kwargs['status_code']
        if 'body' in kwargs:
            body = kwargs['body']

        return MockResponse(kwargs['url'], headers, params, status_code, body)