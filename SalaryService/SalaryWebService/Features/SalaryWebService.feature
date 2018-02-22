Feature: SalaryWebService

Scenario: Check functions salary web service
	Given Connect to the database with settings
	| databaseIp   | databasePort | databaseName | databaseUser | databasePassword |
	| A1QA-TASKS-1 | 1433         | ServiceDB    | TESTSRV      | 123qweASD        |
	When New Employee created on the web service with data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |
	Then The server sent response 'Данные добавлены успешно' in the tag 'AddNewEmployeeResult'
	When Request sent to the database 'Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.id=''Employee.Id'
	
	#FirstName and LastName of the Employee may be swapped
	Then The response data match the Employee created in the previous step
	
	When The created Employee was updated on the web service by data
	| Id  | PrivateId  | FirstName   | LastName    | MiddleName | Experiense | ProfessionId |
	| 666 | 10t500id   | Узбекен     | Рудольф     | Васгенович | 3          | 6            |
	And Request sent to the database 'Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.id=''Employee.Id'
	Then The response data match the Employee created in the previous step
	When New Employees without one of the parameters created on the web service with data
	| Id        | PrivateId  | FirstName  | LastName    | MiddleName | Experiense | ProfessionId |
	| 636549866 |            | Каждыг     | Азмунд      | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   |		      | Азмунд      | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     |		        | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      |			 | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      | Калгырович |            | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      | Калгырович | 6          |              |
	|		    | 5he6yeue   | Каждыг     | Азмунд      | Калгырович | 6          | 5            |	
	Then The server sent response 'Указаны не все параметры' in the tag 'AddNewEmployeeResult'
	When Employee searched on the web service by 'Employee.PrivateId'
	Then The web service response tags named and placed as
	| private_id | last_name | first_name | middle_name | exp | prof_name | salary_amount |

	#FirstName and LastName of the Employee may be swapped
	And Data in web service response match created Employee in the begin scenario

	When Request to getting employee salary was sended to the web service
	| WorkDays | SickDays | OverDays | Month   | IsPrivilegy |
	| 21         | 0        | 0        | 05-2019 | 0           |
	Then The server sent response '1620' in the tag 'GetEmpSalaryResult'

