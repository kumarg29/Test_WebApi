using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext _context;

        public RegionRepository(NZWalksDBContext context)
        {
            this._context = context;
        }
        public async Task<List<Region>> GetAllAsync()
        {
            return await _context.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid Id)
        {
            return await _context.Regions.FirstAsync(r => r.Id == Id);
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await _context.Regions.AddAsync(region);
            await _context.SaveChangesAsync();
            return region;
        }
        public async Task<Region?> UpdateRegionAsync(Guid Id, Region region)
        {
            var ExistingRegion = await _context.Regions.FirstOrDefaultAsync(X => X.Id == Id);
            if(ExistingRegion == null)
            {

                return null;
            }
            ExistingRegion.Code = region.Code;
            ExistingRegion.Name = region.Name;
            ExistingRegion.ImageUrl = region.ImageUrl;
            await _context.SaveChangesAsync();

            return ExistingRegion;

        }

        public async Task<Region?> DeleteRegionAsync(Guid Id)
        {
            var ExistingRegion = _context.Regions.FirstOrDefault(X => X.Id == Id);
            if(ExistingRegion == null)
            {
                return null;
            }

            _context.Regions.Remove(ExistingRegion);
            await _context.SaveChangesAsync();
            return ExistingRegion;
        }
    }
}
