using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Contracts.Auth.Requests;
using PerfumeManufacturerProject.Contracts.Auth.Responses;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IMapper mapper, ILogger<AuthController> logger)
        {
            _authService = authService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.LoginAsync(request.UserName, request.Password);
                return Ok(_mapper.Map<LoginResponse>(result));
            }
            catch (Exception e)
            {
                return HandleAuthErrors(e);
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> RegisterAsync([FromBody] LoginRequest request)
        {
            try
            {
                var result = await _authService.RegisterAsync(request.UserName, request.Password);
                return Ok(_mapper.Map<LoginResponse>(result));
            }
            catch (Exception e)
            {
                return HandleAuthErrors(e);
            }
        }

        [HttpDelete("login")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LogoutAsync()
        {
            try
            {
                await _authService.LogoutAsync();
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleAuthErrors(e);
            }
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMeAsync()
        {
            try
            {
                var result = await _authService.GetMeAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return HandleAuthErrors(e);
            }
        }

        private IActionResult HandleAuthErrors(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return exception switch
            {
                _ => BadRequest(exception.Message)
            };
        }
    }
}
