using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Models
{
    public class EmployeeDto : BaseDto
    {
        string EmployeeCode { get; set; }
        string EmployeeName { get; set; }
        string Occupation { get; set; }
        string EmployeeStatus { get; set; }
        string EmailAddress { get; set; }
        string Phone { get; set; }
        DateTime LastModified { get; set; }
    }
}