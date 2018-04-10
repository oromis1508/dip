def select_request(table_name, condition='', fields='*'):
    """ Generates the sql select query

    :param(str) table_name: from table select
    :param(str) condition: condition for search entries
    :param fields: needed fields from the table (default - all fields)
    :return: the query string
    """
    request = 'SELECT {fields} FROM {table_name}'.format(fields=fields, table_name=table_name)
    if condition:
        request += ' WHERE {condition}'.format(condition=condition)
    return request


def drop_table_request(table_name):
    """ Generates the sql query for delete table from database with all entries in it

    :param(str) table_name: name of the table to delete
    :return: the query string
    """
    return 'DROP TABLE {table_name}'.format(table_name=table_name)


def insert_request(table_name, values, table_columns=''):
    """ Generates the sql query for inserting data to the table

    :param table_columns: parameter needed if not in all columns insert values(default: '')
    Example: '(column_one, column_two)'

    :param(str) values: inserted values into the table, are separated by commas
    :param(str) table_name: name of the table to delete
    :return: the query string
    """
    return 'insert into {table_name} {table_columns} values ({values})'.format(table_name=table_name,
                                                                               values=values,
                                                                               table_columns=table_columns)


def create_table(table_name, column_info):
    """ Generates the sql query for create table

    :param(str) column_info: table names with types
    Example: 'col_one number(10), col_two varchar(15)'

    :param(str) table_name: name of the table to create
    :return: the query string
    """
    return 'create table {table_name} ({column_info})'.format(table_name=table_name,
                                                              column_info=column_info)


def select_random_entry(table_name, entries_number):
    """ Generates the sql select query for getting random entries

    :param(str or int) entries_number: number of the random entries needed
    :param(str) table_name: name of the database table where searched entry
    :return: the query string
    """
    return select_request(table_name='(select * from {table_name} order by dbms_random.value)'.format(table_name=table_name),
                          condition='ROWNUM = {entries_number}'.format(entries_number=entries_number))
