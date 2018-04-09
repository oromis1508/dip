*** Settings ***
Documentation    Test cases for testing the service for getting currency info
Library  ../test_libraries/CustomAsserts.py
Library  ../test_libraries/Database.py
Library  ../test_libraries/Api.py
Library  ../test_libraries/Json.py
Library  ../test_libraries/TestData.py
Variables  ../../test_data/variables/DatabaseDataVariables.py
Variables  ../../test_data/variables/TestDataVariables.py
Variables  ../../framework/interface_utils/http/HttpStatusCodes.py

*** Test Cases ***
Check currency info service
    Initialize Database Table  ${currencies_table_name}
    @{test_data_list}=  Get Test Data List  excel_sheet=${excel_sheet_currencies}  sets_of_test_data=${currencies_test_set_value}  data_one_set=${currencies_values_in_set}
    :FOR  ${test_data}  IN  @{test_data_list}
    \  Check currency info response  ${test_data['cur_id']}  ${test_data['status_code']}  ${test_data['scheme_name']}
    Raise All Soft Asserts


*** Keywords ***
Check currency info response
    [Arguments]  ${cur_id}  ${expected_status_code}  ${scheme_name}
    ${cur_id}  Format Currency Id  ${cur_id}
    ${cur_api_response}=  Get Currency Info From Api Request  ${cur_id}

    Soft Should Be Equal  ${expected_status_code}  ${cur_api_response.status_code}  Checking api response status code (cur_id=${cur_id})
    Return From Keyword If  ${expected_status_code}!=${ok_status_code}

    ${cur_db}=  Get Currency Info From Database  ${cur_id}

    ${is_valid}=  Validate Json Scheme  scheme_name=${scheme_name}  json_file=${cur_api_response.json()}
    Should Be True  ${is_valid}  Checking is api response json scheme is valid

    Should Be Equal Ignore Difference None And Empty String  ${cur_db}  ${cur_api_response.json()}  Checking if currency info of api and database is equal
