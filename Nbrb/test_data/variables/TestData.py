import os

__resources_dir = os.path.join(os.path.dirname(os.path.abspath(__file__)), '../resources')
json_scheme_path = str(__resources_dir) + '/{}.json'

valid_response_code = 200
response_code_not_found = 404
