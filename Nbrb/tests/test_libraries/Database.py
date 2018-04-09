from framework.interface_utils.test_utils.ConfigReader import get_config, set_config
from framework.support.DateTimeUtil import DateTimeUtil
from test_data.variables import DatabaseDataVariables
from framework.interface_utils.database.DatabaseUtil import select_entries
from framework.interface_utils.database.SqlRequests import select_request, select_random_entry
from tests.preconditions import DatabaseFill


def get_dynamic_currency_from_database(cur_id, start_date, end_date):
    """ Call the methods to search database entries by currency id and dates

    :param(str or int) cur_id: id for search in database, if it's 'random' - selected random entry from the table
    :param(str) start_date: start date to search in database (example: '2000-01-31')
    :param(str) end_date: start date to search in database (example: '2000-01-31')
    :return: the list of dictionaries of the entries
    """
    if start_date == '-' or end_date == '-' or type(start_date) == int or type(end_date) == int:
        return
    elif cur_id == '-':
        DatabaseDataVariables.cur_id = ''
        return

    if cur_id == 'random':
        random_entry = select_entries(select_random_entry(table_name=DatabaseDataVariables.dynamics_table_name,
                                                          entries_number=1))
        cur_id = random_entry['CUR_ID']

    DatabaseDataVariables.cur_id = cur_id
    sql_start_date = 'TO_DATE(\'{start_date}\', \'YYYY-MM-DD\')'.format(start_date=start_date)
    sql_end_date = 'TO_DATE(\'{end_date}\', \'YYYY-MM-DD\')'.format(end_date=end_date)
    select_cond = 'cur_id={cur_id} AND ' \
                  'Date_cur >= {start_date} AND ' \
                  'Date_cur <= {end_date}'.format(cur_id=cur_id,
                                                  start_date=sql_start_date,
                                                  end_date=sql_end_date)
    fields = 'Cur_ID, Date_cur, to_char(Cur_OfficialRate)'
    entries = select_entries(request=select_request(table_name=DatabaseDataVariables.dynamics_table_name,
                                                    condition=select_cond,
                                                    fields=fields),
                             is_one_entry=False)

    for i in range(0, len(entries)):
        entries[i]['TO_CHAR(CUR_OFFICIALRATE)'] = float(entries[i]['TO_CHAR(CUR_OFFICIALRATE)'])

    return entries


def get_currency_info_from_database(cur_id):
    """ Call the method to search database entry by currency id

    :param(str or int) cur_id: id for search in database, if it's 'random' - selected random entry from the table
    :return: the dictionary from database
    """
    if cur_id == 'random':
        entry = select_entries(select_random_entry(table_name=DatabaseDataVariables.currencies_table_name,
                                                   entries_number=1))
        DatabaseDataVariables.cur_id = entry['CUR_ID']
    elif cur_id == '-':
        DatabaseDataVariables.cur_id = ''
        entries = select_entries(select_request(table_name=DatabaseDataVariables.currencies_table_name),
                                 is_one_entry=False)
        return entries
    elif type(cur_id) == int:
        entry = select_entries(select_request(table_name=DatabaseDataVariables.currencies_table_name,
                                              condition='cur_id={cur_id}'.format(cur_id=cur_id)))
        DatabaseDataVariables.cur_id = cur_id
    else:
        DatabaseDataVariables.cur_id = cur_id
        return None

    return entry


def initialize_database_table(table_name):
    """ Call methods to creating and filling the table is needed

    :param(str) table_name: name of the table
    """
    cur_date = DateTimeUtil.get_current_date()
    date_config_name = '{table_name}_date'.format(table_name=table_name)
    db_date = get_config(date_config_name)
    is_create = get_config('table_create')
    is_initialize = get_config('table_initialize')

    if db_date != cur_date or is_create:
        DatabaseFill.create_table(table_name)
    if db_date != cur_date or is_initialize:
        DatabaseFill.initialize_table(table_name)
        set_config(key=date_config_name, value=cur_date)
