using System;

namespace BusinessLayer.Model.Models
{
    public class CreateUpdateEmployeeRequest
    {
        public string SiteId { get; set; }
        public string CompanyCode { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Occupation { get; set; }
        public string EmployeeStatus { get; set; }
        public string EmailAddress { get; set; }
        public string Phone { get; set; }
    }
}
