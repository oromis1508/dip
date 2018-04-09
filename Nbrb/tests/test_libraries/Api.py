from datetime import datetime

from test_data.variables import DatabaseDataVariables, WebServiceMethods
from framework.interface_utils.http.HttpUtil import get_request


def get_dynamic_currency_from_api(start_date, end_date):
    """ Call method to sending request to api method for getting dynamic of currency

    :param(str) start_date: parameter startDate for the request (format: 1990-01-31)
    :param(str) end_date: parameter endDate for the request (format: 1990-01-31)
    :return: the response object
    """
    params = {}

    if start_date != '-':
        params['startDate'] = start_date
    if end_date != '-':
        params['endDate'] = end_date

    return get_request(url='{dyn_url}/{cur_id}'.format(dyn_url=WebServiceMethods.get_dynamic,
                                                       cur_id=DatabaseDataVariables.cur_id),
                       params=params)


def get_currency_info_from_api_request():
    """ Call method to sending request to api method for getting currency info

    :return: the response object
    """
    response = get_request('{cur_url}/{cur_id}'.format(cur_url=WebServiceMethods.get_currencies,
                                                       cur_id=DatabaseDataVariables.cur_id))
    return response


def convert_api_date_to_datetime(response_list):
    """ Converted Api datetime in list of currency dynamic to datetime object

    :param(list of dict) response_list: the dynamic of the currency
    :return: changed list
    """
    for i in range(0, len(response_list)):
        response_list[i]['Date'] = datetime.strptime(response_list[i]['Date'], '%Y-%m-%dT%H:%M:%S')
    return response_list
