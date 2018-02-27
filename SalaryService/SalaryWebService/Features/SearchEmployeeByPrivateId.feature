Feature: SearchEmployeeByPrivateId
	The feature checks for the correct work of the web service
	for calculation salary of employees. The feature checks 
	the function of search employee by privateId

Scenario: Search the employee by privateId
	Given The created employee on the web service with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |

	When I search the employee on the web service by 'PrivateId'
	Then The web service response tags named and placed as
	| Tags          |
	| private_id    |
	| last_name     |
	| first_name    |
	| middle_name   |
	| exp           |
	| prof_name     |
	| salary_amount |

	#FirstName and LastName of the Employee may be swapped
	And Data in the web service response match the employee with the data
	| Id  | PrivateId | FirstName | LastName | MiddleName  | Experiense | ProfessionId |
	| 666 | 10500id   | Узбек     | Ашан     | Анатольевич | 8          | 3            |

