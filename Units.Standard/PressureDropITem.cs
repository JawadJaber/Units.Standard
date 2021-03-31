using DotLiquid;
using System;
using System.ComponentModel;

namespace Units.Standard
{
    public class PressureDropITem : INotifyPropertyChanged, IUnit, IPressureDrop, ILiquidizable, IComparable, IComparable<PressureDropITem>
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

        public PressureDropITem()
        {

        }

        public PressureDropITem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static PressureDropITem Create(double value, string unit) { return new PressureDropITem(value, unit); }
        }

        public static PressureDropITem Create(PressureDropITem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        #endregion

        #region IPressureDrop



        private double _ValueInPa { get; set; }
        public double ValueInPa
        {
            get
            {
                return _ValueInPa;
            }
            set
            {
                if (_ValueInPa == value)
                    return;
                _ValueInPa = value;
                OnPropertyChanged(nameof(ValueInPa));
            }
        }


        private double _ValueInInWG { get; set; }
        public double ValueInInWG
        {
            get
            {
                return _ValueInInWG;
            }
            set
            {
                if (_ValueInInWG == value)
                    return;
                _ValueInInWG = value;
                OnPropertyChanged(nameof(ValueInInWG));
            }
        }


        private double _ValueInKPa { get; set; }
        public double ValueInKPa
        {
            get
            {
                return _ValueInKPa;
            }
            set
            {
                if (_ValueInKPa == value)
                    return;
                _ValueInKPa = value;
                OnPropertyChanged(nameof(ValueInKPa));
            }
        }


        private double _ValueInPSI { get; set; }
        public double ValueInPSI
        {
            get
            {
                return _ValueInPSI;
            }
            set
            {
                if (_ValueInPSI == value)
                    return;
                _ValueInPSI = value;
                OnPropertyChanged(nameof(ValueInPSI));
            }
        }



        private double _ValueInFtWG { get; set; }
        public double ValueInFtWG
        {
            get
            {
                return _ValueInFtWG;
            }
            set
            {
                if (_ValueInFtWG == value)
                    return;
                _ValueInFtWG = value;
                OnPropertyChanged(nameof(ValueInFtWG));
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
                case U.inWG:
                    Value = ValueInInWG;
                    break;
                case U.ftWG:
                    Value = ValueInFtWG;
                    break;
                case U.Pa:
                    Value = ValueInPa;
                    break;
                case U.kPa:
                    Value = ValueInKPa;
                    break;
                case U.PSI:
                    Value = ValueInPSI;
                    break;
                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            switch (Unit)
            {
                case U.Pa:
                    double valueInPa = Value;
                    ValueInPa = valueInPa;
                    ValueInInWG = Converter.ConvertPressureDropFrom_Pa_To_INWG(valueInPa);
                    ValueInKPa = valueInPa / 1000;
                    ValueInPSI = Converter.ConvertPressureDropFrom_Pa_To_PSI(valueInPa);
                    ValueInFtWG = Converter.ConvertPressureDropFrom_kPa_To_FtWG(ValueInKPa);
                    break;
                case U.inWG:
                    double valueInInWG = Value;
                    ValueInInWG = valueInInWG;
                    ValueInPa = Converter.ConvertPressureDropFrom_InWG_ToPa(valueInInWG);
                    ValueInKPa = Converter.ConvertPressureDropFrom_InWG_ToPa(valueInInWG) / 1000;
                    ValueInPSI = Converter.ConvertPressureDropFrom_Pa_To_PSI(Converter.ConvertPressureDropFrom_InWG_ToPa(valueInInWG));
                    ValueInFtWG = Converter.ConvertPressureDropFrom_kPa_To_FtWG(ValueInKPa);
                    break;
                case U.kPa:
                    double valueInKPa = Value;
                    ValueInKPa = valueInKPa;
                    ValueInPa = valueInKPa * 1000;
                    ValueInInWG = Converter.ConvertPressureDropFrom_Pa_To_INWG(valueInKPa * 1000);
                    ValueInPSI = Converter.ConvertPressureDropFrom_Pa_To_PSI(valueInKPa * 1000);
                    ValueInFtWG = Converter.ConvertPressureDropFrom_kPa_To_FtWG(ValueInKPa);
                    break;
                case U.PSI:
                    double valueInPSI = Value;
                    ValueInPSI = valueInPSI;
                    ValueInPa = Converter.ConvertPressureDropFrom_PSI_ToPa(valueInPSI);
                    ValueInInWG = Converter.ConvertPressureDropFrom_Pa_To_INWG(Converter.ConvertPressureDropFrom_PSI_ToPa(valueInPSI));
                    ValueInKPa = ValueInPa / 1000;
                    ValueInFtWG = Converter.ConvertPressureDropFrom_kPa_To_FtWG(ValueInKPa);
                    break;
                case U.ftWG:
                    double valueInFtWG = Value;
                    ValueInFtWG = valueInFtWG;
                    ValueInPa = Converter.ConvertPressureFrom_FtWG_To_kPa(valueInFtWG) * 1000;
                    ValueInInWG = Converter.ConvertPressureDropFrom_Pa_To_INWG(Converter.ConvertPressureFrom_FtWG_To_kPa(valueInFtWG) * 1000);
                    ValueInKPa = ValueInPa / 1000;
                    break;
                default:
                    break;
            }

        }


        public double GetValue(string Unit)
        {
            switch (Unit)
            {
                case U.Pa:
                    return ValueInPa;
                    break;
                case U.inWG:
                    return ValueInInWG;
                    break;
                case U.kPa:
                    return ValueInKPa;
                    break;
                case U.PSI:
                    return ValueInPSI;
                    break;
                default:
                    return 0;
                    break;
            }
        }

        #endregion


        public object ToLiquid()
        {
            return new
            {
                Unit,
                Value = Value.ToString("N2"),
                ValueInInWG,
                ValueInPa,
                ValueInPSI
            };
        }


        public int CompareTo(object obj)
        {
            if (obj is PressureDropITem)
            {
                return this.CompareTo((PressureDropITem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(PressureDropITem other)
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
