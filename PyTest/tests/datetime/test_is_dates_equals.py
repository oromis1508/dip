import datetime
import pytest
from framework.support.DateTimeUtil import DateTimeUtil


@pytest.fixture(scope="function",
                params=[(datetime.datetime(1990, 10, 22, 8, 44, 59), datetime.datetime(1990, 10, 22, 8, 44, 59), 0, 'second', True),
                        (datetime.datetime(1990, 9, 30, 8, 44, 59), datetime.datetime(1990, 10, 21, 8, 44, 59), 22, 'day', True),
                        (datetime.datetime(1990, 9, 30, 8, 44, 59), datetime.datetime(1990, 10, 21, 8, 44, 59), 21, 'day', True),
                        (datetime.datetime(1990, 9, 30, 8, 44, 59), datetime.datetime(1990, 10, 21, 8, 44, 59), 20, 'day', False)
                        ])
def param_equal(request):
    return request.param

def test_date_equals(param_equal):
    date_one, date_two, variation_value, variation_type, expected_result = param_equal
    actual_result = DateTimeUtil.equal_dates(date_one, date_two, variation_value, variation_type)
    assert expected_result == actual_result
