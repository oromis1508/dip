Feature: CalculateEmployeeSalary
	The feature checks for the correct work of the web service
	for calculation salary of employees. The feature checks 
	the function of calculating the salary of the added employee

Scenario: Calculate the employee salary
	Given Created the employee on the web service with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |

	When I send request to getting the employee salary to the web service
	| WorkDays   | SickDays | OverDays | Month   | IsPrivilegy |
	| 21         | 0        | 0        | 05-2019 | 0           |
	Then The server sent the response '1620' in the tag 'GetEmpSalaryResult'
