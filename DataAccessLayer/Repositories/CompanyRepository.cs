using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataAccessLayer.Model.Interfaces;
using DataAccessLayer.Model.Models;
using Newtonsoft.Json;

namespace DataAccessLayer.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
	    private readonly IDbWrapper<Company> _companyDbWrapper;

	    public CompanyRepository(IDbWrapper<Company> companyDbWrapper)
	    {
		    _companyDbWrapper = companyDbWrapper;
        }

        public IEnumerable<Company> GetAll()
        {
            return _companyDbWrapper.FindAll();
        }

        public Company GetByCode(string companyCode)
        {
            return _companyDbWrapper.Find(t => t.CompanyCode.Equals(companyCode))?.FirstOrDefault();
        }

        public Company SaveCompany(Company company)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
            if (itemRepo !=null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return _companyDbWrapper.Update(itemRepo);
            }

            return _companyDbWrapper.Insert(company);
        }
        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyDbWrapper.FindAllAsync();
        }

        public async Task<Company> GetByCodeAsync(string companyCode)
        {
            var result = await _companyDbWrapper.FindAsync(t => t.CompanyCode.Equals(companyCode));
            return result.FirstOrDefault();
        }

        public async Task<Company> SaveCompanyAsync(Company company)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(company.SiteId) && t.CompanyCode.Equals(company.CompanyCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                itemRepo.CompanyName = company.CompanyName;
                itemRepo.AddressLine1 = company.AddressLine1;
                itemRepo.AddressLine2 = company.AddressLine2;
                itemRepo.AddressLine3 = company.AddressLine3;
                itemRepo.Country = company.Country;
                itemRepo.EquipmentCompanyCode = company.EquipmentCompanyCode;
                itemRepo.FaxNumber = company.FaxNumber;
                itemRepo.PhoneNumber = company.PhoneNumber;
                itemRepo.PostalZipCode = company.PostalZipCode;
                itemRepo.LastModified = company.LastModified;
                return await _companyDbWrapper.UpdateAsync(itemRepo);
            }

            return await _companyDbWrapper.InsertAsync(company);
        }

        public async Task<Company> UpdateCompanyAsync(Company company)
        {

            var result = await _companyDbWrapper.UpdateAsync(company);
            return result;
        }
        public async Task DeleteCompanyAsync(DataEntity key)
        {
            var itemRepo = _companyDbWrapper.Find(t =>
                t.SiteId.Equals(key.SiteId) && t.CompanyCode.Equals(key.CompanyCode))?.FirstOrDefault();
            if (itemRepo != null)
            {
                await _companyDbWrapper.DeleteAsync(t =>t.SiteId.Equals(key.SiteId) && t.CompanyCode.Equals(key.CompanyCode));
            }
            else
            {
                throw new KeyNotFoundException($"Resource to delete not found: {JsonConvert.SerializeObject(key)}");
            }
        }

        public async Task<Company> GetCompanyAsync(Expression<Func<Company, bool>> expression)
        {
            var result = await _companyDbWrapper.FindAsync(expression);
            return result?.FirstOrDefault();
        }
    }
}
