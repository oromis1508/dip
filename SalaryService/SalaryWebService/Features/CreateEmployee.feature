Feature: CreateEmployee
	The feature checks for the correct work of the web service
	for calculation salary of employees. The feature checks 
	the function of adding new employee with valid and unvalid data
	The feature checks 
	the function of updating added employee by add with new data but old Id
	The feature checks 
	the function of search employee by privateId
	The feature checks 
	the function of calculating the salary of the added employee

Scenario: Create the employee with valid data
	When I create employee on the web service with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |
	Then The server sent the response 'Данные добавлены успешно' in the tag 'AddNewEmployeeResult'
	When I send request to the database to search with the parameters
	| SeachFields                                                            | TableName | SeachCriteria |
	| id, private_id, first_name, last_name, middle_name, exp, Profession_id | employees | id=666        |
	
	#FirstName and LastName of the Employee may be swapped
	Then The data of the database response match the employee with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |

	
Scenario Outline: Create the employee with unvalid data
	When I create employee on the web service with the data
	| Id		| PrivateId		| FirstName		| LastName		| MiddleName	| Experiense	| ProfessionId		|
	| <Id>      | <PrivateId>	| <FirstName>	| <LastName>	| <MiddleName>	| <Experiense>	| <ProfessionId>	|
	Then The server sent the response 'Указаны не все параметры' in the tag 'AddNewEmployeeResult'
	When I send request to the database to search with the parameters
	| SeachFields                                                            | TableName | SeachCriteria   |
	| id, private_id, first_name, last_name, middle_name, exp, Profession_id | employees | <SeachCriteria> |
	Then The database response not contains entries

	Examples: 
	| Id        | PrivateId | FirstName | LastName | MiddleName | Experiense | ProfessionId | SeachCriteria         |
	| 636549866 |           | Каждыг    | Азмунд   | Калгырович | 6          | 5            | id=636549866          |
	| 636549866 | 5he6yeue  |           | Азмунд   | Калгырович | 6          | 5            | id=636549866          |
	| 636549866 | 5he6yeue  | Каждыг    |          | Калгырович | 6          | 5            | id=636549866          |
	| 636549866 | 5he6yeue  | Каждыг    | Азмунд   |            | 6          | 5            | id=636549866          |
	| 636549866 | 5he6yeue  | Каждыг    | Азмунд   | Калгырович |            | 5            | id=636549866          |
	| 636549866 | 5he6yeue  | Каждыг    | Азмунд   | Калгырович | 6          |              | id=636549866          |
	|           | 5he6yeue  | Каждыг    | Азмунд   | Калгырович | 6          | 5            | private_id='5he6yeue' |
