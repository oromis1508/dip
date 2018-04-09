from framework.data_processors.Json import is_json_scheme_valid
from test_data.variables import TestDataVariables


def validate_json_scheme(json_file, scheme_name):
    """ Call method to checking is json file match with scheme
    :param json_file: checking file
    :param(str) scheme_name: validation scheme
    :return: True if the file is valid
    """
    scheme_json = TestDataVariables.json_scheme_path.format(scheme_name)
    return is_json_scheme_valid(json_file=json_file, scheme_file=open(file=scheme_json, encoding='utf-8'))
