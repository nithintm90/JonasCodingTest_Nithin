using System;
using System.Collections.Generic;

namespace WebApi.Models
{
    // 3.Create new repository to get and save employee information with the following data model properties:
    public class EmployeeDto : BaseDto
    {
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public string OccupationName { get; set; }
        public string EmployeeStatus { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime LastModifiedDateTime { get; set; }
    }
}