*** Settings ***
Resource  ../Resources/Keywords.robot

*** Test Cases ***
Test stream method
    ${number of strings}  use method stream to create streams  ${number_of_streams}
    Should be equal  ${number of strings}  ${number_of_streams}
        Check response code  ${ok_status_code}

Test get method with arguments
    ${value of arg}  use method get with args  ${parameter name}  ${parameter value}
    Should be equal  ${value of arg}  ${parameter value}
        Check response code  ${ok_status_code}

Login into the service
    [Template]  Login into the service
    #user       #password       #expected user      #status code
    user        passwd          user                ${ok_status_code}
    unvalid     unvalid         unauthorized        ${unauthorized_status_code}

