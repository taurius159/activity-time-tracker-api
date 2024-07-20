using Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.DTOs;

[Route("api/[controller]")]
[ApiController]
public class ActivityController : ControllerBase
{
    private readonly IActivityRepository activityRepository;

    public ActivityController(IActivityRepository activityRepository)
    {
        this.activityRepository = activityRepository;
    }

    // [HttpGet]
    // public async Task<ActionResult<IEnumerable<Activity>>> GetActivities()
    // {
    //     return await _context.Activities.ToListAsync();
    // }

    [HttpGet("{id}")]
    public async Task<ActionResult<Activity>> GetActivity([FromRoute] Guid id)
    {
         var activityDomain = await activityRepository.GetByIdAsync(id);

        if (activityDomain == null)
        {
            return NotFound();
        }

        //convert to DTO
        var activityDto = new ActivityDto
        {
          Id = activityDomain.Id,
          Name = activityDomain.Name,
          Description = activityDomain.Description  
        };

        return Ok(activityDto);
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] AddActivityRequestDto addActivityRequestDto)
    {
        // convert DTO to domain mondel
        var activity = new Activity{
            Id = Guid.NewGuid(),
            Name = addActivityRequestDto.Name,
            Description = addActivityRequestDto.Description
        };

        // use domain model to create activity
        activity = await activityRepository.CreateAsync(activity);

        // convert back to DTO
        var activityDto = new ActivityDto
        {
            Id = activity.Id,
            Name = activity.Name,
            Description = activity.Description
        };

        return CreatedAtAction(nameof(GetActivity), new { id = activityDto.Id }, activityDto);
    }

    // [HttpPut("{id}")]
    // public async Task<IActionResult> PutActivity(int id, Activity activity)
    // {
    //     if (id != activity.Id)
    //     {
    //         return BadRequest();
    //     }

    //     _context.Entry(activity).State = EntityState.Modified;

    //     try
    //     {
    //         await _context.SaveChangesAsync();
    //     }
    //     catch (DbUpdateConcurrencyException)
    //     {
    //         if (!ActivityExists(id))
    //         {
    //             return NotFound();
    //         }
    //         else
    //         {
    //             throw;
    //         }
    //     }

    //     return NoContent();
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
