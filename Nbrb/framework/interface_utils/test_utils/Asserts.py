from framework.interface_utils.test_utils.Logger import Logger
from framework.support.ListUtil import empty_string_to_none


class Asserts:
    assert_stack = []

    @staticmethod
    def soft_assert(expected_result, actual_result, message):
        """ Soft assert to compare equality of two values

        :param expected_result: expected value for compare
        :param actual_result: actual value for compare
        :param(str) message: friendly message for equality
        """
        try:
            assert expected_result == actual_result
            Logger.add_log(message='{message}\n{expected_result} == {actual_result} : SUCCESS'
                           .format(message=message,
                                   expected_result=expected_result,
                                   actual_result=actual_result))
        except AssertionError:
            Logger.add_log(message='assert failed: expected {expected_result} but found {actual_result}'
                           .format(expected_result=str(expected_result),
                                   actual_result=str(actual_result)))
            Asserts.assert_stack.append('--{message}--FAIL:\n'
                                        'expected: {expected_result}, found: {actual_result}'
                                        .format(message=message,
                                                expected_result=str(expected_result),
                                                actual_result=str(actual_result)))

    @staticmethod
    def assert_all():
        """ Added the message to the log about the found exceptions in the soft assert method
            or that exceptions not found. Throws AssertionError if were found exceptions in the soft assert method
        """
        if len(Asserts.assert_stack) > 0:
            for assert_fail in Asserts.assert_stack:
                Logger.add_log(class_name=Asserts.__class__.__name__,
                               message=assert_fail,
                               log_type='warn')
            raise AssertionError
        else:
            Logger.add_log(class_name=Asserts.__class__.__name__,
                           message='No failed asserts found')

        Asserts.assert_stack.clear()

    @staticmethod
    def equal_lists_with_conversion_empty_string_to_none(exp_list, act_list, message):
        """Equate lists with transformation empty strings to None type

        :param exp_list: expected list to asserting
        :param act_list: real list to asserting
        :param(str) message: friendly message, added to log with assert result
        """
        exp_list = empty_string_to_none(list(exp_list))
        act_list = empty_string_to_none(list(act_list))

        try:
            assert exp_list == act_list
            Logger.add_log('--{message}--SUCCESS\n'
                           '{expected} equal\n{actual}'.format(message=message,
                                                               expected=exp_list,
                                                               actual=act_list))
        except AssertionError:
            Logger.add_log('--{message}--FAIL\n'
                           '{expected} not equal\n{actual}'.format(message=message,
                                                                   expected=exp_list,
                                                                   actual=act_list))
            raise AssertionError
