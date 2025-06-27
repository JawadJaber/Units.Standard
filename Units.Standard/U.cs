using System;
using System.Collections.Generic;
using System.Text;

namespace Units.Standard
{
    public static class U
    {
        public static string Name = "Name";
        public const string ft = "ft";
        public const string cm = "cm";
        public const string m = "m";
        public const string inch = "in";
        //public const string inchcubic = "in³";
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

        public const string CubFt = "ft³";
        public const string CubM = "m³";
        public const string CubIn = "in³";


        public const string HZ = "Hz";
        public const string RPM = "rpm";
        public const string RPS = "rps";
        public const string KgPerHr = "Kg/Hr";
        public const string KgPerS = "Kg/s";
        public const string GPerSec = "g/s";
        public const string IbPerHr = "lbs/Hr";
        
        
        public const string USD = "USD";
        public const string JD = "JD";
        public const string EUR = "EUR";
        public const string SAR = "SAR";



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


    public interface IDefaultUnits
    {
        string DefaultAirFlowUnit { get; set; }
        string DefaultAirVelocityUnit { get; set; }
        string DefaultAirPressureUnit { get; set; }
        string DefaultWaterPressureUnit { get; set; }
        string DefaultWaterFlowUnit { get; set; }
        string DefaultWaterVelocityUnit { get; set; }
        string DefaultPipeSizeUnit { get; set; }
        string DefaultAirTemperatureUnit { get; set; }
        string DefaultWaterTemperatureUnit { get; set; }
        string DefaultDimUnit { get; set; }
        string DefaultPulleySizeUnit { get; set; }
        string DefaultVibrationDisplacementUnit { get; set; }
        string DefaultVibrationVelocityUnit { get; set; }
    }



    public class DefaultUnit:IDefaultUnits
    {

        //public static string StaticAirFlowUnit = U.LpS;
        //public static string StaticSizeUnit = U.mm;
        //public static string StaticVelocityUnit = U.MPS;
        //public static string StaticWaterFlowUnit = U.LpS;
        //public static string StaticAirPressureDropUnit = U.Pa;
        //public static string StaticWaterPressureDropUnit = U.kPa;
        //public static string StaticPipeSizeUnit = U.mm;
        //public static string StaticWaterTemperatureUnit = U.C;
        //public static string StaticAirTemperatureUnit = U.C;
        //public static string StaticWaterVelocityUnit = U.MPS;
        public string DefaultAirFlowUnit { get; set; } = U.LpS;
        public string DefaultAirVelocityUnit { get; set; } = U.MPS;
        public string DefaultAirPressureUnit { get; set; } = U.Pa;
        public string DefaultWaterPressureUnit { get; set; } = U.kPa;
        public string DefaultWaterFlowUnit { get; set; } = U.LpS;
        public string DefaultWaterVelocityUnit { get; set; } = U.MPS;
        public string DefaultPipeSizeUnit { get; set; }= U.mm;
        public string DefaultAirTemperatureUnit { get; set; }= U.C;
        public string DefaultWaterTemperatureUnit { get; set; }= U.C;
        public string DefaultDimUnit { get; set; } = U.mm;

        public static DefaultUnit Instance { get; set; } = new DefaultUnit();

        public static void UpdateFrom(IDefaultUnits defaultUnits)
        {
            
            Instance.DefaultAirFlowUnit = defaultUnits.DefaultAirFlowUnit;
            Instance.DefaultAirVelocityUnit = defaultUnits.DefaultAirVelocityUnit;
            Instance.DefaultAirPressureUnit = defaultUnits.DefaultAirPressureUnit;
            Instance.DefaultWaterPressureUnit = defaultUnits.DefaultWaterPressureUnit;
            Instance.DefaultWaterFlowUnit = defaultUnits.DefaultWaterFlowUnit;
            Instance.DefaultWaterVelocityUnit = defaultUnits.DefaultWaterVelocityUnit;
            Instance.DefaultPipeSizeUnit = defaultUnits.DefaultPipeSizeUnit;
            Instance.DefaultAirTemperatureUnit = defaultUnits.DefaultAirTemperatureUnit;
            Instance.DefaultWaterTemperatureUnit = defaultUnits.DefaultWaterTemperatureUnit;
            Instance.DefaultDimUnit = defaultUnits.DefaultDimUnit;
            Instance.DefaultPulleySizeUnit = defaultUnits.DefaultPulleySizeUnit;
            Instance.DefaultVibrationDisplacementUnit = defaultUnits.DefaultVibrationDisplacementUnit;
            Instance.DefaultVibrationVelocityUnit = defaultUnits.DefaultVibrationVelocityUnit;

        }

        public string DefaultVibrationDisplacementUnit { get; set; } = U.mm;
        public string DefaultVibrationVelocityUnit { get; set; } = U.MMPerSec;
        public string DefaultPulleySizeUnit { get; set; } = U.mm;
    }

}
