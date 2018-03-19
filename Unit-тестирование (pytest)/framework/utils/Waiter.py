import time

class Waiter:

    @staticmethod
    def wait_for_result(some_function, args, return_value = not None, timeout=60, period=5):
        result = None
        waiting_time = 0
        while result != return_value and waiting_time < timeout:
            result = some_function(args)
            waiting_time += period
            time.sleep(period)