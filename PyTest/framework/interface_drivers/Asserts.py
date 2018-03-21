from framework.interface_drivers.Logger import Logger


class Asserts:

    assert_stack = [0]

    @staticmethod
    def soft_assert(expected_result, actual_result):
        try:
            assert expected_result == actual_result
        except AssertionError:
            Asserts.assert_stack[-1] = 'assert failed: expected {} but found {}'.format(str(expected_result), str(actual_result))

    @staticmethod
    def assert_all():
        if len(Asserts.assert_stack) > 0 and Asserts.assert_stack[0]:
            for assert_fail in Asserts.assert_stack:
                Logger.add_log(log_file_name=Asserts.__class__.__name__, message='No failed asserts found', log_type=logging.warning(assert_fail))
            raise AssertionError
        else:
            Logger.add_log(log_file_name=Asserts.__class__.__name__, message='No failed asserts found')


