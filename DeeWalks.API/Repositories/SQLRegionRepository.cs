using AutoMapper.Configuration.Annotations;
using DeeWalks.API.Data;
using DeeWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace DeeWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly DeeWalksDbContext dbContext;

        // Inject DB Context Class into Repository
        public SQLRegionRepository(DeeWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public Task<Region> CreateASync(Region region)
        {
            throw new NotImplementedException();
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null) 
            {
                return null;
            }

            dbContext.Regions.Remove(existingRegion);
            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<List<Region>> GetAllAsync()
        {
           return await dbContext.Regions.ToListAsync();   
        }

        public Task<Region?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Region> GetByRegionIdAsync(Region region)
        {
            throw new NotImplementedException();
        }

        public async Task<Region?> Update(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion == null)
            {
                return null;
            }
            
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public Task<Region?> UpdateAsync(Guid id, Region region)
        {
            throw new NotImplementedException();
        }
    }
}
