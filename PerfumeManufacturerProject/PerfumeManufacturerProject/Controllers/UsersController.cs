using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Contracts.Users.Requests;
using PerfumeManufacturerProject.Contracts.Users.Responses;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersService usersService, IMapper mapper, ILogger<UsersController> logger)
        {
            _usersService = usersService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync(string id)
        {
            try
            {
                var result = await _usersService.GetAsync(id);
                return Ok(_mapper.Map<UserResponse>(result));
            }
            catch (Exception e)
            {
                return HandleUsersErrors(e);
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _usersService.GetUsersAsync();
                return Ok(_mapper.Map<IEnumerable<UserResponse>>(result));
            }
            catch (Exception e)
            {
                return HandleUsersErrors(e);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(UserResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequest request)
        {
            try
            {
                var result = await _usersService.CreateAsync(request.UserName, request.Password, request.FirstName, request.LastName, request.RoleName);
                return Ok(_mapper.Map<UserResponse>(result));
            }
            catch (Exception e)
            {
                return HandleUsersErrors(e);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserRequest request)
        {
            try
            {
                await _usersService.UpdateAsync(request.Id, request.FirstName, request.LastName, request.UserName, request.Password, request.RoleName);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleUsersErrors(e);
            }
        }

        [HttpDelete("{id}")]
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
                return HandleUsersErrors(e);
            }
        }

        private IActionResult HandleUsersErrors(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return exception switch
            {
                _ => BadRequest(exception.Message)
            };
        }
    }
}
