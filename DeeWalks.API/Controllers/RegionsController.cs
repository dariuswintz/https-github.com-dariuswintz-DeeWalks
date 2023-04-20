using AutoMapper;
using DeeWalks.API.Data;
using DeeWalks.API.Models.Domain;
using DeeWalks.API.Models.DTO;
using DeeWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace DeeWalks.API.Controllers
{
    // Location is point to /api /controller
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly DeeWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(DeeWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }
        // Get All Regions
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Data From Database - Domain Models
            var regionsDomain = await regionRepository.GetAllAsync();

            // Map Domain Models to DTOs
          //  var regionsDto = new List<RegionDto>();
            //foreach (var regionDomain in regionsDomain) 
           // {
             //   regionsDto.Add(new RegionDto()
               // {
                 //   Id = regionDomain.Id,
                   // Code = regionDomain.Code,
                   // Name = regionDomain.Name,
                  //  RegionImageUrl = regionDomain.RegionImageUrl,
                //});
          //  }
            // Map Domain Models to DTOs
            var regionsDto = mapper.Map<List<RegionDto>>(regionsDomain);
            // Return DTOs
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain));
        }


        // GET SINGLE REGION (Get Region by ID)
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetbyId([FromRoute] Guid id) 
        {
            
            // Get Region Domain Model From Database
            // var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Region DOmain Model to Region DTO
            //

          

            // Return DTO back to Client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

        // POST Keyword  To Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        public async  Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            // Map or Convert DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(addRegionRequestDto);

            //AutoMapper Option
            //Add a Mappings Foler, with a Mapping Class
            //Create Profiles by :Profile ctrl dot import namespace
            //CreateMap<source and destination>()
            //.ForMember(x => x.Name, opt.MapFrom(x => x.FullName)) 
            //.ReverseMap
            //Define what is the source and destination with public class to public string

            // Use Domain Model to Create Region
            regionDomainModel = await regionRepository.CreateASync(regionDomainModel);
            await dbContext.SaveChangesAsync();

            // Map Domain Model back to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return CreatedAtAction(nameof(GetbyId), new { id = regionDomainModel.Id }, regionDomainModel);
        }



        // Update Region
        // PUT: https://localhost:portnumber/api/regions {id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async  Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {

            // Map DTO to Domain Model
            var regionDomainModel = mapper.Map<Region>(updateRegionRequestDto);

            // Check if region exits
            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);
            
            if (regionDomainModel == null) 
            {
                return NotFound();
            }

            //Convert Domain Model to DTO
            var regionDto = mapper.Map<RegionDto>(regionDomainModel);

            return Ok(regionDto);


        }


        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions {id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
           var regionDomainModel = await regionRepository.DeleteAsync(id);  

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            // Delete Region
            //return deleted Region back
            // Map domain Model to DTO
            
            return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }
    }

    
}
