import cx_Oracle as cx_Oracle

from framework.interface_utils.test_utils.ConfigReader import get_config


def make_dict_factory(cursor):
    """ Method for setting return type of database responses is dictionary

    :param cursor: object to work with the database
    :return: method to create dictionary from the database response
    """
    column_names = [d[0] for d in cursor.description]

    def create_row(*args):
        return dict(zip(column_names, args))

    return create_row


def connect():
    """ Method to getting connection with database.
        Database connection string is in configuration file in resources
    :return: connection object
    """
    conn = cx_Oracle.connect(get_config('database_connect_string'), encoding='UTF8')
    return conn


def select_entries(request, is_one_entry=True):
    """ Used if select request to database returns one entry

    :param is_one_entry: flag showing: is select one entry (default=True)
    :param(str) request: select request. Can use the SqlRequests library.
    :return: dictionary of list of dictionaries with the database data
    """
    cursor = connect().cursor()
    cursor.execute(request)
    cursor.rowfactory = make_dict_factory(cursor)
    if is_one_entry:
        result = cursor.fetchone()
    else:
        result = cursor.fetchall()
    cursor.close()
    return result
