using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using BusinessLayer.Model.Interfaces;
using BusinessLayer.Model.Models;
using Newtonsoft.Json;
using NLog;
using WebApi.Filters;
using WebApi.Models;

namespace WebApi.Controllers
{
    [NotImplExceptionFilter]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper, ILogger logger)
        {
            _companyService = companyService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var items = await _companyService.GetAllCompaniesAsync();
            if (items != null && items.Count() > 0)
            {
                return Ok(_mapper.Map<IEnumerable<CompanyDto>>(items));
            }
            else
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(string companyCode)
        {
            var item = await _companyService.GetCompanyByCodeAsync(companyCode);
            if (item == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(_mapper.Map<CompanyDto>(item));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]CompanyDto company)
        {
            var newCompany = await _companyService.CreateCompanyAsync(_mapper.Map<CompanyInfo>(company));
            if(newCompany == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
            {
                return Created(Request.RequestUri, _mapper.Map<CompanyDto>(newCompany));
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] CompanyDto company)
        {
            var updatedCompany = await _companyService.PutCompanyAsync(_mapper.Map<CompanyInfo>(company));
            if (updatedCompany == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(_mapper.Map<CompanyDto>(updatedCompany));
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(BaseDto baseDto)
        {
            await _companyService.DeleteCompanyAsync(_mapper.Map<BaseInfo>(baseDto));
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}