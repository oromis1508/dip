import pytest

from framework.interface_drivers.Logger import Logger
from framework.support.ListUtil import ListUtil


@pytest.fixture(scope="function",
                params=[([1, 2, 3], ['1', '2', '3'], 'int', 0),
                        ([1, 2, 3], ['1', '2', '3'], 'string', 0),
                        ([1, 2, 3], ['1', '2', '3'], 'double', 3),
                        ([1, 2, 3], ['1', '2', '3'], '', 3),
                        ([1, 5, 3], ['1', '2', '3'], 'int', 1)
                        ])
def param_lists(request):
    return request.param

def test_list_compare(param_lists):
    value_one, value_two, type_for_equal, expected_result = param_lists
    actual_result = ListUtil.equals(value_one, value_two, type_for_equal)

    try:
        assert expected_result == len(actual_result)
        Logger.add_log(message='list compare success with params: {}'.format(str(param_lists)))
    except AssertionError:
        Logger.add_log(message='list compare failed with params: {}'.format(str(param_lists)))
        raise
