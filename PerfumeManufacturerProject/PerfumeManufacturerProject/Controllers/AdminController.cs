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
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IUsersService usersService, IMapper mapper, ILogger<AdminController> logger)
        {
            _usersService = usersService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AdminResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var result = await _usersService.GetAsync(id);
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
                var result = await _usersService.GetAdminsAsync();
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
                var result = await _usersService.CreateAsync(request.UserName, request.Password, request.FirstName, request.LastName, "Admin");
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
                await _usersService.UpdateAdminAsync(request.Id, request.FirstName, request.LastName, request.UserName, request.OldPassword, request.NewPassword);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleAdminErrors(e);
            }
        }

        [HttpDelete]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _usersService.DeleteAsync(id);
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
