import pytest

from framework.interface_drivers.DatabaseUtil import DatabaseUtil
from framework.interface_drivers.Logger import Logger


@pytest.fixture(scope='module')
def database_fixture():
    DatabaseUtil.connect_database('resources\database')
    yield
    DatabaseUtil.close_connection()

def test_database_util(database_fixture):
    response = DatabaseUtil.send_request(DatabaseUtil.select_request('test_table', '*', 'Id=1'))
    try:
        assert response['Id'] == 1 and bool(response['param_one']) == True and response['param_two'] == 'string'
        Logger.add_log(message='database request send successfully')
    except AssertionError:
        Logger.add_log(message='database request send unsuccessfully')
        raise
