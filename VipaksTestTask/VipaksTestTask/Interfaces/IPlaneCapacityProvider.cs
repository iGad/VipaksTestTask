using VipaksTestTask.Models;

namespace VipaksTestTask.Interfaces
{
    /// <summary>
    /// Интерфейс поставщика вместимости конкретного типа самолета
    /// </summary>
    public interface IPlaneCapacityProvider
    {
        int GetPlaneCapacity(PlaneType planeType);
    }
}