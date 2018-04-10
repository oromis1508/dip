def string_lists_combine(list_one, list_two, separator=''):
    """ Combines two list in one with the separator
        Example:\n
        list_one = ['el_1', 'el_2']\n
        list_two = ['el_3', 'el_4']\n
        separator = '='\n
        result = ['el_1=el_3', 'el_2=el_4']
    :param(list) list_one: combining list
    :param(list) list_two: combining list
    :param separator: list values separator
    :return: combined list
    """
    result = []
    for i in range(0, len(list_one)):
        result.append(list_one[i] + separator + list_two[i])
    return result


def empty_string_to_none(custom_list):
    """ Replaces empty string in list with None elements
    :param(list) custom_list: editing list
    :return: edited list
    """
    for i in range(0, len(custom_list)):
        if custom_list[i] == '':
            custom_list[i] = None
    return custom_list
