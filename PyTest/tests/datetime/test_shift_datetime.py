import datetime
import pytest

from framework.interface_drivers.Logger import Logger
from framework.support.DateTimeUtil import DateTimeUtil

class TestCase:
    @pytest.fixture(scope="function",
                    params=[(datetime.datetime(1990, 10, 22, 8, 44, 59), 10, 'day', datetime.datetime(1990, 11, 1, 8, 44, 59)),
                            (datetime.datetime(1990, 10, 22, 8, 44, 59), -10, 'year', datetime.datetime(1980, 10, 22, 8, 44, 59)),
                            (datetime.datetime(1990, 10, 22, 8, 44, 59), 5, 'month', datetime.datetime(1991, 3, 22, 8, 44, 59)),
                            (datetime.datetime(1990, 10, 22, 8, 44, 59), -100, 'second', datetime.datetime(1990, 10, 22, 8, 43, 19)),
                            (datetime.datetime(1990, 10, 22, 8, 44, 59), 0, 'second', datetime.datetime(1990, 10, 22, 8, 44, 59)),
                            (datetime.datetime(1990, 10, 22, 8, 44, 59), 10, 'hour', 'invalid parameter shift_type, need: year, month, day or second')
                            ])
    def param_shift(self, request):
        return request.param

    def test_shift_time(self, param_shift):
        date, shift_value, shift_type, expected_result = param_shift
        actual_result = DateTimeUtil.shift_time(date, shift_value, shift_type)
        try:
            assert expected_result == actual_result
            Logger.add_log(message='test shift datetime success with params: {}'.format(str(param_shift)))
        except AssertionError:
            Logger.add_log(message='test shift datetime failed with params: {}'.format(str(param_shift)))
            raise

