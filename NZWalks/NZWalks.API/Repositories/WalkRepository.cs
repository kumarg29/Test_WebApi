using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext _Context;

        public WalkRepository(NZWalksDBContext Context)
        {
            _Context = Context;
        }

        public async Task<Walk> CreateWalkAsync(Walk walk)
        {
            await _Context.Walks.AddAsync(walk);
            await _Context.SaveChangesAsync();
            return walk;
        }



        public async Task<List<Walk>> GetAllAsync(string? FilterOn = null, string? FilterQuery = null, string? SortBy = null, bool IsAscending = true, int PageNumber = 1, int PageSize = 1000) //FilterOn is the column on which we are filtering and FilterQuery is the Query
        {
            var Walks = _Context.Walks.Include("Difficulty").Include("Region").AsQueryable();
            // Filtering
            if (string.IsNullOrWhiteSpace(FilterOn) == false && string.IsNullOrWhiteSpace(FilterQuery) == false)
            {
                if (FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = Walks.Where(X => X.Name.Contains(FilterQuery));
                }

            }
            //Sorting
            if (string.IsNullOrWhiteSpace(SortBy) == false)
            {
                if (SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = IsAscending ? Walks.OrderBy(X => X.Name) : Walks.OrderByDescending(X => X.Name);
                }
                if (SortBy.Equals("Length", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = IsAscending ? Walks.OrderBy(X => X.LengthInKm) : Walks.OrderByDescending(X => X.LengthInKm);
                }
            }
            //Pagination
            var skipresult = (PageNumber - 1) * PageSize;

            return await Walks.Skip(skipresult).Take(PageSize).ToListAsync();


            //return await _Context.Walks.Include("Difficulty").Include("Region").ToListAsync();

        }
        public async Task<Walk?> GetByIdAsync(Guid Id)
        {
            return await _Context.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(X => X.Id == Id);
        }

        public async Task<Walk?> UpdateAsync(Guid Id, Walk walk)
        {
            var ExistingWalk = await _Context.Walks.FirstOrDefaultAsync(X => X.Id == Id);
            if (ExistingWalk == null)
            {
                return null;
            }
            ExistingWalk.Name = walk.Name;
            ExistingWalk.Description = walk.Description;
            ExistingWalk.LengthInKm = walk.LengthInKm;
            ExistingWalk.WalkImageUrl = walk.WalkImageUrl;
            ExistingWalk.DifficultyId = walk.DifficultyId;
            ExistingWalk.RegionId = walk.RegionId;


            await _Context.SaveChangesAsync();
            return ExistingWalk;


        }
        public async Task<Walk?> DeleteWalkAsync(Guid Id)
        {
            var ExistingWalk = _Context.Walks.FirstOrDefault(X => X.Id == Id);
            if (ExistingWalk == null)
            {
                return null;
            }
            _Context.Walks.Remove(ExistingWalk);
            await _Context.SaveChangesAsync();
            return ExistingWalk;
        }
    }
}
