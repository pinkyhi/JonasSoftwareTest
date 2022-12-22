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
using WebApi.Models.Responses;

namespace WebApi.Controllers
{
    [NotImplExceptionFilter]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EmployeeController(IEmployeeService employeeService, IMapper mapper, ILogger logger)
        {
            _employeeService = employeeService;
            _mapper = mapper;
            _logger = logger;
        }
        // GET api/<controller>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var items = await _employeeService.GetAllEmployeesAsync();
            if(items != null && items.Count() > 0)
            {
                return Ok(_mapper.Map<IEnumerable<EmployeeDto>>(items));
            }
            else
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
        }

        // GET api/<controller>/5
        [HttpGet]
        public async Task<IHttpActionResult> Get(string employeeCode)
        {
            var item = await _employeeService.GetEmployeeByCodeAsync(employeeCode);
            if (item == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(_mapper.Map<EmployeeDto>(item));
            }
        }

        // GET api/<controller>?employeeCode=x&siteId=x&companyCode=x
        // Assessment endpoint
        [HttpGet]
        public async Task<IHttpActionResult> GetViewModel([FromUri] string employeeCode, [FromUri] string siteId, [FromUri] string companyCode)
        {
            EmployeeIndexDto key = new EmployeeIndexDto() { CompanyCode = companyCode, EmployeeCode = employeeCode, SiteId = siteId};
            var item = await _employeeService.GetEmployeeByKeyAsync(_mapper.Map<EmployeeIndexInfo>(key));
            if (item == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(_mapper.Map<EmployeeResponse>(item));
            }
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody] EmployeeDto employee)
        {
            var newEmployee = await _employeeService.CreateEmployeeAsync(_mapper.Map<EmployeeInfo>(employee));
            if (newEmployee == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
            {
                return Created(Request.RequestUri, _mapper.Map<EmployeeDto>(newEmployee));
            }
        }

        // PUT api/<controller>/5
        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] EmployeeDto employee)
        {
            var updatedEmployee = await _employeeService.PutEmployeeAsync(_mapper.Map<EmployeeInfo>(employee));
            if (updatedEmployee == null)
            {
                return StatusCode(System.Net.HttpStatusCode.NoContent);
            }
            else
            {
                return Ok(_mapper.Map<EmployeeDto>(updatedEmployee));
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(EmployeeIndexDto baseDto)
        {
            await _employeeService.DeleteEmployeeAsync(_mapper.Map<EmployeeIndexInfo>(baseDto));
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}