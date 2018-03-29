import xlrd


def get_sheet_dict(file_name, sheet_name):
    book = xlrd.open_workbook(filename=file_name)
    sheet = book.sheet_by_name(sheet_name)
    row = 0
    result_dict = {}
    while sheet.cell(rowx=row, colx=0) != '':
        name = sheet.cell(rowx=row, colx=0)
        column = 1
        values = []
        while sheet.cell(rowx=row, colx=column) != '':
            values.append(sheet.cell(rowx=row, colx=column))
        result_dict.pop({name: values})
    return result_dict
