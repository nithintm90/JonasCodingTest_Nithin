Chuang's Notes: 
- api services exported as a postman collection json file: JonasTest.postman_collection
- As the in memory database requires singleton implementation, i cannot get Ninject working for that so I used Simple Injector. 
- Any api request and exceptions handled are automatically logged inside in memory database with full details using filter, use /api/log to check all logs 
- Made business exception to track business exceptions like empty employee name etc. 

1) Implement rest of Company controller functions, all the way down to data access layer

2) Change all Company controller functions to be asynchronous

3) Create new repository to get and save employee information with the following data model properties:

* string SiteId,
* string CompanyCode,
* string EmployeeCode,
* string EmployeeName,
* string Occupation,
* string EmployeeStatus,
* string EmailAddress,
* string Phone,
* DateTime LastModified

4) Create employee controller to get the following properties for client side:

* string EmployeeCode,
* string EmployeeName,
* string CompanyName,
* string OccupationName,
* string EmployeeStatus,
* string EmailAddress,
* string PhoneNumber,
* string LastModifiedDateTime

5) Add logger to solution and add proper error handling
