using DotLiquid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Units.Standard
{
    public interface IVelocityItem
    {
        double ValueInFtPerMin { get; set; }
        double ValueInMPS { get; set; }

        string Unit { get; set; }
    }

    public class VelocityItem : IUnit, IVelocityItem, INotifyPropertyChanged, ILiquidizable
    {
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

        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.FtPerMin)
            {
                Value = ValueInFtPerMin;
            }
            else
            {
                Value = ValueInMPS;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.FtPerMin)
            {
                double valueInFtPerMin = Value;
                ValueInFtPerMin = valueInFtPerMin;
                ValueInMPS = Converter.ConvertSpeedFrom_FtPerMin_To_MPS(valueInFtPerMin);
            }
            else
            {
                double valueInMPS = Value;
                ValueInMPS = valueInMPS;
                ValueInFtPerMin = Converter.ConvertSpeedFrom_MPS_To_FtPerMin(valueInMPS);
            }
        }



        #endregion

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

        private double _ValueInLbPerFt3 { get; set; }
        public double ValueInFtPerMin
        {
            get
            {
                return _ValueInLbPerFt3;
            }
            set
            {
                if (_ValueInLbPerFt3 == value)
                    return;
                _ValueInLbPerFt3 = value;
                OnPropertyChanged(nameof(ValueInFtPerMin));
            }
        }

        private double _ValueInKgPerM3 { get; set; }
        public double ValueInMPS
        {
            get
            {
                return _ValueInKgPerM3;
            }
            set
            {
                if (_ValueInKgPerM3 == value)
                    return;
                _ValueInKgPerM3 = value;
                OnPropertyChanged(nameof(ValueInMPS));
            }
        }

        public VelocityItem(double valueInFtPerMin, double valueInMPS, string unit)
        {

            if ((unit == U.FtPerMin) && valueInMPS != Converter.ConvertSpeedFrom_FtPerMin_To_MPS(valueInFtPerMin))
            {
                valueInMPS = Converter.ConvertSpeedFrom_FtPerMin_To_MPS(valueInFtPerMin);
            }

            if (unit == U.MPS && valueInFtPerMin != Converter.ConvertSpeedFrom_MPS_To_FtPerMin(valueInMPS))
            {
                valueInFtPerMin = Converter.ConvertSpeedFrom_MPS_To_FtPerMin(valueInMPS);
            }

            this.ValueInMPS = valueInMPS;
            this.ValueInFtPerMin = valueInFtPerMin;
            this.Unit = unit;
        }

        private VelocityItem()
        {

        }

        private VelocityItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static VelocityItem Create(double value, string unit) { return new VelocityItem(value, unit); }
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInMPS,
                ValueInFtPerMin
            };
        }
    }
}
