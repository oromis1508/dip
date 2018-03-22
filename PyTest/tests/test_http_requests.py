import mock
import pytest

from framework.interface_drivers.HttpUtil import HttpUtil
from framework.interface_drivers.Logger import Logger
from framework.interface_drivers.MockRequestsUtil import MockRequestsUtil

@pytest.fixture(scope="function",
                params=[('http://some_test_url.com', {'header':'header_value'}, {'param':'param_value'}, 200),
                        ('http://some_test_url.com', None, {'param': 'param_value'}, 200)])
def param_mock_get(request):
    return request.param

@pytest.fixture(scope="function",
                params=[('http://some_test_url.com', {'header':'header_value'}, {'param':'param_value'}, 200, 'body')])
def param_mock_post(request):
    return request.param

@mock.patch('framework.interface_drivers.HttpUtil.HttpUtil.get_request', side_effect=MockRequestsUtil.mocked_requests)
def test_mock_get(mock_get, param_mock_get):
    url, headers, params, status_code = param_mock_get
    response = HttpUtil.get_request(url=url, headers=headers, params=params, status_code=status_code)
    try:
        assert response.url == url and response.headers == headers and response.params == params and response.status_code == status_code
        Logger.add_log(message='mock get request success with params: {}'.format(str(param_mock_get)))
    except AssertionError:
        Logger.add_log(message='mock get request failed with params: {}'.format(str(param_mock_get)))
        raise

@mock.patch('framework.interface_drivers.HttpUtil.HttpUtil.post_request', side_effect=MockRequestsUtil.mocked_requests)
def test_mock_post(mock_post, param_mock_post):
    url, headers, params, status_code, body = param_mock_post
    response = HttpUtil.post_request(url=url, headers=headers, params=params, status_code=status_code, body=body)
    try:
        assert response.url == url and response.headers == headers and response.params == params and response.status_code == status_code
        Logger.add_log(message='mock post request success with params: {}'.format(str(param_mock_post)))
    except AssertionError:
        Logger.add_log(message='mock post request failed with params: {}'.format(str(param_mock_post)))
        raise