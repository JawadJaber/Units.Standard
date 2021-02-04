using DotLiquid;
using System.ComponentModel;

namespace Units.Standard
{
    public class FinsSpacingItem : IFinsSpacing, INotifyPropertyChanged, IUnit, ILiquidizable
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


        #region IFinsSpacing



        private double _ValueInFPI { get; set; }
        public double ValueInFPI
        {
            get
            {
                return _ValueInFPI;
            }
            set
            {
                if (_ValueInFPI == value)
                    return;
                _ValueInFPI = value;
                OnPropertyChanged(nameof(ValueInFPI));
            }
        }


        private double _ValueInFPMM { get; set; }
        public double ValueInFPMM
        {
            get
            {
                return _ValueInFPMM;
            }
            set
            {
                if (_ValueInFPMM == value)
                    return;
                _ValueInFPMM = value;
                OnPropertyChanged(nameof(ValueInFPMM));
            }
        }


        private double _ValueInFPM { get; set; }
        public double ValueInFPM
        {
            get
            {
                return _ValueInFPM;
            }
            set
            {
                if (_ValueInFPM == value)
                    return;
                _ValueInFPM = value;
                OnPropertyChanged(nameof(ValueInFPM));
            }
        }


        #endregion


        #region IUnits



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
                case U.FPI:
                    Value = ValueInFPI;
                    break;
                case U.FPM:
                    Value = ValueInFPM;
                    break;
                case U.FPMM:
                    Value = ValueInFPMM;
                    break;
                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {


            switch (Unit)
            {
                case U.FPI:
                    double valueInFPI = Value;
                    ValueInFPI = valueInFPI;
                    ValueInFPM = Converter.Convert_FinsPerInch_To_FinsPerMeter(valueInFPI);
                    ValueInFPMM = Converter.Convert_FinsPerInch_To_FinsPerMeter(valueInFPI) / 1000;
                    break;
                case U.FPM:
                    double valueInFPM = Value;
                    ValueInFPM = valueInFPM;
                    ValueInFPI = Converter.Convert_FinsPerMeter_To_FinsPerInch(valueInFPM);
                    ValueInFPMM = valueInFPM / 1000;
                    break;
                case U.FPMM:
                    double valueInFPMM = Value;
                    ValueInFPMM = valueInFPMM;
                    ValueInFPI = Converter.Convert_FinsPerMeter_To_FinsPerInch(valueInFPMM * 1000);
                    ValueInFPM = valueInFPMM * 1000;
                    break;
                default:
                    break;
            }
        }



        #endregion


        #region Construction

        public FinsSpacingItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }


        public static class Factory
        {
            public static FinsSpacingItem Create(double value, string unit) { return new FinsSpacingItem(value, unit); }
        }

        public static FinsSpacingItem Create(FinsSpacingItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value,
                Unit,
                ValueInFPI,
                ValueInFPM,
                ValueInFPMM
            };
        }

        #endregion

    }
}
