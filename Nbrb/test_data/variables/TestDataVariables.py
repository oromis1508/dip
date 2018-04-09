import os

__resources_dir = os.path.join(os.path.dirname(os.path.abspath(__file__)), '../resources')
json_scheme_path = str(__resources_dir) + '/{}.json'

test_data_path = str(__resources_dir) + '/test_data.xls'
excel_sheet_currencies = 'Currency'
excel_sheet_dynamics = 'Dynamic'

currencies_test_set_value = 5
currencies_values_in_set = 3
dynamics_test_set_value = 8
dynamics_values_in_set = 5
