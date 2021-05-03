using System;
using System.Collections.Generic;
using System.Text;

namespace Units.Standard
{
    public static class UnitsMethods
    {
        public static List<string> GetUnitsList(string name)
        {
            switch (name)
            {
                case AirFlowItem.Name:
                    return AirFlowItem.GetUnits();
                case VelocityItem.Name:
                    return VelocityItem.GetUnits();
                case TemperatureItem.Name:
                    return TemperatureItem.GetUnits();
                case AreaItem.Name:
                    return AreaItem.GetUnits();
                case CapacityItem.Name:
                    return CapacityItem.GetUnits();
                case PressureDropITem.Name:
                    return PressureDropITem.GetUnits();
                case WaterFlowItem.Name:
                    return WaterFlowItem.GetUnits();
                case WeightItem.Name:
                    return WeightItem.GetUnits();

                default:
                    return new List<string>();

            }
        }
    }
}
