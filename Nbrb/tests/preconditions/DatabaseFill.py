from framework.interface_utils.test_utils.Logger import Logger
from test_data.variables import DatabaseDataVariables, WebServiceMethods
from framework.interface_utils.database import SqlRequests, DatabaseUtil
from framework.interface_utils.http.HttpUtil import get_request
from framework.support import DictionaryUtil
from framework.support.DateTimeUtil import DateTimeUtil
from framework.support.ListUtil import string_lists_combine


def create_table(table_name):
    """ Creates table in the database
    :param(str) table_name: name of created table
    """
    cursor = DatabaseUtil.connect().cursor()
    cursor.execute(SqlRequests.select_request(table_name='user_tables',
                                              fields='count(*)',
                                              condition='table_name = \'{table_name}\''.format(table_name=table_name)))
    result = cursor.fetchone()
    is_table_exists = result[0]
    if is_table_exists:
        cursor.execute(SqlRequests.drop_table_request(table_name))

    created_table_info = ', '.join(string_lists_combine(list_one=DatabaseDataVariables.column_names[table_name],
                                                        list_two=DatabaseDataVariables.column_types[table_name],
                                                        separator=' '))
    create_table_request = SqlRequests.create_table(table_name=table_name,
                                                    column_info=created_table_info)
    cursor.execute(create_table_request)

    cursor.close()


def initialize_table(table_name):
    """ Inserts values in created table
    :param(str) table_name: name of table to insert
    """
    if table_name == DatabaseDataVariables.currencies_table_name:
        __initialize_currencies_table()
    elif table_name == DatabaseDataVariables.dynamics_table_name:
        __initialize_dynamics_table()
    else:
        Logger.add_log(message='Initialization of database failed: name of table {table_name} invalid'
                       .format(table_name=table_name),
                       log_type='warn')


def __initialize_currencies_table():
    """ Inserts values in currencies table
    """
    response = get_request(WebServiceMethods.get_currencies).json()

    conn = DatabaseUtil.connect()
    cursor = conn.cursor()
    for entry in response:
        entry = DictionaryUtil.shielding_comas_to_sql_requests(entry)
        entry = DictionaryUtil.add_commas_to_strings(entry)
        request = SqlRequests.insert_request(DatabaseDataVariables.currencies_table_name,
                                             ', '.join(map(str, entry.values())))
        cursor.execute(request)

    conn.commit()
    cursor.close()


def __initialize_dynamics_table():
    """ Inserts values in dynamics table. Date converted to the Sql Date type
    """
    response = get_request(WebServiceMethods.get_currencies).json()
    start_date = DateTimeUtil.shift_time(date=DateTimeUtil.get_current_date(date_format=None),
                                         shift_value=-1,
                                         shift_type='month')
    params = {'startDate': start_date, 'endDate': DateTimeUtil.get_current_date()}

    conn = DatabaseUtil.connect()
    cursor = conn.cursor()

    for currency in response:
        response = get_request(url='{dynamic_url}/{cur_id}'.format(dynamic_url=WebServiceMethods.get_dynamic,
                                                                   cur_id=currency['Cur_ID']),
                               params=params).json()
        if len(response):
            for entry in response:
                entry['Date'] = 'TO_DATE(\'{date}\', \'YYYY-MM-DD\"T\"HH24:MI:SS\')'.format(date=entry['Date'])
                request = SqlRequests.insert_request(DatabaseDataVariables.dynamics_table_name,
                                                     ', '.join(map(str, entry.values())))

                cursor.execute(request)

    conn.commit()
    cursor.close()
