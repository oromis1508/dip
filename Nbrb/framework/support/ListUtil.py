def list_combine(list_one, list_two, separator=''):
    result = []
    for i in range(0, len(list_one)):
        result.append(list_one[i] + separator + list_two[i])
    return result
