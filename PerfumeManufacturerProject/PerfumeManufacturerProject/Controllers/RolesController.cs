using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Contracts.Roles.Requests;
using PerfumeManufacturerProject.Contracts.Roles.Responses;

namespace PerfumeManufacturerProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _profilesService;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRolesService profilesService, IMapper mapper, ILogger<RolesController> logger)
        {
            _profilesService = profilesService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _profilesService.GetAsync();
                return Ok(_mapper.Map<IEnumerable<RoleResponse>>(result));
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
        {
            try
            {
                var result = await _profilesService.CreateAsync(request.Name);
                return Ok(_mapper.Map<RoleResponse>(result));
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleRequest request)
        {
            try
            {
                await _profilesService.UpdateAsync(request.Id, request.Name);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _profilesService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        private IActionResult HandleProfilesErrors(Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return exception switch
            {
                _ => BadRequest(exception.Message)
            };
        }
    }
}
