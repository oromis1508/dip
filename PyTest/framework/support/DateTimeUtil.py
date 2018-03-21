import datetime

class DateTimeUtil :

    @staticmethod
    def shift_time(date, shift_value, shift_type):
        type_lower = shift_type.lower()
        if type_lower == 'year':
            year = date.year + shift_value
            return datetime.datetime(year, date.month, date.day, date.hour, date.minute, date.second)
        elif type_lower == 'month':
            years_shift = (date.month + shift_value) // 12
            month_shifted = (date.month + shift_value) % 12

            return datetime.datetime(date.year + years_shift, month_shifted, date.day, date.hour, date.minute, date.second)
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

        seconds = date_difference.seconds % 60
        minutes = (date_difference.seconds // 60) % 60
        hours = (date_difference.seconds // (60*60)) % 24
        days = date_difference.days % 30
        months = date_difference.days // 30
        years = date_difference.days // 365

        if variation_type == 'second':
            return not years and not months and not days and not hours and not minutes and seconds <= variation_value
        elif variation_type == 'minute':
            return not years and not months and not days and not hours and (minutes < variation_value or (minutes == variation_value and not seconds))
        elif variation_type == 'hour':
            return not years and not months and not days and (hours < variation_value or (hours == variation_value and not minutes and not seconds))
        elif variation_type == 'day':
            return not years and not months and (days < variation_value or (days == variation_value and not hours and not minutes and not seconds))
        elif variation_type == 'month':
            return not years and (months < variation_value or (months == variation_value and not days and not hours and not minutes and not seconds))
        elif variation_type == 'year':
            return years < variation_value or (years == variation_value and not months and not days and not hours and not minutes and not seconds)
        else:
            return False

    @staticmethod
    def is_date_between(date, lower_boundary, upper_boundary):
        return lower_boundary <= date <= upper_boundary
