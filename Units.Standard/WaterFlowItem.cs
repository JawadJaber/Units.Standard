using DotLiquid;
using System.ComponentModel;

namespace Units.Standard
{
    public class WaterFlowItem : IUnit, IWaterFlow, INotifyPropertyChanged, ILiquidizable
    {
        public WaterFlowItem(double valueInGPM, double valueInMPS, double valueInLPS, string unit)
        {
            if (unit == U.GPM)
            {
                ValueInGPM = valueInGPM;
                ValueInM3PS = Converter.ConvertWaterFlowFrom_GMP_To_M3PS(valueInGPM);
                ValueInLPS = Converter.ConvertWaterFlowFrom_M3PS_To_LPS(ValueInM3PS);
                Unit = unit;
            }
            else if (unit == U.M3PS)
            {
                ValueInM3PS = valueInMPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_M3PS_To_GPM(valueInMPS);
                ValueInLPS = Converter.ConvertWaterFlowFrom_M3PS_To_LPS(valueInMPS);
                Unit = unit;
            }
            else if (unit == U.LPS || unit == U.LpS)
            {
                ValueInLPS = valueInLPS;
                ValueInM3PS = Converter.ConvertWaterFlowFrom_LPS_To_M3PS(valueInLPS);
                ValueInGPM = Converter.ConvertWaterFlowFrom_M3PS_To_GPM(ValueInM3PS);
                Unit = unit;
            }

        }

        private WaterFlowItem()
        {

        }

        private WaterFlowItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static WaterFlowItem Create(double value, string unit) { return new WaterFlowItem(value, unit); }
        }


        #region Properties

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

        private double _ValueInGPM { get; set; }
        public double ValueInGPM
        {
            get
            {
                return _ValueInGPM;
            }
            set
            {
                if (_ValueInGPM == value)
                    return;
                _ValueInGPM = value;
                OnPropertyChanged(nameof(ValueInGPM));
            }
        }

        private double _ValueInLPS { get; set; }
        public double ValueInLPS
        {
            get
            {
                return _ValueInLPS;
            }
            set
            {
                if (_ValueInLPS == value)
                    return;
                _ValueInLPS = value;
                OnPropertyChanged(nameof(ValueInLPS));
            }
        }

        private double _ValueInMPS { get; set; }
        public double ValueInM3PS
        {
            get
            {
                return _ValueInMPS;
            }
            set
            {
                if (_ValueInMPS == value)
                    return;
                _ValueInMPS = value;
                OnPropertyChanged(nameof(ValueInM3PS));
            }
        }

        #endregion

        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.GPM || Unit == "GPM")
            {
                Value = ValueInGPM;
            }
            else if (Unit == U.LPS || Unit == U.LpS)
            {
                Value = ValueInLPS;
            }
            else if (Unit == U.M3PS)
            {
                Value = ValueInM3PS;
            }

        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.GPM || Unit == "GPM")
            {
                double valueInGPM = Value;
                ValueInGPM = valueInGPM;
                ValueInM3PS = Converter.ConvertWaterFlowFrom_GMP_To_M3PS(valueInGPM);
                ValueInLPS = Converter.ConvertWaterFlowFrom_M3PS_To_LPS(Converter.ConvertWaterFlowFrom_GMP_To_M3PS(valueInGPM));
            }
            else if (Unit == U.LPS || Unit == U.LpS)
            {
                double valueInLPS = Value;
                ValueInLPS = valueInLPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_M3PS_To_GPM(Converter.ConvertWaterFlowFrom_LPS_To_M3PS(valueInLPS));
                ValueInM3PS = Converter.ConvertWaterFlowFrom_LPS_To_M3PS(valueInLPS);
            }
            else if (Unit == U.M3PS)
            {
                double valueInMPS = Value;
                ValueInM3PS = valueInMPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_M3PS_To_GPM(valueInMPS);
                ValueInLPS = Converter.ConvertWaterFlowFrom_M3PS_To_LPS(valueInMPS);
            }

        }



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
        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("3"),
                Unit,
                ValueInGPM,
                ValueInLPS,
                ValueInM3PS
            };
        }
    }
}
