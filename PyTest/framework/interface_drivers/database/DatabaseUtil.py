import sqlite3
import os

from framework.interface_drivers.logger.Logger import Logger


class DatabaseUtil:
    conn = None

    @staticmethod
    def __dict_factory(cursor, row):
        d = {}
        for idx, col in enumerate(cursor.description):
            d[col[0]] = row[idx]
        return d

    @staticmethod
    def connect_database(database_name):
        base_dir = os.path.dirname(os.path.abspath(__file__))
        db_path = os.path.join(base_dir, '../../..', database_name)
        DatabaseUtil.conn = sqlite3.connect(db_path)
        DatabaseUtil.conn.row_factory = DatabaseUtil.__dict_factory
        Logger.add_log(message='connected to {database_name} database'.format(database_name=database_name))

    @staticmethod
    def send_request(request):
        cursor = DatabaseUtil.conn.cursor()
        cursor.execute(request)
        Logger.add_log(message='{request} request sent to database'.format(request=request))
        return cursor.fetchone()

    @staticmethod
    def close_connection():
        DatabaseUtil.conn.close()
        Logger.add_log(message='database connect closed')

    @staticmethod
    def select_request(table_name, fields, condition):
        return 'SELECT {fields} FROM {table_name} WHERE {condition}'.format(fields=fields,
                                                                            table_name=table_name,
                                                                            condition=condition)
