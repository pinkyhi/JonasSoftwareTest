using AutoMapper;
using BusinessLayer.Model.Models;
using BusinessLayer.Model.Models.ViewModels;
using WebApi.Models;
using WebApi.Models.Responses;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>();
            CreateMap<BaseDto, BaseInfo>();
            CreateMap<EmployeeIndexInfo, EmployeeIndexDto>();
            CreateMap<EmployeeIndexDto, EmployeeIndexInfo>();
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<CompanyDto, CompanyInfo>();
            CreateMap<EmployeeInfo, EmployeeDto>();
            CreateMap<EmployeeDto, EmployeeInfo>();
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();

            CreateMap<EmployeeViewModel, EmployeeResponse>();
        }
    }
}