from test_data.variables import DatabaseData, WebServiceMethods
from framework.interface_utils.HttpUtil import get_request


def get_dynamic_currency_from_api(start_date, end_date):
    params = {'startDate': start_date, 'endDate': end_date}

    return get_request(url='{dyn_url}/{cur_id}'.format(dyn_url=WebServiceMethods.get_dynamic,
                                                       cur_id=DatabaseData.cur_id),
                       params=params)


def get_currency_info_from_api_request():
    response = get_request('{cur_url}/{cur_id}'.format(cur_url=WebServiceMethods.get_currencies,
                                                       cur_id=DatabaseData.cur_id))
    return response
