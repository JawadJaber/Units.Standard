﻿using DotLiquid;
using System.ComponentModel;

namespace Units.Standard
{
    public class WaterFlowItem : IUnit, IWaterFlow, INotifyPropertyChanged, ILiquidizable
    {
        public WaterFlowItem(double valueInGPM, double valueInMPS, double valueInLPS, string unit)
        {
            if (unit == U.GPM)
            {
                ValueInGPM = valueInGPM;
                ValueInMPS = Converter.ConvertWaterFlowFrom_GMP_To_MPS(valueInGPM);
                ValueInLPS = Converter.ConvertWaterFlowFrom_MPS_To_LPS(ValueInMPS);
                Unit = unit;
            }
            else if (unit == U.MPS)
            {
                ValueInMPS = valueInMPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_MPS_To_GPM(valueInMPS);
                ValueInLPS = Converter.ConvertWaterFlowFrom_MPS_To_LPS(valueInMPS);
                Unit = unit;
            }
            else if (unit == U.LPS)
            {
                ValueInLPS = valueInLPS;
                ValueInMPS = Converter.ConvertWaterFlowFrom_LPS_To_MPS(valueInLPS);
                ValueInGPM = Converter.ConvertWaterFlowFrom_MPS_To_GPM(ValueInMPS);
                Unit = unit;
            }

        }

        private WaterFlowItem()
        {

        }

        private WaterFlowItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static WaterFlowItem Create(double value, string unit) { return new WaterFlowItem(value, unit); }
        }


        #region Properties

        private string _Unit { get; set; }
        public string Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                if (_Unit == value)
                    return;
                _Unit = value;
                OnPropertyChanged(nameof(Unit));
                UpdateWhenUnitChanged();
            }
        }

        private double _Value { get; set; }
        public double Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (_Value == value)
                    return;
                _Value = value;
                OnPropertyChanged(nameof(Value));
                UpdateWhenValueChanged();
            }
        }

        private double _ValueInGPM { get; set; }
        public double ValueInGPM
        {
            get
            {
                return _ValueInGPM;
            }
            set
            {
                if (_ValueInGPM == value)
                    return;
                _ValueInGPM = value;
                OnPropertyChanged(nameof(ValueInGPM));
            }
        }

        private double _ValueInLPS { get; set; }
        public double ValueInLPS
        {
            get
            {
                return _ValueInLPS;
            }
            set
            {
                if (_ValueInLPS == value)
                    return;
                _ValueInLPS = value;
                OnPropertyChanged(nameof(ValueInLPS));
            }
        }

        private double _ValueInMPS { get; set; }
        public double ValueInMPS
        {
            get
            {
                return _ValueInMPS;
            }
            set
            {
                if (_ValueInMPS == value)
                    return;
                _ValueInMPS = value;
                OnPropertyChanged(nameof(ValueInMPS));
            }
        }

        #endregion

        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.GPM || Unit == "GPM")
            {
                Value = ValueInGPM;
            }
            else if (Unit == U.LPS)
            {
                Value = ValueInLPS;
            }
            else if (Unit == U.MPS)
            {
                Value = ValueInMPS;
            }

        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.GPM || Unit == "GPM")
            {
                double valueInGPM = Value;
                ValueInGPM = valueInGPM;
                ValueInMPS = Converter.ConvertWaterFlowFrom_GMP_To_MPS(valueInGPM);
                ValueInLPS = Converter.ConvertWaterFlowFrom_MPS_To_LPS(Converter.ConvertWaterFlowFrom_GMP_To_MPS(valueInGPM));
            }
            else if (Unit == U.LPS)
            {
                double valueInLPS = Value;
                ValueInLPS = valueInLPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_MPS_To_GPM(Converter.ConvertWaterFlowFrom_LPS_To_MPS(valueInLPS));
                ValueInMPS = Converter.ConvertWaterFlowFrom_LPS_To_MPS(valueInLPS);
            }
            else if (Unit == U.MPS)
            {
                double valueInMPS = Value;
                ValueInMPS = valueInMPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_MPS_To_GPM(valueInMPS);
                ValueInLPS = Converter.ConvertWaterFlowFrom_MPS_To_LPS(valueInMPS);
            }

        }



        #region NotifiedPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


        #endregion
        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("3"),
                Unit,
                ValueInGPM,
                ValueInLPS,
                ValueInMPS
            };
        }
    }
}