using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PerfumeManufacturerProject.Business.Interfaces.Exceptions;
using PerfumeManufacturerProject.Business.Interfaces.Models;
using PerfumeManufacturerProject.Business.Interfaces.Services;
using PerfumeManufacturerProject.Data.EF;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PerfumeManufacturerProject.Business.Services
{
    public class ProfilesService : IProfilesService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProfilesService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProfileModel>> GetAsync()
        {
            var profiles = await _context.Profiles.ToListAsync();
            return _mapper.Map<IEnumerable<ProfileModel>>(profiles);
        }

        public async Task<ProfileModel> CreateAsync(string name)
        {
            if (await _context.Profiles.FirstOrDefaultAsync(x => x.Name == name) != null)
                throw new ProfileAlreadyExistsException(name);

            var profile = new Data.Interfaces.Models.Profile { Name = name };
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProfileModel>(profile);
        }

        public async Task UpdateAsync(string id, string name)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new ProfileNotFoundException(id);

            profile.Name = string.IsNullOrEmpty(name) ? profile.Name : name;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id.ToString() == id) ?? throw new ProfileNotFoundException(id);

            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
        }

        public Task AddPermissionAsync(string id, string name)
        {
            throw new System.NotImplementedException();
        }
        
        public Task DeletePermissionAsync(string id, string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
