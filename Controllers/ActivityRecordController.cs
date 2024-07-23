using Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.Domains;
using Models.DTOs;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ActivityRecordController : ControllerBase
{
    private readonly IActivityRecordRepository activityRecordRepository;
    private readonly IMapper mapper;
    private readonly UserManager<IdentityUser> userManager;

    public ActivityRecordController(IActivityRecordRepository activityRecordRepository, IMapper mapper, UserManager<IdentityUser> userManager)
    {
        this.activityRecordRepository = activityRecordRepository;
        this.mapper = mapper;
        this.userManager = userManager;
    }
    private string GetUserId()
    {
        return User.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ActivityRecord>>> GetActivityRecords()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }
        // Get data from database - domain models
        var activityRecordsDomain = await activityRecordRepository.GetAllByUserAsync(userId);

        var activityRecordsDto = mapper.Map<List<ActivityRecordDto>>(activityRecordsDomain);

        // Map Domain Models to DTO
        return Ok(activityRecordsDto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActivityRecord>> GetActivityRecord([FromRoute] Guid id)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        var activityRecordDomain = await activityRecordRepository.GetByRecordIdAndUserIdAsync(id, userId);

        if (activityRecordDomain == null)
        {
            return NotFound();
        }

        return Ok(mapper.Map<ActivityRecordDto>(activityRecordDomain));
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AddActivityRecordRequestDto addActivityRecordRequestDto)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        // convert DTO to domain mondel
        var activityRecord = mapper.Map<ActivityRecord>(addActivityRecordRequestDto);
        activityRecord.UserId = userId;

        // use domain model to create activity
        activityRecord = await activityRecordRepository.CreateAsync(activityRecord);

        // convert back to DTO
        var activityRecordDto = mapper.Map<ActivityRecordDto>(activityRecord);

        return CreatedAtAction(nameof(GetActivityRecord), new { id = activityRecordDto.Id }, activityRecordDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized();
        }

        // check if activity exists
        var activityDomainModel = await activityRecordRepository.DeleteAsync(id, userId);

        if(activityDomainModel == null)
        {
            return NotFound();
        }

        //convert domain model to DTO
        var activityDto = mapper.Map<ActivityRecordDto>(activityDomainModel);

        return Ok(activityDto);
    }
}
