*** Settings ***
Library  ../libraries/HttpBinMethods.py
Variables  ../utils/interface_utils/http/HttpCodes.py

*** Variables ***
${parameter_name}               arg
${parameter_value}              value
${number_of_streams}            ${10}


*** Keywords ***
Login Into The Service
    [Arguments]  ${user}  ${password}  ${status_code}
    ${response}=  Authorization By Method Base Auth  user=${user}  password=${password}
    Should Be Equal  ${response.status_code}  ${status_code}  Check the login status code
    Return From Keyword If  ${response.status_code}  ${unauthorized_status_code}
    Should Be Equal  ${response.json()['user']}  ${user}  Check the login username
