using DeeWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace DeeWalks.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateASync(Region region);

        Task<Region> GetByRegionIdAsync(Region region);

        Task<Region?> UpdateAsync(Guid id, Region region);

        Task<Region?> DeleteAsync(Guid id);

    }
}
