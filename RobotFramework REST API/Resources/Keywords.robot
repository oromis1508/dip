*** Settings ***
Library  ../Libraries/HttpBinMethods.py

*** Keywords ***
Check response code
    [Arguments]  ${code}
    ${current status code}  Get status code
    Should be equal  ${current status code}  ${code}

Login into the service
    [Arguments]  ${user}  ${password}  ${expected_user}  ${status_code}
    ${authorized user}  Authorization by method base auth  ${user}  ${password}
    SHOULD BE EQUAL  ${authorized user}  ${expected_user}
        Check response code  ${status_code}

*** Variables ***
${parameter_name}               arg
${parameter_value}              value
${ok_status_code}               ${200}
${unauthorized_status_code}     ${401}
${number_of_streams}            ${10}
