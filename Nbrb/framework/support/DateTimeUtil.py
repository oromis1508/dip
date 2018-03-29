import datetime


class DateTimeUtil:

    @staticmethod
    def is_date_between(date, lower_boundary, upper_boundary):
        return lower_boundary <= date <= upper_boundary

    @staticmethod
    def get_current_date(date_format='%Y-%m-%d'):
        if not date_format:
            return datetime.date.today()
        return datetime.date.today().strftime(date_format)

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

