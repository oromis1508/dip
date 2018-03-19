import datetime
import pytest

from framework.utils.DateTimeUtil import DateTimeUtil

@pytest.fixture(scope="function",
                params=[(datetime.datetime(1990, 10, 22, 8, 44, 59), 10, 'day', datetime.datetime(1990, 11, 1, 8, 44, 59)),
                        (datetime.datetime(1990, 10, 22, 8, 44, 59), -10, 'year', datetime.datetime(1980, 10, 22, 8, 44, 59)),
                        (datetime.datetime(1990, 10, 22, 8, 44, 59), 5, 'month', datetime.datetime(1991, 3, 22, 8, 44, 59)),
                        (datetime.datetime(1990, 10, 22, 8, 44, 59), -100, 'second', datetime.datetime(1990, 10, 22, 8, 43, 19)),
                        (datetime.datetime(1990, 10, 22, 8, 44, 59), 0, 'second', datetime.datetime(1990, 10, 22, 8, 44, 59)),
                        (datetime.datetime(1990, 10, 22, 8, 44, 59), 10, 'hour', 'invalid parameter shift_type, need: year, month, day or second')
                        ])
def param_shift(request):
    return request.param

def test_shift_time(param_shift):
    date, shift_value, shift_type, expected_result = param_shift
    actual_result = DateTimeUtil.shift_time(date, shift_value, shift_type)
    assert expected_result == actual_result

