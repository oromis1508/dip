from framework.interface_drivers.logger.Logger import Logger


class Asserts:
    assert_stack = []

    @staticmethod
    def soft_assert(expected_result: object, actual_result: object) -> object:
        try:
            assert expected_result == actual_result
            Logger.add_log(message='{expected_result} == {actual_result} : SUCCESS')
        except AssertionError:
            Asserts.assert_stack.append('assert failed: expected {expected_result} but found {actual_result}'
                                        .format(expected_result=str(expected_result),
                                                actual_result=str(actual_result)))

    @staticmethod
    def assert_all():
        if len(Asserts.assert_stack) > 0 and Asserts.assert_stack[0]:
            for assert_fail in Asserts.assert_stack:
                Logger.add_log(log_file_name=Asserts.__class__.__name__,
                               message=assert_fail,
                               log_type='warn')
            raise AssertionError
        else:
            Logger.add_log(log_file_name=Asserts.__class__.__name__,
                           message='No failed asserts found')
