using DotLiquid;
using System.ComponentModel;

namespace Units.Standard
{
    public class TemperatureItem : IUnit, ITemperature, INotifyPropertyChanged, ILiquidizable
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
            if (Unit == U.F || Unit == "F")
            {
                Value = ValueInF;
            }
            else
            {
                Value = ValueInC;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.F || Unit == "F")
            {
                double valueInF = Value;
                ValueInF = valueInF;
                ValueInC = Converter.ConvertTempFrom_F_To_C(valueInF);
            }
            else
            {
                double valueInC = Value;
                ValueInC = valueInC;
                ValueInF = Converter.ConvertTempFrom_C_To_F(valueInC);
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

        public TemperatureItem(double valueInF, double valueInC, string unit)
        {

            if ((unit == U.F || unit == "F") && valueInC != Converter.ConvertTempFrom_F_To_C(valueInF))
            {
                valueInC = Converter.ConvertTempFrom_F_To_C(valueInF);
            }

            if (unit == U.C && valueInF != Converter.ConvertTempFrom_C_To_F(valueInC))
            {
                valueInF = Converter.ConvertTempFrom_C_To_F(valueInC);
            }

            this.ValueInC = valueInC;
            this.ValueInF = valueInF;
            this.Unit = unit;
        }

        private TemperatureItem()
        {

        }

        private TemperatureItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static TemperatureItem Create(double value, string unit) { return new TemperatureItem(value, unit); }
        }


        public static TemperatureItem Create(TemperatureItem item)
        {
            return TemperatureItem.Factory.Create(item.Value, item.Unit);
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
    }
}
