Feature: UpdateEmployee
	The feature checks for the correct work of the web service
	for calculation salary of employees. The feature checks 
	the function of updating added employee by add with new data but old Id

Scenario: Update the created employee
	Given The employee created on the web service with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |
	When I update employee on the web service with the data
	| Id  | PrivateId  | FirstName   | LastName    | MiddleName | Experiense | ProfessionId |
	| 666 | 10t500id   | Узбекен     | Рудольф     | Васгенович | 3          | 6            |
		And I send request to the database to search with the parameters
		| SeachFields                                                            | TableName | SeachCriteria |
		| id, private_id, first_name, last_name, middle_name, exp, Profession_id | employees | id=666        |
	Then The data of the database response match the employee with the data
	| Id  | PrivateId  | FirstName   | LastName    | MiddleName | Experiense | ProfessionId |
	| 666 | 10t500id   | Узбекен     | Рудольф     | Васгенович | 3          | 6            |
