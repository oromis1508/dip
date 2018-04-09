from framework.interface_drivers.http import StatusCodes


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

        headers = kwargs.get('headers', None)
        params = kwargs.get('params', None)
        status_code = kwargs.get('status_code', StatusCodes.ok_status_code)
        body = kwargs.get('body', None)

        return MockResponse(url=kwargs['url'],
                            headers=headers,
                            params=params,
                            status_code=status_code,
                            body=body)
