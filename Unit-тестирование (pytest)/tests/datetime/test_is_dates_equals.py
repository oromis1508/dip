import datetime
import pytest
from framework.utils.DateTimeUtil import DateTimeUtil


@pytest.fixture(scope="function",
                params=[(datetime.datetime(1990, 10, 22, 8, 44, 59), datetime.datetime(1990, 10, 22, 8, 44, 59), datetime.datetime(0,0,0,0,0,0), True),
                        (datetime.datetime(1990, 9, 30, 8, 44, 59), datetime.datetime(1990, 10, 31, 8, 44, 59), datetime.datetime(0,0,32,0,0,0), True),
                        (datetime.datetime(1990, 9, 30, 8, 44, 59), datetime.datetime(1990, 10, 31, 8, 44, 59), datetime.datetime(0,0,31,0,0,0), False)
                        ])
def param_equal(request):
    return request.param

def test_date_equals(param_equal):
    date_one, date_two, variation_datetime, expected_result = param_equal
    actual_result = DateTimeUtil.equal_dates(date_one, date_two, variation_datetime)
    assert expected_result == actual_result
