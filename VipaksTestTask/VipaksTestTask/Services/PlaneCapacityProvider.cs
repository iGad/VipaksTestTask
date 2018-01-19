using System.Collections.Generic;

namespace VipaksTestTask.Services
{
    public class PlaneCapacityProvider : IPlaneCapacityProvider
    {
        private readonly Dictionary<PlaneType, int> _capacityDictionary = new Dictionary<PlaneType, int>
        {
            {PlaneType.A380, 300},
            {PlaneType.Boing747_400, 250},
            {PlaneType.Boing747_800, 400},
            { PlaneType.Ssj300, 300}
        };
        public int GetPlaneCapacity(PlaneType planeType)
        {
            return _capacityDictionary[planeType];
        }
    }
}