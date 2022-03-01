using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Contracts.Profiles.Requests;
using PerfumeManufacturerProject.Contracts.Profiles.Responses;

namespace PerfumeManufacturerProject.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProfilesController : ControllerBase
    {
        private readonly IProfilesService _profilesService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProfilesController> _logger;

        public ProfilesController(IProfilesService profilesService, IMapper mapper, ILogger<ProfilesController> logger)
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
                return Ok(_mapper.Map<IEnumerable<ProfileResponse>>(result));
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateProfileRequest request)
        {
            try
            {
                var result = await _profilesService.CreateAsync(request.Name);
                return Ok(_mapper.Map<ProfileResponse>(result));
            }
            catch (Exception e)
            {
                return HandleProfilesErrors(e);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateProfileRequest request)
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
