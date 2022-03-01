using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Contracts.Admin.Responses;
using PerfumeManufacturerProject.Contracts.Users.Requests;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Controllers
{
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
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

        //edit, delete

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
