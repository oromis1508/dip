Feature: UpdateEmployee
	The feature checks for the correct work of the web service
	for calculation salary of employees. The feature checks 
	the function of updating added employee by add with new data but old Id

@mytag
Scenario: Update the created employee
	Given Created the employee on the web service with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |
	When I update the created employee on the web service by the data
	| Id  | PrivateId  | FirstName   | LastName    | MiddleName | Experiense | ProfessionId |
	| 666 | 10t500id   | Узбекен     | Рудольф     | Васгенович | 3          | 6            |
		And I send request to the database 'Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.id=''Employee.Id'
	Then The response data match the Employee with the data
	| Id  | PrivateId  | FirstName   | LastName    | MiddleName | Experiense | ProfessionId |
	| 666 | 10t500id   | Узбекен     | Рудольф     | Васгенович | 3          | 6            |

