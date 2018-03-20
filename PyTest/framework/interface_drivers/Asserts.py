import logging

class Asserts:

    assert_stack = []

    @staticmethod
    def soft_assert(expected_result, actual_result):
        try:
            assert expected_result == actual_result
        except AssertionError:
            Asserts.assert_stack[-1] = 'assert failed: expected {} but found {}'.format(str(expected_result), str(actual_result))

    @staticmethod
    def assert_all():
        if len(Asserts.assert_stack) > 0:
            for assert_fail in Asserts.assert_stack:
                logging.error(assert_fail)
            raise AssertionError
        else:
            logging.info('No failed asserts found')


