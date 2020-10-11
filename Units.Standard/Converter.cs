﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Units.Standard
{
    public static class Converter
    {
        public static double ConvertTempFrom_F_To_C(double TempInF)
        {
            return (TempInF - 32) / 1.8;
        }


        public static double ConvertTempFrom_C_To_F(double TempInC)
        {
            return TempInC * 1.8 + 32;
        }

        public static double ConvertTempDifferenceFrom_deltaF_To_deltaC(double DeltaTempInF)
        {
            return DeltaTempInF / 1.8;
        }


        public static double ConvertTempDifferenceFrom_deltaC_To_deltaF(double DeltaTempInC)
        {
            return DeltaTempInC * 1.8;
        }

        public static double ConvertCapacityFrom_MBH_To_KW(double CapacityInMBH)
        {
            return CapacityInMBH / 3.412;
        }

        public static double ConvertCapacityFrom_KW_To_MBH(double CapacityInKW)
        {
            return CapacityInKW * 3.412;
        }

        public static double ConvertCapacityFrom_KW_To_TR(double CapacityInKW)
        {
            return CapacityInKW / 3.517;
        }

        public static double ConvertCapacityFrom_TR_To_KW(double CapacityInKW)
        {
            return CapacityInKW * 3.517;
        }


        public static double ConvertLengthFrom_M_To_In(double LengthInM)
        {
            return LengthInM / 0.0254;
        }

        public static double ConvertLengthFrom_In_To_M(double LengthInInch)
        {
            return LengthInInch * 0.0254;
        }

        public static double ConvertLengthFrom_M_To_Ft(double LengthInM)
        {
            return LengthInM / 0.3048;
        }

        public static double ConvertLengthFrom_Ft_To_M(double LengthInFeet)
        {
            return LengthInFeet * 0.3048;
        }

        public static double Convert_FinsPerInch_To_FinsPerMeter(double FinsPerInch)
        {
            return FinsPerInch / 0.0254;
        }


        public static double Convert_FinsPerMeter_To_FinsPerInch(double FinsPerMeter)
        {
            return FinsPerMeter * 0.0254;
        }




        public static double ConvertAirFlowFrom_MPS_To_CFM(double FlowInMPS)
        {
            return FlowInMPS * 2118.880003;
        }

        public static double ConvertAirFlowFrom_MPS_To_LPS(double FlowInMPS)
        {
            return FlowInMPS * 1000.0;
        }

        public static double ConvertAirFlowFrom_MPS_To_CMH(double FlowInMPS)
        {
            return FlowInMPS * 3600.0;
        }


        public static double ConvertAirFlowFrom_CFM_To_MPS(double FlowInCFM)
        {
            return FlowInCFM / 2118.880003;
        }

        public static double ConvertAirFlowFrom_LPS_To_MPS(double FlowInLPS)
        {
            return FlowInLPS / 1000.0;
        }

        public static double ConvertAirFlowFrom_CMH_To_MPS(double FlowInCMH)
        {
            return FlowInCMH / 3600.0;
        }





        public static double ConvertWaterFlowFrom_MPS_To_GPM(double FlowInMPS)
        {
            return FlowInMPS * 15850.372483753;//13198.127997058;
        }
        public static double ConvertWaterFlowFrom_MPS_To_LPS(double FlowInMPS)
        {
            return FlowInMPS * 1000.0;
        }


        public static double ConvertWaterFlowFrom_MPS_To_CMH(double FlowInMPS)
        {
            return FlowInMPS * 3600.0;
        }


        public static double ConvertWaterFlowFrom_GMP_To_MPS(double FlowInGPM)
        {
            return FlowInGPM / 15850.372483753;//13198.127997058;
        }

        public static double ConvertWaterFlowFrom_LPS_To_MPS(double FlowInLPS)
        {
            return FlowInLPS / 1000.0;
        }


        public static double ConvertWaterFlowFrom_CMH_To_MPS(double FlowInCMH)
        {
            return FlowInCMH / 3600.0;
        }


        public static double ConvertPressureDropFrom_Pa_To_INWG(double Pa)
        {
            return Pa / 249.0;
        }

        public static double ConvertPressureDropFrom_InWG_ToPa(double InWG)
        {
            return InWG * 249.0;
        }

        public static double ConvertPressureFrom_FtWG_To_kPa(double FtWG)
        {
            return FtWG / 0.33455256555148;
        }

        public static double ConvertPressureDropFrom_kPa_To_FtWG(double kPa)
        {
            return kPa * 0.33455256555148;
        }


        public static double ConvertFoulingFactorFrom_Sqft_h_FPerBtu_to_SqM_CPerkW(double Sqft_h_FPerBtu)
        {

            return Sqft_h_FPerBtu / 0.0056779;
        }

        public static double ConvertFoulingFactorFrom_SqM_CPerkW_to_Sqft_h_FPerBtu(double SqM_CPerkW)
        {

            return SqM_CPerkW * 0.0056779;
        }


        public static double ConvertPressureDropFrom_Pa_To_PSI(double Pa)
        {
            return Pa / 6894.76;
        }



        public static double ConvertPressureDropFrom_PSI_ToPa(double PSI)
        {
            return PSI * 6894.76;
        }


        public static double ConvertDensityFrom_KgPerM3_To_LbPerFt3(double KgPerM3)
        {
            return KgPerM3 / 16.02;
        }

        public static double ConvertDensityFrom_LbPerFt3_To_KgPerM3(double LbPerFt3)
        {
            return LbPerFt3 * 16.02;
        }



        public static double ConvertViscosityFrom_KgPerMS_To_cP(double KgPerM3)
        {
            return KgPerM3 * 1000;
        }

        public static double ConvertViscosityFrom_cP_To_KgPerMS(double LbPerFt3)
        {
            return LbPerFt3 / 1000;
        }



        public static double ConvertDensityFrom_NewtonPerM_To_DynePerCM(double NewtonPerM)
        {
            return NewtonPerM * 1000;
        }

        public static double ConvertDensityFrom_DynePerCM_To_NewtonPerM(double DynePerCM)
        {
            return DynePerCM / 1000;
        }


        public static double ConvertSpeedFrom_FtPerMin_To_MPS(double FPM)
        {
            return FPM * 0.00508;
        }


        public static double ConvertSpeedFrom_MPS_To_FtPerMin(double mps)
        {
            return mps / 0.00508;
        }


      


        public static double ConvertWeightFrom_KG_To_LB(double kg)
        {
            return kg * 2.20462;
        }


        public static double ConvertWeightFrom_LB_To_KG(double kg)
        {
            return kg / 2.20462;
        }
    }
}
