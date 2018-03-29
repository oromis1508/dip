import cx_Oracle as cx_Oracle

from test_data.variables import DatabaseData
from framework.interface_utils.SqlRequests import select_request


def __dict_factory(cursor, row):
    d = {}
    for idx, col in enumerate(cursor.description):
        d[col[0]] = row[idx]
    return d


def make_dict_factory(cursor):
    column_names = [d[0] for d in cursor.description]

    def create_row(*args):
        return dict(zip(column_names, args))

    return create_row


def connect():
    conn = cx_Oracle.connect(DatabaseData.database_connect_string, encoding="UTF8")
    return conn


def select_one_entry(request):
    cursor = connect().cursor()
    cursor.execute(request)
    cursor.rowfactory = make_dict_factory(cursor)
    result = cursor.fetchone()
    cursor.close()
    return result


def select_entries(request):
    cursor = connect().cursor()
    cursor.execute(request)
    cursor.rowfactory = make_dict_factory(cursor)
    result = cursor.fetchall()
    cursor.close()
    return result


def get_random_entry(table_name):
    request = select_request(table_name='{table_name} SAMPLE(10)'.format(table_name=table_name),
                             condition='ROWNUM = 1')
    return select_one_entry(request)
