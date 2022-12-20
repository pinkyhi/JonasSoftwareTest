﻿using AutoMapper;
using BusinessLayer.Model.Models;
using WebApi.Models;

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
            CreateMap<CompanyInfo, CompanyDto>();
            CreateMap<CompanyDto, CompanyInfo>();
            CreateMap<ArSubledgerInfo, ArSubledgerDto>();
        }
    }
}