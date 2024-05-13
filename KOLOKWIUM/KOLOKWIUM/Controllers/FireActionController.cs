using KOLOKWIUM.Ex;
using KOLOKWIUM.Services;
using Microsoft.AspNetCore.Mvc;

namespace KOLOKWIUM.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FireActionController : ControllerBase
{
    private readonly IFireActionService _fireActionService;

    public FireActionController(IFireActionService fireActionService)
    {
        _fireActionService = fireActionService;
    }

    // działa dla FireFighter z ID = 2
    [HttpGet]
    public async Task<IActionResult> GetActionListForFireFighterAsync(int id)
    {
        return Ok(await _fireActionService.GetFireActionsListByFirefighterIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> AssignFiretruckToAction(int idFireTruck, int idFireAction)
    {
        try
        {
            await _fireActionService.AssignFireTruckToActionAsync(idFireTruck, idFireAction);
        }
        catch (FireTruckAlreadyTakenException)
        {
            return StatusCode(400, "firetruck is already taken");
        }
        catch (NoEqException)
        {
            return StatusCode(400, "This action needs firetruck with special eq!");
        }

        return Ok();

    }
}