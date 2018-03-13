import datetime

class DateTimeUtil :

    @staticmethod
    def shift_time(date, shift_value, shift_type):
        type_lower = shift_type.lower()
        if type_lower == 'year':
            year = date.year + shift_value
            return datetime.datetime(year, date.month, date.day, date.hour, date.minute, date.second)
        elif type_lower == 'month':
            month = date.month + shift_value
            return datetime.datetime(date.year, month, date.day, date.hour, date.minute, date.second)
        elif type_lower == 'day':
            return date + datetime.timedelta(days=shift_value)
        elif type_lower == 'second':
            return date + datetime.timedelta(seconds=shift_value)

    @staticmethod
    def equal_dates(date_one, date_two, variation_datetime):
        date_difference = abs(date_one - date_two)
        if date_difference < variation_datetime:
            return True
        else:
            return False
        
    @staticmethod
    def is_date_between(date, lower_boundary, upper_boundary):
        return lower_boundary < date < upper_boundary
