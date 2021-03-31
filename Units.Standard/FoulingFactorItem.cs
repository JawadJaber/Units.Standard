using DotLiquid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Units.Standard
{
    public class FoulingFactorItem : IFoulingFactor, INotifyPropertyChanged, IUnit, ILiquidizable, IComparable, IComparable<FoulingFactorItem>
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

        #endregion

        public FoulingFactorItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static FoulingFactorItem Create(double value, string unit) { return new FoulingFactorItem(value, unit); }
        }


        public static FoulingFactorItem Create(FoulingFactorItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        #region IFoulingFactor



        private double _ValueInSqM_CPerkW { get; set; }
        public double ValueInSqM_CPerkW
        {
            get
            {
                return _ValueInSqM_CPerkW;
            }
            set
            {
                if (_ValueInSqM_CPerkW == value)
                    return;
                _ValueInSqM_CPerkW = value;
                OnPropertyChanged(nameof(ValueInSqM_CPerkW));
            }
        }


        private double _ValueInSqft_h_FPerBtu { get; set; }
        public double ValueInSqft_h_FPerBtu
        {
            get
            {
                return _ValueInSqft_h_FPerBtu;
            }
            set
            {
                if (_ValueInSqft_h_FPerBtu == value)
                    return;
                _ValueInSqft_h_FPerBtu = value;
                OnPropertyChanged(nameof(ValueInSqft_h_FPerBtu));
            }
        }





        #endregion


        #region IUnit



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

        private string _ValueAsString { get; set; }
        public string ValueAsString
        {
            get
            {
                return _ValueAsString;
            }
            set
            {
                if (_ValueAsString == value)
                    return;
                _ValueAsString = value;
                OnPropertyChanged(nameof(ValueAsString));
            }
        }

        public void UpdateWhenUnitChanged()
        {
            switch (Unit)
            {
                case U.Sqft_h_FPerBtu:
                    Value = ValueInSqft_h_FPerBtu;
                    ValueAsString = Value.ToString("N6");
                    break;
                case U.SqM_CPerkW:
                    Value = ValueInSqM_CPerkW;
                    ValueAsString = Value.ToString("N6");
                    break;
                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            switch (Unit)
            {
                case U.Sqft_h_FPerBtu:
                    double valueSqft_h_FPerBtu = Value;
                    ValueInSqft_h_FPerBtu = valueSqft_h_FPerBtu;
                    ValueInSqM_CPerkW = Converter.ConvertFoulingFactorFrom_Sqft_h_FPerBtu_to_SqM_CPerkW(valueSqft_h_FPerBtu);
                    ValueAsString = Value.ToString("N6");
                    break;
                case U.SqM_CPerkW:
                    double valueInSqM_CPerkW = Value;
                    ValueInSqM_CPerkW = valueInSqM_CPerkW;
                    ValueInSqft_h_FPerBtu = Converter.ConvertFoulingFactorFrom_SqM_CPerkW_to_Sqft_h_FPerBtu(valueInSqM_CPerkW);
                    ValueAsString = Value.ToString("N6");
                    break;
                default:
                    break;
            }
        }



        #endregion


        #region ILiquidizable

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToSignificantDigits(4),
                Unit,
                ValueInSqft_h_FPerBtu,
                ValueInSqM_CPerkW,
                ValueAsString
            };
        }

        #endregion


        public int CompareTo(object obj)
        {
            if (obj is FoulingFactorItem)
            {
                return this.CompareTo((FoulingFactorItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(FoulingFactorItem other)
        {
            if (this != null && other != null)
            {
                return this.Value.CompareTo(other.Value);
            }
            else
            {
                return 0;
            }

        }

    }
}
