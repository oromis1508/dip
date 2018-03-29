from framework.data_processors.JsonShemeValidator import is_json_valid
from test_data.variables import TestData


def validate_json_scheme(json_file, scheme_name):
    scheme_json = TestData.json_scheme_path.format(scheme_name)
    return is_json_valid(json_file=json_file, scheme_file=open(file=scheme_json, encoding='utf-8'))
