*** Settings ***
Documentation    Test cases for testing the service for getting dynamic of currency
Library  ../test_libraries/CustomAsserts.py
Library  ../test_libraries/Database.py
Library  ../test_libraries/Api.py
Library  ../test_libraries/Json.py
Library  ../test_libraries/TestData.py
Variables  ../../test_data/variables/DatabaseDataVariables.py
Variables  ../../test_data/variables/TestDataVariables.py
Variables  ../../framework/interface_utils/http/HttpStatusCodes.py

*** Test Cases ***
Check dynamic of currency service
    Initialize Database Table  ${dynamics_table_name}
    @{test_data_list}=  Get Test Data List  excel_sheet=${excel_sheet_dynamics}  sets_of_test_data=${dynamics_test_set_value}  data_one_set=${dynamics_values_in_set}
    :FOR  ${test_data}  IN  @{test_data_list}
    \  Check dynamic of currency response  ${test_data['cur_id']}  ${test_data['status_code']}  ${test_data['start_date']}  ${test_data['end_date']}  ${test_data['scheme_name']}
    Raise All Soft Asserts


*** Keywords ***
Check dynamic of currency response
    [Arguments]  ${cur_id}  ${expected_status_code}  ${start_date}  ${end_date}  ${scheme_name}
    ${cur_db}=  Get Dynamic Currency From Database  ${cur_id}  ${start_date}  ${end_date}
    ${cur_api_response}=  Get Dynamic Currency From Api  ${start_date}  ${end_date}

    Soft Should Be Equal  ${expected_status_code}  ${cur_api_response.status_code}  Checking status code of the dynamics response (cur_id=${cur_id}, start_date=${start_date}, end_date=${end_date})
    Return From Keyword If  ${expected_status_code}==${bad_request_status_code}

    ${is_valid}=  Validate Json Scheme  scheme_name=${scheme_name}  json_file=${cur_api_response.json()}
    Should Be True  ${is_valid}  Checking that response json scheme is correct

    ${cur_api}=  Convert Api Date To Datetime  ${cur_api_response.json()}
    Should Be Equal Values Of List Dictionaries  ${cur_db}  ${cur_api}  Checking that response values of json is correct
