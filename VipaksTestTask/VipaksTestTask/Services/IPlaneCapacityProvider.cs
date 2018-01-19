namespace VipaksTestTask.Services
{
    public interface IPlaneCapacityProvider
    {
        int GetPlaneCapacity(PlaneType planeType);
    }
}