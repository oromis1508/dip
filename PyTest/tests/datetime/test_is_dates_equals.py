import datetime
import pytest

from framework.interface_drivers.logger.Logger import Logger
from framework.support.DateTimeUtil import DateTimeUtil


@pytest.fixture(scope="function",
                params=[(datetime.datetime(1990, 10, 22, 8, 44, 59), datetime.datetime(1990, 10, 22, 8, 44, 54), 5,
                         'second', True),
                        (datetime.datetime(1990, 10, 21, 8, 44, 59), datetime.datetime(1990, 10, 21, 8, 44, 53), 5,
                         'second', False),
                        (datetime.datetime(1990, 9, 30, 8, 44, 59), datetime.datetime(1990, 10, 21, 8, 44, 59), 22,
                         'day', True),
                        (datetime.datetime(1990, 9, 30, 8, 44, 59), datetime.datetime(1990, 10, 23, 8, 44, 59), 22,
                         'day', False),
                        (datetime.datetime(1990, 10, 21, 8, 44, 59), datetime.datetime(1990, 10, 23, 8, 44, 59), 48,
                         'hour', True),
                        (datetime.datetime(1990, 10, 21, 8, 44, 59), datetime.datetime(1990, 10, 23, 8, 45, 0), 48,
                         'hour', False),
                        (datetime.datetime(1990, 9, 30, 9, 17, 59), datetime.datetime(1990, 9, 30, 8, 44, 59), 33,
                         'minute', True),
                        (datetime.datetime(1990, 9, 30, 9, 18, 59), datetime.datetime(1990, 9, 30, 8, 44, 59), 33,
                         'minute', False),
                        (datetime.datetime(1991, 11, 21, 8, 44, 59), datetime.datetime(1990, 10, 21, 8, 44, 59), 13,
                         'month', True),
                        (datetime.datetime(1991, 11, 21, 9, 44, 59), datetime.datetime(1990, 10, 21, 8, 44, 59), 13,
                         'month', False),
                        (datetime.datetime(1990, 10, 21, 8, 44, 59), datetime.datetime(1890, 10, 21, 8, 44, 59), 100,
                         'year', True),
                        (datetime.datetime(1990, 10, 21, 8, 45, 0), datetime.datetime(1890, 10, 21, 8, 44, 59), 100,
                         'year', False)
                        ])
def param_equal(request):
    return request.param


def test_date_equals(param_equal):
    date_one, date_two, variation_value, variation_type, expected_result = param_equal
    actual_result = DateTimeUtil.equal_dates(date_one=date_one,
                                             date_two=date_two,
                                             variation_value=variation_value,
                                             variation_type=variation_type)
    try:
        assert expected_result == actual_result
        Logger.add_log(
            message='test datetime equal success with params: {param_equal}'.format(param_equal=str(param_equal)))
    except AssertionError:
        Logger.add_log(
            message='test datetime equal failed with params: {param_equal}'.format(param_equal=str(param_equal)))
        raise
