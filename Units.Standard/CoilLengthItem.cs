using DotLiquid;
using System.ComponentModel;

namespace Units.Standard
{
    public class CoilLengthItem : INotifyPropertyChanged, IUnit, ICoilLength, ILiquidizable
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

        #region ICoilLength



        private double _ValueInInch { get; set; }
        public double ValueInInch
        {
            get
            {
                return _ValueInInch;
            }
            set
            {
                if (_ValueInInch == value)
                    return;
                _ValueInInch = value;
                OnPropertyChanged(nameof(ValueInInch));
            }
        }


        private double _ValueInMM { get; set; }
        public double ValueInMM
        {
            get
            {
                return _ValueInMM;
            }
            set
            {
                if (_ValueInMM == value)
                    return;
                _ValueInMM = value;
                OnPropertyChanged(nameof(ValueInMM));
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
                case U.inch:
                    Value = ValueInInch;
                    break;
                case U.mm:
                    Value = ValueInMM;
                    break;

                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.inch)
            {
                double valueInInch = Value;
                ValueInInch = valueInInch;
                ValueInMM = Converter.ConvertLengthFrom_In_To_M(valueInInch) * 1000;
            }
            else
            {
                double valueInMM = Value;
                ValueInMM = valueInMM;
                ValueInInch = Converter.ConvertLengthFrom_M_To_In(valueInMM / 1000);
            }
        }


        #endregion

        #region Construction

        public CoilLengthItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public CoilLengthItem()
        {

        }

        public object ToLiquid()
        {
            return new
            {
                Value,
                Unit,
                ValueInInch,
                ValueInMM
            };
        }

        #endregion
    }
}
