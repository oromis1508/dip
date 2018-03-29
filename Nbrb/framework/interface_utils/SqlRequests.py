def select_request(table_name, condition, fields='*'):
    return 'SELECT {fields} FROM {table_name} WHERE {condition}'.format(fields=fields,
                                                                        table_name=table_name,
                                                                        condition=condition)


def drop_table_request(table_name):
    return 'DROP TABLE {table_name}'.format(table_name=table_name)


def insert_request(table_name, values, table_columns=''):
    return 'insert into {table_name} {table_columns} values ({values})'.format(table_name=table_name,
                                                                               values=values,
                                                                               table_columns=table_columns)


def create_table(table_name, column_info):
    return 'create table {table_name} ({column_info})'.format(table_name=table_name,
                                                              column_info=column_info)
