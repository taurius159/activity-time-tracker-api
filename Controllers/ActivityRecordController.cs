using Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Domains;
using Models.DTOs;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActivityRecordController : ControllerBase
{
    private readonly IActivityRecordRepository activityRecordRepository;
    private readonly IMapper mapper;

    public ActivityRecordController(IActivityRecordRepository activityRecordRepository, IMapper mapper)
    {
        this.activityRecordRepository = activityRecordRepository;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityRecord>>> GetActivityRecords()
    {
        // Get data from database - domain models
        var activityRecordsDomain = await activityRecordRepository.GetAllAsync();

        var activityRecordsDto = mapper.Map<List<ActivityRecordDto>>(activityRecordsDomain);

        // Map Domain Models to DTO
        return Ok(activityRecordsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityRecord>> GetActivityRecord([FromRoute] Guid id)
    {
         var activityRecordDomain = await activityRecordRepository.GetByIdAsync(id);

        if (activityRecordDomain == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<ActivityRecordDto>(activityRecordDomain));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AddActivityRecordRequestDto addActivityRecordRequestDto)
    {
        // convert DTO to domain mondel
        var activityRecord = mapper.Map<ActivityRecord>(addActivityRecordRequestDto);

        // use domain model to create activity
        activityRecord = await activityRecordRepository.CreateAsync(activityRecord);

        // convert back to DTO
        var activityRecordDto = mapper.Map<ActivityRecordDto>(activityRecord);

        return CreatedAtAction(nameof(GetActivityRecord), new { id = activityRecordDto.Id }, activityRecordDto);
    }

    // [HttpPut]
    // [Route("{id:Guid}")]
    // public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateActivityRequestDto updateActivityRequestDto)
    // {
    //     //map DTO to domain model for passing to repository for updating
    //     var activityDomainModel = mapper.Map<Activity>(updateActivityRequestDto);
        
    //     // check if activity exists
    //     activityDomainModel = await activityRepository.UpdateAsync(id, activityDomainModel);

    //     if (activityDomainModel == null)
    //     {
    //         return NotFound();
    //     }

    //     //convert domain model to DTO
    //     var activityDto = mapper.Map<ActivityDto>(activityDomainModel);

    //     return Ok(activityDto);
    // }

    // [HttpDelete]
    // [Route("{id:Guid}")]
    // public async Task<IActionResult> Delete([FromRoute] Guid id)
    // {
    //     // check if activity exists
    //     var activityDomainModel = await activityRepository.DeleteAsync(id);

    //     if(activityDomainModel == null)
    //     {
    //         return NotFound();
    //     }

    //     //convert domain model to DTO
    //     var activityDto = mapper.Map<ActivityDto>(activityDomainModel);

    //     return Ok(activityDto);
    // }
}
