def add_commas_to_strings(dictionary):
    """ Adds commas to values of dictionary if it type is string
        Example: string -> 'string'
    :param(dict) dictionary: source dictionary
    :return: modified dictionary
    """
    for key in dictionary.keys():
        if isinstance(dictionary[key], str):
            dictionary[key] = '\'{}\''.format(dictionary[key])
    return dictionary


def shielding_comas_to_sql_requests(dictionary):
    """ Modifies dictionary values. If type of value is string, replaces symbol (') on two ('')

    :param(dict) dictionary: source dictionary
    :return: modified dictionary
    """
    for key in dictionary.keys():
        if isinstance(dictionary[key], str):
            dictionary[key] = dictionary[key].replace('\'', '\'\'')
    return dictionary
