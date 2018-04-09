import json
from jsonschema import validate, ValidationError

from framework.interface_utils.test_utils.Logger import Logger


def is_json_scheme_valid(json_file, scheme_file):
    """ Checks json scheme of the file

    :param json_file: validated json file
    :param scheme_file: json file with json scheme
    :return: True if the file scheme is valid otherwise - False
    """
    scheme = json.load(scheme_file)
    try:
        validate(json_file, scheme)
        return True
    except ValidationError:
        return False


def get_json_value(file_path, keys):
    """ Return the value by key from json file

    :param(str) file_path: path to json file
    :param(list) keys: list of keys for search value
    Example:
        For file {'key' : {'sub_key' : 'value'}} keys will be ['key', 'sub_key']
    :return: value of the last key in the list keys
    """
    with open(file=file_path) as file:
        json_file = json.load(file)
        value = None
        for key in keys:
            value = json_file[key]
            json_file = value
        if value is None:
            Logger.add_log(message='Invalid keys {keys} for json {json_path}'.format(keys=keys, json_path=file_path),
                           log_type='warn')
    return value


def set_json_value(file_path, keys, value):
    """ Set value to key in json file

    :param(str) file_path: path to json file
    :param(list) keys: list of keys for search edited key
    Example:
        For file {'key' : {'sub_key' : 'value'}} keys will be ['key', 'sub_key']
        Value will be set to 'sub_key' in the block 'key'
    :param value: the value to set for the key
    """
    json_file = json.load(open(file=file_path))
    if len(keys) > 1:
        for i in range(0, len(keys) - 1):
            block = json_file[keys[i]]
            json_file = block
    json_file[keys[-1]] = value
    json.dump(json_file, open(file=file_path, mode='w'))
