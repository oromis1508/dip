import pytest
from framework.interface_drivers.HttpUtil import HttpUtil


@pytest.fixture(scope="function",
                params=[('get', 'url', {'header' : 'value'}, {'param1' : 'value1'}),
                        ('get', 'url', ),
                        (6, 5)])
def param_test(request):
    return request.param

def test_compare(param_test):
    HttpUtil.send_request(param_test)