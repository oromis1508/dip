from test_data.variables import DatabaseData, WebServiceMethods
from framework.interface_utils import DatabaseUtil, SqlRequests
from framework.interface_utils.HttpUtil import get_request
from framework.support import DictionaryUtil
from framework.support.DateTimeUtil import DateTimeUtil
from framework.support.ListUtil import list_combine


def create_table(table_name):
    cursor = DatabaseUtil.connect().cursor()
    cursor.execute(SqlRequests.select_request(table_name='user_tables',
                                              fields='count(*)',
                                              condition='table_name = \'{table_name}\''.format(table_name=table_name)))
    result = cursor.fetchone()
    is_table_exists = result[0]
    if is_table_exists:
        cursor.execute(SqlRequests.drop_table_request(table_name))

    created_table_info = ', '.join(list_combine(list_one=DatabaseData.column_names[table_name],
                                                list_two=DatabaseData.column_types[table_name],
                                                separator=' '))
    create_table_request = SqlRequests.create_table(table_name=table_name,
                                                    column_info=created_table_info)
    cursor.execute(create_table_request)

    cursor.execute(SqlRequests.select_request(table_name='USER_TAB_COLUMNS',
                                              condition='table_name = \'{table_name}\''.format(
                                                  table_name=DatabaseData.currencies_table_name),
                                              fields='column_name, data_type, data_length'))
    cursor.close()


def initialize_currencies_table():
    response = get_request(WebServiceMethods.get_currencies).json()

    conn = DatabaseUtil.connect()
    cursor = conn.cursor()
    for entry in response:
        entry = DictionaryUtil.shielding_comas_to_sql_requests(entry)
        entry = DictionaryUtil.add_commas_to_strings(entry)
        request = SqlRequests.insert_request(DatabaseData.currencies_table_name, ', '.join(map(str, entry.values())))
        cursor.execute(request)

    conn.commit()
    cursor.close()


def initialize_dynamics_table():
    response = get_request(WebServiceMethods.get_currencies).json()
    start_date = DateTimeUtil.shift_time(date=DateTimeUtil.get_current_date(date_format=None),
                                         shift_value=-30,
                                         shift_type='day')
    params = {'startDate': start_date, 'endDate': DateTimeUtil.get_current_date()}

    conn = DatabaseUtil.connect()
    cursor = conn.cursor()

    for currency in response:
        response = get_request(url='{dynamic_url}/{cur_id}'.format(dynamic_url=WebServiceMethods.get_dynamic,
                                                                   cur_id=currency['Cur_ID']),
                               params=params).json()
        if len(response):
            for entry in response:
                entry['Date'] = 'TO_DATE(\'{date}\', \'YYYY-MM-DDTHH24:MI:SS\')'

                cursor.execute(
                    SqlRequests.insert_request(DatabaseData.dynamics_table_name, ', '.join(map(str, entry))))

    conn.commit()
    cursor.close()
