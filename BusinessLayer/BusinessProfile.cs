using AutoMapper;
using BusinessLayer.Model.Models;
using DataAccessLayer.Model.Models;

namespace BusinessLayer
{
    public class BusinessProfile : Profile
    {
        public BusinessProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<DataEntity, BaseInfo>();
            CreateMap<BaseInfo, DataEntity>();
            CreateMap<Company, CompanyInfo>();
            CreateMap<CompanyInfo, Company>();
            CreateMap<ArSubledger, ArSubledgerInfo>();
        }
    }

}