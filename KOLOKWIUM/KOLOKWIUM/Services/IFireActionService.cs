using KOLOKWIUM.DTO;

namespace KOLOKWIUM.Services;

public interface IFireActionService
{
    Task<IEnumerable<FireFighterActionDTO>> GetFireActionsListByFirefighterIdAsync(int id);
    Task AssignFireTruckToActionAsync(int idFireTruck, int idFireAction);
}