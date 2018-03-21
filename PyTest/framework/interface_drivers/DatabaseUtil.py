import sqlite3

import os


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
        db_path = os.path.join(base_dir, '../..', database_name)
        DatabaseUtil.conn = sqlite3.connect(db_path)
        DatabaseUtil.conn.row_factory = DatabaseUtil.__dict_factory

    @staticmethod
    def send_request(request):
        cursor = DatabaseUtil.conn.cursor()
        cursor.execute(request)
        return cursor.fetchone()

    @staticmethod
    def close_connection():
        DatabaseUtil.conn.close()

    @staticmethod
    def select_request(table_name, fields, condition):
        return 'SELECT {} FROM {} WHERE {}'.format(fields, table_name, condition)
