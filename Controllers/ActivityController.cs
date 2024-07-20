using Api.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Domains;
using Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly IActivityRepository activityRepository;
    private readonly IMapper mapper;

    public ActivityController(IActivityRepository activityRepository, IMapper mapper)
    {
        this.activityRepository = activityRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
    {
        // Get data from database - domain models
        var activitiesDomain = await activityRepository.GetAllAsync();

        var activitiesDto = mapper.Map<List<ActivityDto>>(activitiesDomain);

        // Map Domain Models to DTO
        return Ok(activitiesDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity([FromRoute] Guid id)
    {
         var activityDomain = await activityRepository.GetByIdAsync(id);

        if (activityDomain == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<ActivityDto>(activityDomain));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AddActivityRequestDto addActivityRequestDto)
    {
        // convert DTO to domain mondel
        var activity = mapper.Map<Activity>(addActivityRequestDto);

        // use domain model to create activity
        activity = await activityRepository.CreateAsync(activity);

        // convert back to DTO
        var activityDto = mapper.Map<ActivityDto>(activity);

        return CreatedAtAction(nameof(GetActivity), new { id = activityDto.Id }, activityDto);
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutActivity(int id, Activity activity)
    // {
    //     // //map DTO to domain model for passing to repository for updating
    //     // var activityDomainModel = mapper.Map<Activity>(updateRegionRequestDto);
        
    //     // // check if region exists
    //     // regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

    //     // if (regionDomainModel == null)
    //     // {
    //     //     return NotFound();
    //     // }

    //     // //convert domain model to DTO
    //     // var regionDto = mapper.Map<RegionDTO>(regionDomainModel);

    //     // return Ok(regionDto);
    // }

    // [HttpDelete("{id}")]
    // public async Task<IActionResult> DeleteActivity(int id)
    // {
    //     var activity = await _context.Activities.FindAsync(id);
    //     if (activity == null)
    //     {
    //         return NotFound();
    //     }

    //     _context.Activities.Remove(activity);
    //     await _context.SaveChangesAsync();

    //     return NoContent();
    // }
}
