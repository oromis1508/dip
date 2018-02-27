Feature: CreateEmployee
	The feature checks for the correct work of the web service
	for calculation salary of employees. The feature checks 
	the function of adding new employee with valid and unvalid data


	#Given Connect to the database with settings
	#| databaseIp   | databasePort | databaseName | databaseUser | databasePassword |
	#| A1QA-TASKS-1 | 1433         | ServiceDB    | TESTSRV      | 123qweASD        |

Scenario: Create the employee with valid data
	When I create new employee on the web service with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |
	Then The server sent the response 'Данные добавлены успешно' in the tag 'AddNewEmployeeResult'
	When I send request to the database 'Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.id=''Employee.Id'
	
	#FirstName and LastName of the Employee may be swapped
	Then The response data match the employee with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |

	
Scenario Outline: Create the employee with unvalid data

	When I create new employee on the web service with the data
	| Id		| PrivateId		| FirstName		| LastName		| MiddleName	| Experiense	| ProfessionId		|
	| <Id>      | <PrivateId>	| <FirstName>	| <LastName>	| <MiddleName>	| <Experiense>	| <ProfessionId>	|
	Then The server sent the response 'Указаны не все параметры' in the tag 'AddNewEmployeeResult'
	When I send request to the database 'Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.id=''Employee.Id'	
	Then The database response not contains entries

	Examples: 
	| Id        | PrivateId  | FirstName  | LastName    | MiddleName | Experiense | ProfessionId |
	| 636549866 |            | Каждыг     | Азмунд      | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   |		      | Азмунд      | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     |		        | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      |			 | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      | Калгырович |            | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      | Калгырович | 6          |              |
	|		    | 5he6yeue   | Каждыг     | Азмунд      | Калгырович | 6          | 5            |	

