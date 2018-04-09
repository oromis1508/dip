from framework.interface_utils.test_utils.Asserts import Asserts
from framework.interface_utils.test_utils.Logger import Logger
from framework.support.ListUtil import empty_string_to_none


def soft_should_be_equal(expected_result, actual_result, message):
    """Call the soft assert method

    :param expected_result: expected value to asserting
    :param actual_result: real value to asserting
    :param(str) message: friendly message, added to log with assert result
    """
    Asserts.soft_assert(expected_result=expected_result, actual_result=actual_result, message=message)


def raise_all_soft_asserts():
    """Call the method which throws AssertException if it's found in any soft assert
    """
    Asserts.assert_all()


def should_be_equal_values_of_list_dictionaries(exp_dicts, act_dicts, message):
    """Equate values of dictionaries in list by soft assert
    :param(list of dict) exp_dicts: expected list of dictionaries to asserting
    :param(list of dict) act_dicts: real list of dictionaries to asserting
    :param(str) message: friendly message, added to log with assert result
    """
    for i in range(0, len(exp_dicts)):
        Asserts.soft_assert(expected_result=str(exp_dicts[i].values()),
                            actual_result=str(act_dicts[i].values()),
                            message=message)


def should_be_equal_ignore_difference_none_and_empty_string(exp_list, act_list, message):
    """Equate json files with transformation empty strings to None type

    :param(dict or list of dict) exp_list: expected values to asserting
    :param(dict or list of dict) act_list: real values to asserting
    :param(str) message: friendly message, added to log with assert result
    """
    if type(exp_list) == dict:
        Asserts.equal_lists_with_conversion_empty_string_to_none(exp_list=exp_list.values(),
                                                                 act_list=act_list.values(),
                                                                 message=message)
    else:
        for i in range(0, len(exp_list)):
            Asserts.equal_lists_with_conversion_empty_string_to_none(exp_list=exp_list[i].values(),
                                                                     act_list=act_list[i].values(),
                                                                     message=message)

