__web_service_name = 'http://www.nbrb.by/API/ExRates'
get_currencies = '{web_service_name}/Currencies'.format(web_service_name=__web_service_name)
get_dynamic = '{web_service_name}/Rates/Dynamics'.format(web_service_name=__web_service_name)
