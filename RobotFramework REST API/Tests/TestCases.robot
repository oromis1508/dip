*** Settings ***
Resource  ../resources/Keywords.robot

*** Test Cases ***
Test Stream Method
    ${response}=  Use Method Stream To Create Streams  ${number_of_streams}
    Should Be Equal  ${response.text.count('\n')}  ${number_of_streams}  Check the number of strings in the response
    Should Be Equal  ${response.status_code}  ${ok_status_code}  Check the web service method of creating streams status code

Test Get Method With Arguments
    ${params}=  Create Dictionary  ${parameter name}=${parameter value}
    ${response}=  Use Method Get With Args   params=${params}
    Should Be Equal  ${response.json()['args']}   ${params}  Check parameters of the response of the get web service method
    Should Be Equal  ${response.status_code}  ${ok_status_code}  Check status code of the get method of the web service

Login Into The Service
    [Template]  Login Into The Service
    #user       #password      #status code
    user        passwd         ${ok_status_code}
    unvalid     unvalid        ${unauthorized_status_code}

