using DotLiquid;
using System;
using System.ComponentModel;

namespace Units.Standard
{
    public class Units : IFoulingFactor, INotifyPropertyChanged, IUnit, ILiquidizable
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

        public Units(double value, string unit)
        {
            Unit = unit;
            Value = value;
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

        public void UpdateWhenUnitChanged()
        {
            switch (Unit)
            {
                case U.Sqft_h_FPerBtu:
                    Value = ValueInSqft_h_FPerBtu;
                    break;
                case U.SqM_CPerkW:
                    Value = ValueInSqM_CPerkW;
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
                    break;
                case U.SqM_CPerkW:
                    double valueInSqM_CPerkW = Value;
                    ValueInSqM_CPerkW = valueInSqM_CPerkW;
                    ValueInSqft_h_FPerBtu = Converter.ConvertFoulingFactorFrom_SqM_CPerkW_to_Sqft_h_FPerBtu(valueInSqM_CPerkW);
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
                Value,
                Unit,
                ValueInSqft_h_FPerBtu,
                ValueInSqM_CPerkW
            };
        }

        #endregion
    }
}
