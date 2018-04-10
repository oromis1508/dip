import os
import xlrd

from framework.interface_utils.test_utils.Logger import Logger


def get_sheet_dict(file_name, sheet_name, values_sets, keys_num):
    """ Method for getting data from excel file.
        The keys to the data are the first column on the worksheet of the excel file.
        If the cell type is date, the value is converted to the datetime type
    :param(int) keys_num: number of the not empty worksheet rows
    :param(int) values_sets: number of the not empty columns without the column with names
    :param(str) file_name: path to opened excel file (not supported the xlst format)
    :param(str) sheet_name: the name of the worksheet that contains data
    :returns: the list of dictionaries. Each dict contains the values from not empty column of the worksheet.
    """
    book = xlrd.open_workbook(filename=file_name)
    sheet = book.sheet_by_name(sheet_name)

    row = 0
    column = 1
    result = []
    try:
        while column <= values_sets:
            values = {}
            try:
                while row < keys_num:
                    name = sheet.cell_value(rowx=row, colx=0)

                    if sheet.cell_type(rowx=row, colx=column) == xlrd.XL_CELL_DATE:
                        date_as_datetime = xlrd.xldate_as_datetime(sheet.cell_value(rowx=row, colx=column), 0)
                        value = date_as_datetime.date().isoformat()
                    elif sheet.cell_type(rowx=row, colx=column) == xlrd.XL_CELL_NUMBER:
                        value = sheet.cell_value(rowx=row, colx=column)
                        if float(value) % 1 < 0.00001:
                            value = int(value)
                    else:
                        value = sheet.cell_value(rowx=row, colx=column)

                    values[name] = value

                    row += 1
            except IndexError:
                Logger.add_log(message='The python library \'xlrd\' not supported checking on empty cells',
                               class_name=os.path.basename(__file__))

            result.append(values)
            row = 0
            column += 1
    except IndexError:
        Logger.add_log(message='The python library \'xlrd\' not supported checking on empty cells',
                       class_name=os.path.basename(__file__))

    return result
