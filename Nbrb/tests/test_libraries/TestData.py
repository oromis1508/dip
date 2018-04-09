from framework.data_processors.ExcelReader import get_sheet_dict
from test_data.variables import TestDataVariables


def get_test_data_list(excel_sheet, sets_of_test_data, data_one_set):
    """ Call method to getting test data from excel book

    :param(int) data_one_set: number of values in the data set
    :param(int) sets_of_test_data: number of the test data sets
    :param(str) excel_sheet: name of the excel worksheet
    :return: list of dictionaries with parameters
    """
    return get_sheet_dict(file_name=TestDataVariables.test_data_path,
                          sheet_name=excel_sheet,
                          values_sets=sets_of_test_data,
                          keys_num=data_one_set)
