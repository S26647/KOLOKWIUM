using KOLOKWIUM.DTO;
using KOLOKWIUM.Ex;
using KOLOKWIUM.Repos;

namespace KOLOKWIUM.Services;

public class FireActionService : IFireActionService
{
    private readonly IFireActionRepo _fireActionRepo;

    public FireActionService(IFireActionRepo fireActionRepo)
    {
        _fireActionRepo = fireActionRepo;
    }


    public async Task<IEnumerable<FireFighterActionDTO>> GetFireActionsListByFirefighterIdAsync(int id)
    {
        IEnumerable<FireFighterActionDTO> list = await _fireActionRepo.GetFireActionByFireFighterIdAsync(id);

        return list;
    }

    public async Task AssignFireTruckToActionAsync(int idFireTruck, int idFireAction)
    {
        bool truckEq = await _fireActionRepo.CheckIfFireTruckNeedsSpecialEqAsync(idFireTruck);
        bool actionEq = await _fireActionRepo.CheckIfActionNeedsSpecialEqAsync(idFireAction);

        if (actionEq && !truckEq)
        {
            throw new NoEqException();
        }

        bool fireTruckTaken = await _fireActionRepo.CheckIfFireTruckTakenAsync(idFireTruck);
        if (fireTruckTaken)
        {
            throw new FireTruckAlreadyTakenException();
        }
    }
}