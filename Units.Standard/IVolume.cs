using System;
using System.Collections.Generic;
using System.Text;

namespace Units.Standard
{
    public interface IVolume
    {
        double ValueInCubicInch { get; set; }
        double ValueInCubicMeter { get; set; }
        double ValueInCubicFeet { get; set; }


    }
}
