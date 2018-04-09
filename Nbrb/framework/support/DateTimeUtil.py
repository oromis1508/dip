import datetime


class DateTimeUtil:

    @staticmethod
    def get_current_date(date_format='%Y-%m-%d'):
        """ Returns string representation of the today's date

        :param date_format: format of returned string (default: '%Y-%m-%d')
        :return: string of the date
        """
        if not date_format:
            return datetime.date.today()
        return datetime.date.today().strftime(date_format)

    @staticmethod
    def shift_time(date, shift_value, shift_type):
        """ Shift date on needed time

        :param(datetime.datetime or datetime.date) date: source date
        :param(str or int) shift_value: number of shifting
        :param(str) shift_type: type of shifting (maybe 'year', 'month', 'day', 'second')
        :return: shifted datetime
        """
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
            if type(date) is datetime.date:
                return datetime.datetime(year=date.year + years_shift,
                                         month=month_shifted,
                                         day=date.day)

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
