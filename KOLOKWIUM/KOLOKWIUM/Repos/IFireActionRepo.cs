using KOLOKWIUM.DTO;

namespace KOLOKWIUM.Repos;

public interface IFireActionRepo
{
    Task<IEnumerable<FireFighterActionDTO>> GetFireActionByFireFighterIdAsync(int id);

    Task AssignFireTruckToActionAsync(int idFireAction, int idFireTruck);

    Task<bool> CheckIfFireTruckTakenAsync(int idFireTruck);
    Task<bool> CheckIfFireTruckNeedsSpecialEqAsync(int idFireTruck);
    Task<bool> CheckIfActionNeedsSpecialEqAsync(int idFireAction);
}