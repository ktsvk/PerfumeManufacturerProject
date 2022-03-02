using System;
using System.Collections.Generic;
using System.Net;
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
        private readonly IRolesService _rolesService;
        private readonly IMapper _mapper;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRolesService rolesService, IMapper mapper, ILogger<RolesController> logger)
        {
            _rolesService = rolesService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RoleResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var result = await _rolesService.GetAsync();
                return Ok(_mapper.Map<IEnumerable<RoleResponse>>(result));
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(RoleResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateRoleRequest request)
        {
            try
            {
                var result = await _rolesService.CreateAsync(request.Name);
                return Ok(_mapper.Map<RoleResponse>(result));
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpPut]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRoleRequest request)
        {
            try
            {
                await _rolesService.UpdateAsync(request.Id, request.Name);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await _rolesService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpGet("permissions")]
        [ProducesResponseType(typeof(IEnumerable<PermissionResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPermissionsAsync()
        {
            try
            {
                var result = await _rolesService.GetPermissionsAsync();
                return Ok(_mapper.Map<IEnumerable<PermissionResponse>>(result));
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpPost("permissions")]
        [ProducesResponseType(typeof(IEnumerable<PermissionResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddPermissionsAsync([FromBody] AddPermissionRequest request)
        {
            try
            {
                await _rolesService.AddPermissionAsync(request.RoleId, request.PermissionId);
                return NoContent();
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpDelete("permissions")]
        [ProducesResponseType(typeof(IEnumerable<PermissionResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeletePermissionsAsync([FromBody] AddPermissionRequest request)
        {
            try
            {
                await _rolesService.DeletePermissionAsync(request.RoleId, request.PermissionId);
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
