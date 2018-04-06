import pytest

from framework.interface_drivers.database.DatabaseUtil import DatabaseUtil
from framework.interface_drivers.logger.Logger import Logger


@pytest.fixture(scope='module',
                params=[(1, True, 'string')])
def database_fixture(request):
    DatabaseUtil.connect_database('resources\database')
    yield request.param
    DatabaseUtil.close_connection()


def test_database_util(database_fixture):
    response = DatabaseUtil.send_request(request=DatabaseUtil.select_request('test_table', '*', 'Id=1'))
    expect_id, expect_param_one, expect_param_two = database_fixture
    try:
        assert response['Id'] == expect_id and \
               bool(response['param_one']) == expect_param_one and \
               response['param_two'] == expect_param_two
        Logger.add_log(message='database request send successfully')
    except AssertionError:
        Logger.add_log(message='database request send unsuccessfully')
        raise
