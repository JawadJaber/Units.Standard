using DotLiquid;
using System.ComponentModel;

namespace Units.Standard
{
    public class TemperatureDifferenceItem : INotifyPropertyChanged, IUnit, ITemperatureDifference, ILiquidizable
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


        #region Construction

        public TemperatureDifferenceItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }


        public static class Factory
        {
            public static TemperatureDifferenceItem Create(double value, string unit) { return new TemperatureDifferenceItem(value, unit); }
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
                case U.C:
                    Value = ValueInC;
                    break;
                case U.F:
                    Value = ValueInF;
                    break;
                case "F":
                    Value = ValueInF;
                    break;

                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.F || Unit == "F")
            {
                double valueInF = Value;
                ValueInF = valueInF;
                ValueInC = Converter.ConvertTempDifferenceFrom_deltaF_To_deltaC(valueInF);
            }
            else
            {
                double valueInC = Value;
                ValueInC = valueInC;
                ValueInF = Converter.ConvertTempDifferenceFrom_deltaC_To_deltaF(valueInC);
            }
        }



        #endregion


        #region ITemperatureDifference



        private double _ValueInC { get; set; }
        public double ValueInC
        {
            get
            {
                return _ValueInC;
            }
            set
            {
                if (_ValueInC == value)
                    return;
                _ValueInC = value;
                OnPropertyChanged(nameof(ValueInC));
            }
        }


        private double _ValueInF { get; set; }
        public double ValueInF
        {
            get
            {
                return _ValueInF;
            }
            set
            {
                if (_ValueInF == value)
                    return;
                _ValueInF = value;
                OnPropertyChanged(nameof(ValueInF));
            }
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInC,
                ValueInF
            };
        }





        #endregion

    }
}
