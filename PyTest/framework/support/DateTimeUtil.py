import datetime
from dateutil.relativedelta import relativedelta

from framework.interface_drivers.logger.Logger import Logger


class DateTimeUtil:

    @staticmethod
    def shift_time(date, shift_value, shift_type):
        type_lower = shift_type.lower()
        if type_lower == 'year':
            year = date.year + shift_value
            return datetime.datetime(year=year,
                                     month=date.month,
                                     day=date.day,
                                     hour=date.hour,
                                     minute=date.minute,
                                     second=date.second)
        elif type_lower == 'month':
            years_shift = (date.month + shift_value) // 12
            month_shifted = (date.month + shift_value) % 12
            return datetime.datetime(year=date.year + years_shift,
                                     month=month_shifted,
                                     day=date.day,
                                     hour=date.hour,
                                     minute=date.minute,
                                     second=date.second)
        elif type_lower == 'day':
            return date + datetime.timedelta(days=shift_value)
        elif type_lower == 'second':
            return date + datetime.timedelta(seconds=shift_value)
        else:
            return 'invalid parameter shift_type, need: year, month, day or second'

    @staticmethod
    def equal_dates(date_one, date_two, variation_value, variation_type):
        variation_type = variation_type.lower()
        date_difference = abs(date_one - date_two)

        if variation_type == 'second':
            variation = datetime.timedelta(seconds=variation_value)
        elif variation_type == 'minute':
            variation = datetime.timedelta(seconds=variation_value * 60)
        elif variation_type == 'hour':
            variation = datetime.timedelta(seconds=variation_value * 60 * 60)
        elif variation_type == 'day':
            variation = datetime.timedelta(days=variation_value)
        elif variation_type == 'month':
            if date_one < date_two:
                date_one += relativedelta(months=variation_value)
                return date_one >= date_two
            else:
                date_two += relativedelta(months=variation_value)
                return date_two >= date_one
        elif variation_type == 'year':
            leap = variation_value // 4 - 1
            variation = datetime.timedelta(days=(variation_value * 365 + leap))
        else:
            Logger.add_log(message='Invalid format of variation type')
            return False

        return date_difference <= variation

    @staticmethod
    def is_date_between(date, lower_boundary, upper_boundary):
        return lower_boundary <= date <= upper_boundary
