import sqlite3

class DatabaseUtil:

    conn = None

    @staticmethod
    def dict_factory(cursor, row):
        d = {}
        for idx, col in enumerate(cursor.description):
            d[col[0]] = row[idx]
        return d

    @staticmethod
    def connect_database(database_name):
        DatabaseUtil.conn = sqlite3.connect(database_name)
        DatabaseUtil.conn.row_factory = DatabaseUtil.dict_factory

    @staticmethod
    def send_request(request):
        cursor = DatabaseUtil.conn.cursor()
        cursor.execute(request)
        return cursor.fetchall()

    @staticmethod
    def close_connection():
        DatabaseUtil.conn.close()