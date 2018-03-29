import json

from jsonschema import validate, ValidationError


def is_json_valid(json_file, scheme_file):
    scheme = json.load(scheme_file)
    try:
        validate(json_file, scheme)
        return True
    except ValidationError:
        return False
