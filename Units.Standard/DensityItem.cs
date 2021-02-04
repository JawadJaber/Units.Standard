using DotLiquid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Units.Standard
{
    public interface IDensityItem
    {
        double ValueInLbPerFt3 { get; set; }
        double ValueInKgPerM3 { get; set; }

        string Unit { get; set; }
    }

    public class DensityItem : IUnit, IDensityItem, INotifyPropertyChanged, ILiquidizable
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
            if (Unit == U.LbPerFt3)
            {
                Value = ValueInLbPerFt3;
            }
            else
            {
                Value = ValueInKgPerM3;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.LbPerFt3)
            {
                double valueInF = Value;
                ValueInLbPerFt3 = valueInF;
                ValueInKgPerM3 = Converter.ConvertDensityFrom_LbPerFt3_To_KgPerM3(valueInF);
            }
            else
            {
                double valueInC = Value;
                ValueInKgPerM3 = valueInC;
                ValueInLbPerFt3 = Converter.ConvertDensityFrom_KgPerM3_To_LbPerFt3(valueInC);
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
        public double ValueInLbPerFt3
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
                OnPropertyChanged(nameof(ValueInLbPerFt3));
            }
        }

        private double _ValueInKgPerM3 { get; set; }
        public double ValueInKgPerM3
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
                OnPropertyChanged(nameof(ValueInKgPerM3));
            }
        }

        public DensityItem(double valueInLbPerFt3, double valueInKgPerM3, string unit)
        {

            if ((unit == U.LbPerFt3) && valueInKgPerM3 != Converter.ConvertDensityFrom_LbPerFt3_To_KgPerM3(valueInLbPerFt3))
            {
                valueInKgPerM3 = Converter.ConvertDensityFrom_LbPerFt3_To_KgPerM3(valueInLbPerFt3);
            }

            if (unit == U.KgPerM3 && valueInLbPerFt3 != Converter.ConvertDensityFrom_KgPerM3_To_LbPerFt3(valueInKgPerM3))
            {
                valueInLbPerFt3 = Converter.ConvertDensityFrom_KgPerM3_To_LbPerFt3(valueInKgPerM3);
            }

            this.ValueInKgPerM3 = valueInKgPerM3;
            this.ValueInLbPerFt3 = valueInLbPerFt3;
            this.Unit = unit;
        }

        private DensityItem()
        {

        }

        private DensityItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static DensityItem Create(double value, string unit) { return new DensityItem(value, unit); }
        }


        public static DensityItem Create(DensityItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInKgPerM3,
                ValueInLbPerFt3
            };
        }
    }

}
