import datetime
import pytest
from framework.support.DateTimeUtil import DateTimeUtil


@pytest.fixture(scope="function",
                params=[(datetime.datetime(1990, 10, 22, 8, 44, 59), datetime.datetime(1990, 10, 22, 8, 44, 59), datetime.datetime(1990, 10, 22, 8, 44, 59), True),
                        (datetime.datetime(1990, 9, 30, 8, 44, 59), datetime.datetime(1989, 9, 30, 8, 44, 59), datetime.datetime(1990, 9, 30, 10, 44, 59), True),
                        (datetime.datetime(1991, 9, 30, 8, 44, 59), datetime.datetime(1990, 10, 31, 8, 44, 59), datetime.datetime(1990, 12, 31, 8, 44, 59), False)
                        ])
def param_between(request):
    return request.param

def test_is_datetime_between(param_between):
    date, lower_boundary, upper_boundary, expected_result = param_between
    actual_result = DateTimeUtil.is_date_between(date, lower_boundary, upper_boundary)
    assert expected_result == actual_result
