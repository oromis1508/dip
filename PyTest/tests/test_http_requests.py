import mock
import pytest

from framework.interface_drivers.http import StatusCodes
from framework.interface_drivers.http.HttpUtil import HttpUtil
from framework.interface_drivers.logger.Logger import Logger
from framework.interface_drivers.http.MockRequestsUtil import MockRequestsUtil


@pytest.fixture(scope="function",
                params=[('http://some_test_url.com', {'header': 'header_value'}, {'param': 'param_value'}, StatusCodes.ok_status_code),
                        ('http://some_test_url.com', None, {'param': 'param_value'}, StatusCodes.ok_status_code)])
def param_mock_get(request):
    return request.param


@pytest.fixture(scope="function",
                params=[
                    ('http://some_test_url.com', {'header': 'header_value'}, {'param': 'param_value'}, StatusCodes.ok_status_code, 'body')])
def param_mock_post(request):
    return request.param


@mock.patch('framework.interface_drivers.http.HttpUtil.HttpUtil.get_request', side_effect=MockRequestsUtil.mocked_requests)
def test_mock_get(mock_get, param_mock_get):
    """ Test the request method get by the mock library
    :param mock_get: needed for the mock method
    :param param_mock_get: parameters for test
    """
    url, headers, params, status_code = param_mock_get
    response = HttpUtil.get_request(url=url,
                                    headers=headers,
                                    params=params,
                                    status_code=status_code)
    try:
        assert response.url == url and \
               response.headers == headers and \
               response.params == params and \
               response.status_code == status_code
        Logger.add_log(
            message='mock get request success with params: {param_mock_get}'.format(param_mock_get=str(param_mock_get)))
    except AssertionError:
        Logger.add_log(
            message='mock get request failed with params: {param_mock_get}'.format(param_mock_get=str(param_mock_get)))
        raise


@mock.patch('framework.interface_drivers.http.HttpUtil.HttpUtil.post_request', side_effect=MockRequestsUtil.mocked_requests)
def test_mock_post(mock_post, param_mock_post):
    """ Test the request method post by the mock library
    :param mock_get: needed for the mock method
    :param param_mock_get: parameters for test
    """
    url, headers, params, status_code, body = param_mock_post
    response = HttpUtil.post_request(url=url,
                                     headers=headers,
                                     params=params,
                                     status_code=status_code,
                                     body=body)
    try:
        assert response.url == url and \
               response.headers == headers and \
               response.params == params and \
               response.status_code == status_code
        Logger.add_log(message='mock post request success with params: {param_mock_post}'.format(
            param_mock_post=str(param_mock_post)))
    except AssertionError:
        Logger.add_log(message='mock post request failed with params: {param_mock_post}'.format(
            param_mock_post=str(param_mock_post)))
        raise
