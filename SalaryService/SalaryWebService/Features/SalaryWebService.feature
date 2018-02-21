Feature: SalaryWebService

Scenario: Check functions salary web service
	Given Connect to the database
	When New Employee Created on the web service
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |
	Then The server sent response 'Данные добавлены успешно' in the tag 'AddNewEmployeeResult'
	When Request sent to the database 'Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.id=''Employee.Id'
	Then The response data match the Employee created in the previous step
	When New Employee Updated on the web service
	| Id  | PrivateId  | FirstName   | LastName    | MiddleName | Experiense | ProfessionId |
	| 666 | 10t500id   | Узбекен     | Рудольф     | Васгенович | 3          | 6            |
	And Request sent to the database 'Select em.id, em.private_id, em.first_name, em.last_name, em.middle_name, em.exp, em.Profession_id from employees em where em.id=''Employee.Id'
	Then The response data match the Employee created in the previous step
	When New Employee Created on the web service
	| Id        | PrivateId  | FirstName  | LastName    | MiddleName | Experiense | ProfessionId |
	|		    | 5he6yeue   | Каждыг     | Азмунд      | Калгырович | 6          | 5            |
	| 636549866 |            | Каждыг     | Азмунд      | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   |		      | Азмунд      | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     |		        | Калгырович | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      |			 | 6          | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      | Калгырович |            | 5            |
	| 636549866 | 5he6yeue   | Каждыг     | Азмунд      | Калгырович | 6          |              |
	Then The server sent response 'Указаны не все параметры' in the tag 'AddNewEmployeeResult'
	When Employee searched on the web service by 'Employee.PrivateId'
	Then The response match documentation
	And Data match Employee after update
	When 
