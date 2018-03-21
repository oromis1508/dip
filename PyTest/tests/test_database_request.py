import pytest

from framework.interface_drivers.DatabaseUtil import DatabaseUtil


@pytest.fixture(scope='module')
def database_fixture():
    DatabaseUtil.connect_database('resources\database')
    yield
    DatabaseUtil.close_connection()

def test_database_util(database_fixture):
    response = DatabaseUtil.send_request(DatabaseUtil.select_request('test_table', '*', 'Id=1'))
    assert response['Id'] == 1 and bool(response['param_one']) == True and response['param_two'] == 'string'
