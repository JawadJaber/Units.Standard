using DotLiquid;
using System.ComponentModel;

namespace Units.Standard
{
    public class AirFlowItem : IUnit, IAirFlow, INotifyPropertyChanged, ILiquidizable
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


        #region Properties

        private double _ValueInCFM { get; set; }
        public double ValueInCFM
        {
            get
            {
                return _ValueInCFM;
            }
            set
            {
                if (_ValueInCFM == value)
                    return;
                _ValueInCFM = value;
                OnPropertyChanged(nameof(ValueInCFM));
            }
        }

        private double _ValueInCMH { get; set; }
        public double ValueInCMH
        {
            get
            {
                return _ValueInCMH;
            }
            set
            {
                if (_ValueInCMH == value)
                    return;
                _ValueInCMH = value;
                OnPropertyChanged(nameof(ValueInCMH));
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
        public double ValueInMPS
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
                OnPropertyChanged(nameof(ValueInMPS));
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


        #endregion


        #region Construction

        public AirFlowItem(double valueInCFM, double valueInMPS, double valueInCMH, double valueInLPS, string unit)
        {
            if (unit == U.CFM)
            {
                ValueInCFM = valueInCFM;
                ValueInMPS = Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM);
                ValueInCMH = Converter.ConvertAirFlowFrom_MPS_To_CMH(Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM));
                ValueInLPS = Converter.ConvertAirFlowFrom_MPS_To_LPS(Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM));
                Unit = unit;
            }
            else if (unit == U.MPS)
            {
                ValueInMPS = valueInMPS;
                ValueInCFM = Converter.ConvertAirFlowFrom_MPS_To_CFM(valueInMPS);
                ValueInCMH = Converter.ConvertAirFlowFrom_MPS_To_CMH(valueInMPS);
                ValueInLPS = Converter.ConvertAirFlowFrom_MPS_To_LPS(valueInMPS);
                Unit = unit;
            }
            else if (unit == U.CMH)
            {
                ValueInCMH = valueInCMH;
                ValueInCFM = Converter.ConvertAirFlowFrom_MPS_To_CFM(Converter.ConvertAirFlowFrom_CMH_To_MPS(valueInCMH));
                ValueInLPS = Converter.ConvertAirFlowFrom_MPS_To_LPS(Converter.ConvertAirFlowFrom_CMH_To_MPS(valueInCMH));
                ValueInMPS = Converter.ConvertAirFlowFrom_CMH_To_MPS(valueInCMH);
                Unit = unit;
            }
            else if (unit == U.LPS)
            {
                ValueInLPS = valueInLPS;
                ValueInCFM = Converter.ConvertAirFlowFrom_MPS_To_CFM(Converter.ConvertAirFlowFrom_LPS_To_MPS(valueInLPS));
                ValueInCMH = Converter.ConvertAirFlowFrom_MPS_To_CMH(Converter.ConvertAirFlowFrom_LPS_To_MPS(valueInLPS));
                ValueInMPS = Converter.ConvertAirFlowFrom_LPS_To_MPS(valueInLPS);
                Unit = unit;
            }
        }

        private AirFlowItem()
        {

        }

        public AirFlowItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        #endregion


        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.CFM)
            {
                Value = ValueInCFM;
            }
            else if (Unit == U.CMH)
            {
                Value = ValueInCMH;
            }
            else if (Unit == U.MPS)
            {
                Value = ValueInMPS;
            }
            else if (Unit == U.LPS)
            {
                Value = ValueInLPS;
            }

        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.CFM)
            {
                double valueInCFM = Value;
                ValueInCFM = valueInCFM;
                ValueInCMH = Converter.ConvertAirFlowFrom_MPS_To_CMH(Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM));
                ValueInLPS = Converter.ConvertAirFlowFrom_MPS_To_LPS(Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM));
                ValueInMPS = Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM);

            }
            else if (Unit == U.CMH)
            {
                double valueInCMH = Value;
                ValueInCMH = valueInCMH;
                ValueInCFM = Converter.ConvertAirFlowFrom_MPS_To_CFM(Converter.ConvertAirFlowFrom_CMH_To_MPS(valueInCMH));
                ValueInLPS = Converter.ConvertAirFlowFrom_MPS_To_LPS(Converter.ConvertAirFlowFrom_CMH_To_MPS(valueInCMH));
                ValueInMPS = Converter.ConvertAirFlowFrom_CMH_To_MPS(valueInCMH);
            }
            else if (Unit == U.LPS)
            {
                double valueInLPS = Value;
                ValueInLPS = valueInLPS;
                ValueInCMH = Converter.ConvertAirFlowFrom_MPS_To_CMH(Converter.ConvertAirFlowFrom_LPS_To_MPS(valueInLPS));
                ValueInCFM = Converter.ConvertAirFlowFrom_MPS_To_CFM(Converter.ConvertAirFlowFrom_LPS_To_MPS(valueInLPS));
                ValueInMPS = Converter.ConvertAirFlowFrom_LPS_To_MPS(valueInLPS);
            }
            else if (Unit == U.MPS)
            {
                double valueInMPS = Value;
                ValueInMPS = valueInMPS;
                ValueInCMH = Converter.ConvertAirFlowFrom_MPS_To_CMH(valueInMPS);
                ValueInLPS = Converter.ConvertAirFlowFrom_MPS_To_LPS(valueInMPS);
                ValueInCFM = Converter.ConvertAirFlowFrom_MPS_To_CFM(valueInMPS);
            }
        }

        public static class Factory
        {
            public static AirFlowItem Create(double value, string unit) { return new AirFlowItem(value, unit); }
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInCFM,
                ValueInCMH,
                ValueInLPS,
                ValueInMPS
            };
        }
    }
}
