using DotLiquid;
using System.ComponentModel;

namespace Units.Standard
{
    public class CapacityItem : IUnit, INotifyPropertyChanged, ICapacity, ILiquidizable
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

        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.MBH)
            {
                Value = ValueInMBH;
            }
            else if (Unit == U.TR)
            {
                Value = ValueInTR;
            }
            else if (Unit == U.kW)
            {
                Value = ValueInKW;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.MBH)
            {
                double valueInMBH = Value;
                ValueInMBH = valueInMBH;
                ValueInKW = Converter.ConvertCapacityFrom_MBH_To_KW(valueInMBH);
                ValueInTR = Converter.ConvertCapacityFrom_KW_To_TR(Converter.ConvertCapacityFrom_MBH_To_KW(valueInMBH));
            }
            else if (Unit == U.kW)
            {
                double valueInKW = Value;
                ValueInKW = valueInKW;
                ValueInMBH = Converter.ConvertCapacityFrom_KW_To_MBH(valueInKW);
                ValueInTR = Converter.ConvertCapacityFrom_KW_To_TR(valueInKW);
            }
            else if (Unit == U.TR)
            {
                double valueInTR = Value;
                ValueInTR = valueInTR;
                ValueInKW = Converter.ConvertCapacityFrom_TR_To_KW(valueInTR);
                ValueInMBH = Converter.ConvertCapacityFrom_KW_To_MBH(Converter.ConvertCapacityFrom_TR_To_KW(valueInTR));
            }
        }

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

        private double _ValueInMBH { get; set; }
        public double ValueInMBH
        {
            get
            {
                return _ValueInMBH;
            }
            set
            {
                if (_ValueInMBH == value)
                    return;
                _ValueInMBH = value;
                OnPropertyChanged(nameof(ValueInMBH));
            }
        }

        private double _ValueInTR { get; set; }
        public double ValueInTR
        {
            get
            {
                return _ValueInTR;
            }
            set
            {
                if (_ValueInTR == value)
                    return;
                _ValueInTR = value;
                OnPropertyChanged(nameof(ValueInTR));
            }
        }

        private double _ValueInKW { get; set; }
        public double ValueInKW
        {
            get
            {
                return _ValueInKW;
            }
            set
            {
                if (_ValueInKW == value)
                    return;
                _ValueInKW = value;
                OnPropertyChanged(nameof(ValueInKW));
            }
        }


        public CapacityItem(double valueInMBH, double valueInTR, double valueInKW, string unit)
        {
            if (unit == U.MBH)
            {
                ValueInMBH = valueInMBH;
                ValueInTR = Converter.ConvertCapacityFrom_KW_To_TR(Converter.ConvertCapacityFrom_MBH_To_KW(valueInMBH));
                ValueInKW = Converter.ConvertCapacityFrom_MBH_To_KW(valueInMBH);
                Unit = unit;
            }

            if (unit == U.kW)
            {
                ValueInKW = valueInKW;
                ValueInTR = Converter.ConvertCapacityFrom_KW_To_TR(valueInKW);
                ValueInMBH = Converter.ConvertCapacityFrom_KW_To_MBH(valueInKW);
                Unit = unit;
            }

            if (unit == U.TR)
            {
                ValueInTR = valueInTR;
                ValueInMBH = Converter.ConvertCapacityFrom_KW_To_MBH(Converter.ConvertCapacityFrom_TR_To_KW(valueInTR));
                ValueInKW = Converter.ConvertCapacityFrom_TR_To_KW(valueInTR);
                Unit = unit;
            }
        }


        private CapacityItem()
        {

        }

        public CapacityItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static CapacityItem Create(double value, string unit) { return new CapacityItem(value, unit); }
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInKW,
                ValueInMBH,
                ValueInTR,

            };
        }
    }
}
