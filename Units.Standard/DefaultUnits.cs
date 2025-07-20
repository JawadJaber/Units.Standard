using System;
using System.Collections.Generic;
using System.Text;

namespace Units.Standard
{
    public class DefaultUnits
    {
        public static List<Tuple<string, string>> DefaultUnitsList = new List<Tuple<string, string>>();

        static DefaultUnits()
        {
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(AirFlowItem), "U.CFM"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(TemperatureItem), "U.F"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(TemperatureDifferenceItem), "U.F"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(PressureDropITem), "U.inWG"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(CoilLengthItem), "U.inch"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(WaterMassFlowItem), "U.IbPerHr"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(RefrigerantMassFlowItem), "U.IbPerHr"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(WaterFlowItem), "U.GPM"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(AltitudeItem), "U.ft"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(AreaItem), "U.SqIn"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(VolumeItem), "U.CubFt"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(WeightItem), "U.Lb"));
            DefaultUnitsList.Add(new Tuple<string, string>(nameof(CapacityItem), "U.MBH"));
        }
    }
}
