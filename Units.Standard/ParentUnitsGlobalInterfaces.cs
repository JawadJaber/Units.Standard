using System;
using System.Collections.Generic;
using System.Text;

namespace Units.Standard
{
    public interface ITemperatureUnit
    {
        string TempUnit { get; set; }
    }

    public interface IAirFlowUnit
    {
        string AirFlowUnit { get; set; }
    }

    public interface IWaterFlowUnit
    {
        string WaterFlowUnit { get; set; }
    }

    public interface IPressureDropUnit
    {
        string PressureDropUnit { get; set; }
    }

    public interface IVelocityUnit
    {
        string VelocityUnit { get; set; }
    }
}
