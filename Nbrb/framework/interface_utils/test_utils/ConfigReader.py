from framework.data_processors.Json import get_json_value, set_json_value
from test_data.variables import TestDataVariables

config_path = TestDataVariables.json_scheme_path.format('configuration')


def get_config(key):
    """ Returns value of configuration file by the key. Config file path is in variable config_path

    :param(str) key: key of the variable in json configuration file
    :return: value of the key
    """
    return get_json_value(keys=[key], file_path=config_path)


def set_config(key, value):
    """ Set value of configuration file by the key. Config file path is in variable config_path

    :param(str) key: key of the variable in json configuration file
    :param value: value to set for the configuration
    """
    set_json_value(keys=[key], value=value, file_path=config_path)
