using BusinessLayer.Model.Models;
using BusinessLayer.Model.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Model.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync();
        Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode); 
        Task<EmployeeViewModel> GetEmployeeByKeyAsync(EmployeeIndexInfo key); 
        Task<EmployeeInfo> CreateEmployeeAsync(EmployeeInfo employeeInfo);
        Task<EmployeeInfo> PutEmployeeAsync(EmployeeInfo employeeInfo);
        Task DeleteEmployeeAsync(EmployeeIndexInfo key);
    }
}
