class ListUtil:
    result = []

    @staticmethod
    def equals(list_one, list_two, type_for_equal=None):
        type_for_equal = type_for_equal.lower()
        if 'string' in type_for_equal :
            list_one = list(map(str, list_one))
            list_two = list(map(str, list_two))
        elif 'int' in type_for_equal:
            list_one = list(map(int, list_one))
            list_two = list(map(int, list_two))
        map(ListUtil.compare, list_one, list_two)
        return ListUtil.result

    @staticmethod
    def compare(value_one, value_two):
        ListUtil.result[-1] = [value_one, value_two, value_one == value_two]

