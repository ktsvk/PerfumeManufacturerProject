using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Contracts.Admin.Responses;
using PerfumeManufacturerProject.Contracts.Admin.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Net;

namespace PerfumeManufacturerProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IAdminService adminService, IMapper mapper, ILogger<AdminController> logger)
        {
            _adminService = adminService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AdminResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var result = await _adminService.GetAsync(id);
                return Ok(_mapper.Map<AdminResponse>(result));
            }
            catch (Exception e)
            {
                return HandleAdminErrors(e);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AdminResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _adminService.GetAsync();
                return Ok(_mapper.Map<IEnumerable<AdminResponse>>(result));
            }
            catch (Exception e)
            {
                return HandleAdminErrors(e);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(AdminResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAdminRequest request)
        {
            try
            {
                var result = await _adminService.CreateAsync(request.UserName, request.FirstName, request.LastName, request.Password);
                return Ok(_mapper.Map<AdminResponse>(result));
            }
            catch (Exception e)
            {
                return HandleAdminErrors(e);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAdminRequest request)
        {
            try
            {
                await _adminService.UpdateAsync(request.Id, request.UserName, request.FirstName, request.LastName, request.Password);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleAdminErrors(e);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _adminService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleAdminErrors(e);
            }
        }

        private IActionResult HandleAdminErrors(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return exception switch
            {
                _ => BadRequest(exception.Message)
            };
        }
    }
}
