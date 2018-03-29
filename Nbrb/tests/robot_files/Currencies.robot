*** Settings ***
Documentation    Test cases for testing the method for getting currency info
Library  ../test_libraries/Database.py
Library  ../test_libraries/Api.py
Library  ../test_libraries/Json.py
Library  ../preconditions/DatabaseFill.py
Variables  ../../test_data/variables/DatabaseData.py
Variables  ../../test_data/variables/TestData.py

*** Test Cases ***
Test
    Create Table  ${currencies_table_name}
    Initialize Currencies Table
    ${cur_db}=  Get Currency Info From Database
    ${cur_api_response}=  Get Currency Info From Api Request
    ${is_valid}=  Validate Json Scheme  scheme_name=currency_scheme  json_file=${cur_api_response.json()}

    ${db_values}=  Convert To String  ${cur_db.values()}
    ${api_values}=  Convert To String  ${cur_api_response.json().values()}

    Should Be Equal  ${valid_response_code}  ${cur_api_response.status_code}
    Should Be Equal  ${db_values}  ${api_values}
    Should Be True  ${is_valid}
