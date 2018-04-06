import pytest

from framework.interface_drivers.logger.Logger import Logger
from framework.interface_drivers.test_utils.Asserts import Asserts
from framework.support.ListUtil import ListUtil


@pytest.fixture(scope="function",
                params=[([1, 2, 3], ['1', '2', '3'], 'int', []),
                        ([1, 2, 3], ['1', '2', '3'], 'string', []),
                        ([1, 2, 3], ['1', '2', '3'], 'double', [0, 1, 2]),
                        ([1, 2, 3], ['1', '2', '3'], '', [0, 1, 2]),
                        ([1, 5, 3], ['1', '2', '3'], 'int', [2])
                        ])
def param_lists(request):
    return request.param


def test_list_compare(param_lists):
    value_one, value_two, type_for_equal, not_equal_elements = param_lists
    actual_result = ListUtil.equals(list_one=value_one,
                                    list_two=value_two,
                                    type_for_equal=type_for_equal)
    actual_not_equal_elements = [el_info[2] for el_info in actual_result]
    Logger.add_log(message='type for conversion is {type}'.format(type=type_for_equal))
    map(Asserts.soft_assert, not_equal_elements, actual_not_equal_elements)
    Asserts.assert_all()
    Logger.add_log(message='list compare success with params: {param_lists}'.format(param_lists=str(param_lists)))
