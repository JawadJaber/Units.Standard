using System;
using System.Collections.Generic;
using System.Text;

namespace Units.Standard
{
    public static class U
    {
        public static string Name = "Name";
        public const string ft = "ft";
        public const string m = "m";
        public const string inch = "in";
        public const string mils = "mils";
        public const string mm = "mm";
        public const string F = "°F";
        public const string C = "°C";
        public const string CFM = "CFM";
        public const string LPS = "LPS";
        public const string LpS = "L/s";
        public const string MPS = "m/s";
        public const string M3PS = "m³/s";
        public const string CMH = "m³/hr";
        public const string TR = "TR";
        public const string MBH = "MBH";
        public const string kW = "kW";
        public const string GPM = "US gpm";
        public const string ftWG = "ftWG";
        public const string kPa = "kPa";
        public const string Pa = "Pa";
        public const string PSI = "PSI";
        public const string Bar = "Bar";
        public const string inWG = "inWG";
        public const string SqM_CPerkW = "m²°C/kW";
        public const string Sqft_h_FPerBtu = "ft²h°F/Btu";
        public const string FinsPerInch = "FinsPerInch";
        public const string FinsPerMeter = "FinsPerMeter";
        public const string FPI = "fpi";
        public const string FPM = "fin/m";
        public const string FPMM = "fin/mm";

        public const string LbPerFt3 = "Lb/ft³";
        public const string KgPerM3 = "Kg/m³";

        public const string Lb = "Lb";
        public const string Kg = "Kg";

        public const string KgPerMS = "Kg/m-s";
        public const string cP = "cP";
        public const string FtPerMin = "ft/min";
        
        public const string MMPerSec = "mm/s";
        public const string InchPerSec = "inches/s";


        public const string NewtonPerM = "N/m";
        public const string DynePerCM = "Dyne/cm";
        
        
        
        public const string SqFt = "ft²";
        public const string SqM = "m²";
        public const string SqIn = "in²";
        
        
        public const string HZ = "Hz";
        public const string RPM = "rpm";
        public const string RPS = "rps";
        public const string KgPerHr = "Kg/Hr";
        public const string IbPerHr = "lbs/Hr";

        public static string ToUnit(this string inputUnit)
        {
            if (string.IsNullOrWhiteSpace(inputUnit))
            {
                return string.Empty;
            }

            if (inputUnit.ToLower() == "c" || inputUnit == U.C)
            {
                return U.C;
            }

            if (inputUnit.ToLower() == "f" || inputUnit == U.F)
            {
                return U.F;
            }

            if (inputUnit.ToLower() == "cfm" || inputUnit == U.CFM)
            {
                return U.CFM;
            }


            if (inputUnit.ToLower() == "lps" || inputUnit == U.LPS)
            {
                return U.LPS;
            }

            if (inputUnit.ToLower() == "cmh" || inputUnit == U.CMH)
            {
                return U.CMH;
            }

            if (inputUnit.ToLower() == "gpm" || inputUnit == U.GPM)
            {
                return U.GPM;
            }

            if (inputUnit.ToLower() == "ft" || inputUnit == U.ft)
            {
                return U.ft;
            }

            if (inputUnit.ToLower() == "inwg" || inputUnit == U.inWG)
            {
                return U.inWG;
            }

            if (inputUnit.ToLower() == "pa" || inputUnit == U.Pa)
            {
                return U.Pa;
            }

            if (inputUnit.ToLower() == "psi" || inputUnit == U.PSI)
            {
                return U.PSI;
            }

            if (inputUnit.ToLower() == "mbh" || inputUnit == U.MBH)
            {
                return U.MBH;
            }

            if (inputUnit.ToLower() == "tr" || inputUnit == U.TR)
            {
                return U.TR;
            }

            if (inputUnit.ToLower() == "kw" || inputUnit == U.kW)
            {
                return U.kW;
            }


            throw new Exception($"input unit {inputUnit} is not defined");
        }


    }
}
