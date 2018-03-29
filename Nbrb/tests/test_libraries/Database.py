from test_data.variables import DatabaseData
from framework.interface_utils.DatabaseUtil import get_random_entry, select_entries
from framework.interface_utils.SqlRequests import select_request


def get_dynamic_currency_from_database(start_date, end_date):
    random_entry = get_random_entry(DatabaseData.dynamics_table_name)
    cur_id = random_entry['Cur_id']
    DatabaseData.cur_id = cur_id
    sql_start_date = 'TO_DATE(\'{start_date}\', \'YYYY-MM-DD\')'.format(start_date=start_date)
    sql_end_date = 'TO_DATE(\'{end_date}\', \'YYYY-MM-DD\')'.format(end_date=end_date)
    select_cond = 'cur_id={cur_id} and date>={start_date} and date<={end_date}'.format(cur_id=cur_id,
                                                                                       start_date=sql_start_date,
                                                                                       end_date=sql_end_date)
    entries = select_entries(select_request(table_name=DatabaseData.dynamics_table_name,
                                            condition=select_cond))
    return entries


def get_currency_info_from_database():
    entry = get_random_entry(DatabaseData.currencies_table_name)
    DatabaseData.cur_id = entry['CUR_ID']
    return entry
