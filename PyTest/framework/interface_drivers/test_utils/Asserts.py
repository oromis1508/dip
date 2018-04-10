from framework.interface_drivers.logger.Logger import Logger


class Asserts:
    assert_stack = []

    @staticmethod
    def soft_assert(expected_result, actual_result):
        try:
            assert expected_result == actual_result
            Logger.add_log(message='{expected_result} == {actual_result} : SUCCESS'
                           .format(expected_result=expected_result,
                                   actual_result=actual_result))
        except AssertionError:
            Asserts.assert_stack.append('assert failed: expected {expected_result} but found {actual_result}'
                                        .format(expected_result=expected_result,
                                                actual_result=actual_result))

    @staticmethod
    def assert_all():
        if len(Asserts.assert_stack) > 0:
            for assert_fail in Asserts.assert_stack:
                Logger.add_log(class_name=Asserts.__class__.__name__,
                               message=assert_fail,
                               log_type='warn')
            Asserts.assert_stack.clear()
            raise AssertionError
        else:
            Logger.add_log(class_name=Asserts.__class__.__name__,
                           message='No failed asserts found')

    @staticmethod
    def assert_lists_various_length(expected_list, actual_list):
        max_len = max(len(expected_list), len(actual_list))
        for i in range(0, max_len):
            actual_value = Asserts.__get_list_value_for_compare__(actual_list, i)
            expected_value = Asserts.__get_list_value_for_compare__(expected_list, i)
            Asserts.soft_assert(expected_value, actual_value)
        Asserts.assert_all()

    @staticmethod
    def __get_list_value_for_compare__(some_list, iterator):
        if len(some_list) == iterator:
            res_value = None
        else:
            res_value = some_list[iterator]
        return res_value
