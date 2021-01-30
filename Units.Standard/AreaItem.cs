using DotLiquid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Units.Standard
{
    public interface IAreaItem
    {
        double ValueInSqM { get; set; }
        double ValueInSqFt { get; set; }
        double ValueInSqIn { get; set; }
    }
    public class AreaItem : IAreaItem, IUnit, ILiquidizable,INotifyPropertyChanged
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



        #region IAreaItem






        private double _ValueInSqM { get; set; } = 0;
        public double ValueInSqM
        {
            get
            {
                return _ValueInSqM;
            }
            set
            {
                if (_ValueInSqM == value)
                    return;
                _ValueInSqM = value;
                OnPropertyChanged(nameof(ValueInSqM));
            }
        }


        private double _ValueInSqFt { get; set; } = 0;
        public double ValueInSqFt
        {
            get
            {
                return _ValueInSqFt;
            }
            set
            {
                if (_ValueInSqFt == value)
                    return;
                _ValueInSqFt = value;
                OnPropertyChanged(nameof(ValueInSqFt));
            }
        }


        private double _ValueInSqIn { get; set; } = 0;
        public double ValueInSqIn
        {
            get
            {
                return _ValueInSqIn;
            }
            set
            {
                if (_ValueInSqIn == value)
                    return;
                _ValueInSqIn = value;
                OnPropertyChanged(nameof(ValueInSqIn));
            }
        }


        #endregion

        #region IUnit



        private string _Unit { get; set; } = string.Empty;
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


        private double _Value { get; set; } = 0;
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



        #endregion

        #region Construction

        public AreaItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static AreaItem Create(double value, string unit) { return new AreaItem(value, unit); }
        }

        #endregion

        public object ToLiquid()
        {
            throw new NotImplementedException();
        }

        public void UpdateWhenUnitChanged()
        {
            switch (Unit)
            {
                case U.SqM:
                    Value = ValueInSqM;
                    break;
                case U.SqFt:
                    Value = ValueInSqFt;
                    break;
                case U.SqIn:
                    Value = ValueInSqIn;
                    break;
                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            switch (Unit)
            {
                case U.SqM:
                    double valueInSqM = Value;
                    ValueInSqM = valueInSqM;
                    ValueInSqFt = Converter.ConvertAreaFrom_SqM_To_SqFt(valueInSqM);
                    ValueInSqIn = Converter.ConvertAreaFrom_SqM_To_SqIn(valueInSqM);
                    break;
                case U.SqIn:
                    double valueInAqIn = Value;
                    ValueInSqIn = valueInAqIn;
                    ValueInSqFt = Converter.ConvertAreaFrom_SqIn_To_SqFt(valueInAqIn);
                    ValueInSqM = Converter.ConvertAreaFrom_SqIn_To_SqM(valueInAqIn);
                    break;
                case U.SqFt:
                    double valueInSqFt = Value;
                    ValueInSqFt = valueInSqFt;
                    ValueInSqIn = Converter.ConvertAreaFrom_SqFt_To_SqIn(valueInSqFt);
                    ValueInSqM = Converter.ConvertAreaFrom_SqFt_To_SqM(valueInSqFt);
                    break;
                default:
                    break;
            }
        }
    }
}
