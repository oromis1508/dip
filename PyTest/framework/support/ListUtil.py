class ListUtil:
    result = []

    @staticmethod
    def equals(list_one, list_two, type_for_equal=''):
        ListUtil.result.clear()
        type_for_equal = type_for_equal.lower()

        if 'string' in type_for_equal :
            list_one = list(map(str, list_one))
            list_two = list(map(str, list_two))
        elif 'int' in type_for_equal:
            list_one = list(map(int, list_one))
            list_two = list(map(int, list_two))

        for i in range(0, len(list_one)):
            ListUtil.__compare(list_one[i], list_two[i], i)

        return ListUtil.result

    @staticmethod
    def __compare(value_one, value_two, position):
        if value_one != value_two:
            ListUtil.result.append([value_one, value_two, position])

