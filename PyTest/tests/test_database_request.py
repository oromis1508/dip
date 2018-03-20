import pytest

from framework.interface_drivers.DatabaseUtil import DatabaseUtil


@pytest.fixture(scope='module')
def test_fixture():
    DatabaseUtil.connect_database('resources/database')
    yield
    DatabaseUtil.close_connection()

def test_database_util():
    response = DatabaseUtil.send_request(DatabaseUtil.select_request('test', '*', 'Id=1'))
    assert response['Id'] == 1 and response['param_one'] == True and response['param_two'] == 'string'