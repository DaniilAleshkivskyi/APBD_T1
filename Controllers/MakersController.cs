using APBD_TEST_TEMPLATE.DTOs;
using APBD_TEST_TEMPLATE.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_TEST_TEMPLATE.Controllers;

[ApiController]
[Route("api/makers")]
public class MakersController : ControllerBase
{
    private readonly IMakerService _makerService;

    public MakersController(IMakerService makerService)
    {
        _makerService = makerService;
    }
    
    
    
    [HttpGet]
    public async Task<IActionResult> GetAllMakersAsync([FromQuery] string? name =  null)
    {
        var result = await _makerService.getAllMakersAsync(name);
        if (result.Count == 0)
        {
            return BadRequest();
        }
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateMakerAsync([FromBody] MakerDTO maker)
    {
        await _makerService.CreateMakerAsync(maker);
        return Ok();
    }
    
}