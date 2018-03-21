from unittest import mock
import pytest
from framework.interface_drivers.HttpUtil import HttpUtil
from framework.interface_drivers.MockRequestsUtil import MockRequestsUtil


@pytest.fixture(scope="function",
                params=[('http://url', {'header1' : 'value1'}, {'param1' : 'value1'}, 'body1', 200),
                        ('http://url2', {'header2' : 'value2'}, {'param2' : 'value2'}, 'body2', 200)
                        ])
def param_test(request):
    return request.param

@mock.patch('requests.get', side_effect=MockRequestsUtil.mocked_requests)
def test_compare(mock_get):
    url, headers, params, body, status_code = ('http://url', {'header1' : 'value1'}, {'param1' : 'value1'}, 'body1', 200)
    response = HttpUtil.get_request(url=url, headers=headers, params=params)
    assert response.headers == headers

@mock.patch('requests.post', side_effect=MockRequestsUtil.mocked_requests)
def test_compare_p(mock_post):
    url, headers, params, body, status_code = ('http://url', {'header1' : 'value1'}, {'param1' : 'value1'}, 'body1', 200)
    response = HttpUtil.post_request(url=url, headers=headers, params=params, body=body)
    assert response.headers == headers