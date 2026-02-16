using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkRepository
    {
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<List<Walk>> GetAllAsync(string? FilterOn = null, string? FilterQuery = null, string? SortBy = null, bool IsAscending = true, int PageBumber = 1, int PageSize = 1000);
        Task<Walk?> GetByIdAsync(Guid Id);
        Task<Walk?> UpdateAsync(Guid Id, Walk walk);
        Task<Walk?> DeleteWalkAsync(Guid Id);
    }
}
