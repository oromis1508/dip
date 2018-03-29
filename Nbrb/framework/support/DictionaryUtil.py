def add_commas_to_strings(dictionary):
    for key in dictionary.keys():
        if isinstance(dictionary[key], str):
            dictionary[key] = '\'{}\''.format(dictionary[key])
    return dictionary


def replace_empty_strings_with_null_sql_type(dictionary):
    for key in dictionary.keys():
        if dictionary[key] == '\'\'' or dictionary[key] == '':
            dictionary[key] = 'Null'
    return dictionary


def shielding_comas_to_sql_requests(dictionary):
    for key in dictionary.keys():
        if isinstance(dictionary[key], str):
            dictionary[key] = dictionary[key].replace('\'', '\'\'')
    return dictionary
