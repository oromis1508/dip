import time

class Waiter:

    @staticmethod
    def wait_for_result(some_function, timeout=60, period=5):
        result = None
        waiting_time = 0
        while result is None and waiting_time < timeout:
            result = some_function()
            waiting_time += period
            time.sleep(period)