using BusinessLayer.Model.Interfaces;
using System.Collections.Generic;
using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Interfaces;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;
using System;

namespace BusinessLayer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyInfo>> GetAllCompaniesAsync()
        {
            var result = await _companyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CompanyInfo>>(result);
        }

        public async Task<CompanyInfo> GetCompanyByCodeAsync(string companyCode)
        {
            var result = await _companyRepository.GetByCodeAsync(companyCode);
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task<CompanyInfo> CreateCompanyAsync(CompanyInfo companyInfo)
        {
            companyInfo.LastModified = DateTime.Now;
            var result = await _companyRepository.SaveCompanyAsync(_mapper.Map<Company>(companyInfo));
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task<CompanyInfo> PutCompanyAsync(CompanyInfo companyInfo)
        {
            companyInfo.LastModified = DateTime.Now;
            var result = await _companyRepository.UpdateCompanyAsync(_mapper.Map<Company>(companyInfo));
            return _mapper.Map<CompanyInfo>(result);
        }

        public async Task DeleteCompanyAsync(BaseInfo key)
        {
            await _companyRepository.DeleteCompanyAsync(_mapper.Map<DataEntity>(key));
        }
    }
}
