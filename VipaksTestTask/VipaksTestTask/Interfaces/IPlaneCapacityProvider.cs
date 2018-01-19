using VipaksTestTask.Models;
using VipaksTestTask.Services;

namespace VipaksTestTask.Interfaces
{
    public interface IPlaneCapacityProvider
    {
        int GetPlaneCapacity(PlaneType planeType);
    }
}