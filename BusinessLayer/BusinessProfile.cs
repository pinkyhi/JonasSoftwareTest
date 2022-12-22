using AutoMapper;
using BusinessLayer.Model.Models;
using BusinessLayer.Model.Models.ViewModels;
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
            CreateMap<EmployeeIndex, EmployeeIndexInfo>();
            CreateMap<EmployeeIndexInfo, EmployeeIndex>();
            CreateMap<Company, CompanyInfo>();
            CreateMap<CompanyInfo, Company>();
            CreateMap<Employee, EmployeeInfo>();
            CreateMap<EmployeeInfo, Employee>();
            CreateMap<ArSubledger, ArSubledgerInfo>();

            CreateMap<Employee, EmployeeViewModel>()
                .ForMember(x => x.LastModifiedDateTime, y => y.MapFrom(z => z.LastModified))
                .ForMember(x => x.PhoneNumber, y => y.MapFrom(z => z.Phone))
                .ForMember(x => x.OccupationName, y => y.MapFrom(z => z.Occupation));

        }
    }

}