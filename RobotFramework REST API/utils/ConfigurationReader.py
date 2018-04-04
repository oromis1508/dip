import json
import os


def get_setting(key):
    base_dir = os.path.dirname(os.path.abspath(__file__))
    settings_path = os.path.join(base_dir, '../resources/configuration.json')
    json_data = json.load(open(settings_path))
    return json_data[key]
