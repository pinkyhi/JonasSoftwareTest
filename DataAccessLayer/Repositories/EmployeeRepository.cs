using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbWrapper<Employee> _employeeDbWrapper;
        private readonly IDbWrapper<Company> _companyDbWrapper;

        public EmployeeRepository(IDbWrapper<Employee> employeeDbWrapper, IDbWrapper<Company> companyDbWrapper)
        {
            _employeeDbWrapper = employeeDbWrapper;
            _companyDbWrapper = companyDbWrapper;
        }

        public IEnumerable<Employee> GetAll()
        {
            return _employeeDbWrapper.FindAll();
        }

        public Employee GetByCode(string employeeCode)
        {
            return _employeeDbWrapper.Find(t => t.EmployeeCode.Equals(employeeCode))?.FirstOrDefault();
        }

        public Employee SaveEmployee(Employee employee)
        {
            var itemRepo = _employeeDbWrapper.Find(t => t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode) && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return _employeeDbWrapper.Update(itemRepo);
            }

            return _employeeDbWrapper.Insert(employee);
        }
        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeDbWrapper.FindAllAsync();
        }

        public async Task<Employee> GetByCodeAsync(string employeeCode)
        {
            var result = await _employeeDbWrapper.FindAsync(t => t.EmployeeCode.Equals(employeeCode));
            return result.FirstOrDefault();
        }

        public async Task<Employee> SaveEmployeeAsync(Employee employee)
        {
            if (!CheckCompanyKey(employee))
            {
                throw new ArgumentException("Company key is unknown");
            }
            var itemRepo = _employeeDbWrapper.Find(t => t.SiteId.Equals(employee.SiteId) && t.CompanyCode.Equals(employee.CompanyCode) && t.EmployeeCode.Equals(employee.EmployeeCode))?.FirstOrDefault();

            if (itemRepo != null)
            {
                itemRepo.EmployeeName = employee.EmployeeName;
                itemRepo.Occupation = employee.Occupation;
                itemRepo.EmployeeStatus = employee.EmployeeStatus;
                itemRepo.EmailAddress = employee.EmailAddress;
                itemRepo.Phone = employee.Phone;
                itemRepo.LastModified = employee.LastModified;
                return await _employeeDbWrapper.UpdateAsync(itemRepo);
            }

            return await _employeeDbWrapper.InsertAsync(employee);
        }

        public async Task<Employee> UpdateEmployeeAsync(Employee employee)
        {
            if (!CheckCompanyKey(employee))
            {
                throw new ArgumentException("Company key is unknown");
            }

            var result = await _employeeDbWrapper.UpdateAsync(employee);
            return result;
        }
        public async Task DeleteEmployeeAsync(EmployeeIndex key)
        {
            var itemRepo = _employeeDbWrapper.Find(t => t.SiteId.Equals(key.SiteId) && t.CompanyCode.Equals(key.CompanyCode) && t.EmployeeCode.Equals(key.EmployeeCode))?.FirstOrDefault();

            if (itemRepo != null)
            {
                await _employeeDbWrapper.DeleteAsync(t => t.SiteId.Equals(key.SiteId) && t.CompanyCode.Equals(key.CompanyCode) && t.EmployeeCode.Equals(key.EmployeeCode));
            }
            else
            {
                throw new KeyNotFoundException($"Resource to delete not found: \n{JsonConvert.SerializeObject(key)}");
            }
        }

        public async Task<Employee> GetEmployeeAsync(Expression<Func<Employee, bool>> expression)
        {
            var employee = await _employeeDbWrapper.FindAsync(expression);
            return employee?.FirstOrDefault();
        }

        private bool CheckCompanyKey(Employee employee)
        {
            var company = _companyDbWrapper.Find(x => x.SiteId.Equals(employee.SiteId) && x.CompanyCode.Equals(employee.SiteId)).FirstOrDefault();
            return company != null;

        }
    }
}
