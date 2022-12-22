using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using BusinessLayer.Model.Models.ViewModels;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper _mapper;

        public EmployeeService(IEmployeeRepository employeeRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            this.companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<EmployeeInfo>> GetAllEmployeesAsync()
        {
            var result = await _employeeRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<EmployeeInfo>>(result);
        }

        public async Task<EmployeeInfo> GetEmployeeByCodeAsync(string employeeCode)
        {
            var result = await _employeeRepository.GetByCodeAsync(employeeCode);
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task<EmployeeViewModel> GetEmployeeByKeyAsync(EmployeeIndexInfo key)
        {
            var employee = await _employeeRepository.GetEmployeeAsync(x => x.SiteId.Equals(key.SiteId) && x.CompanyCode.Equals(key.CompanyCode) && x.EmployeeCode.Equals(key.EmployeeCode));
            if(employee == null)
            {
                return null;
            }
            var company = await companyRepository.GetCompanyAsync(x => x.SiteId.Equals(key.SiteId) && x.CompanyCode.Equals(key.CompanyCode));

            var viewModel = _mapper.Map<EmployeeViewModel>(employee);

            viewModel.CompanyName = company?.CompanyName;
            return viewModel;
        }


        public async Task<EmployeeInfo> CreateEmployeeAsync(EmployeeInfo employeeInfo)
        {
            employeeInfo.LastModified = DateTime.Now;
            var result = await _employeeRepository.SaveEmployeeAsync(_mapper.Map<Employee>(employeeInfo));
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task<EmployeeInfo> PutEmployeeAsync(EmployeeInfo employeeInfo)
        {
            employeeInfo.LastModified = DateTime.Now;
            var result = await _employeeRepository.UpdateEmployeeAsync(_mapper.Map<Employee>(employeeInfo));
            return _mapper.Map<EmployeeInfo>(result);
        }

        public async Task DeleteEmployeeAsync(EmployeeIndexInfo key)
        {
            await _employeeRepository.DeleteEmployeeAsync(_mapper.Map<EmployeeIndex>(key));
        }
    }
}
