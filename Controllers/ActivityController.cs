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

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateActivityRequestDto updateActivityRequestDto)
    {
        //map DTO to domain model for passing to repository for updating
        var activityDomainModel = mapper.Map<Activity>(updateActivityRequestDto);
        
        // check if activity exists
        activityDomainModel = await activityRepository.UpdateAsync(id, activityDomainModel);

        if (activityDomainModel == null)
        {
            return NotFound();
        }

        //convert domain model to DTO
        var activityDto = mapper.Map<ActivityDto>(activityDomainModel);

        return Ok(activityDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        // check if activity exists
        var activityDomainModel = await activityRepository.DeleteAsync(id);

        if(activityDomainModel == null)
        {
            return NotFound();
        }

        //convert domain model to DTO
        var activityDto = mapper.Map<ActivityDto>(activityDomainModel);

        return Ok(activityDto);
    }
}
