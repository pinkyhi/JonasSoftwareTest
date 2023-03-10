using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Model.Models;

namespace DataAccessLayer.Model.Interfaces
{
    public interface ICompanyRepository
    {
        IEnumerable<Company> GetAll();
        Company GetByCode(string companyCode);
        Company SaveCompany(Company company);
        Task<IEnumerable<Company>> GetAllAsync();
        Task<Company> GetByCodeAsync(string companyCode);
        Task<Company> SaveCompanyAsync(Company company);
        Task<Company> UpdateCompanyAsync(Company company);
        Task<Company> GetCompanyAsync(Expression<Func<Company, bool>> expression);
        Task DeleteCompanyAsync(DataEntity key);
    }
}
