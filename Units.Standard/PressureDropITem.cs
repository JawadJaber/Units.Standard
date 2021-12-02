using DotLiquid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

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



        private double _ValueInBar { get; set; }
        public double ValueInBar
        {
            get
            {
                return _ValueInBar;
            }
            set
            {
                if (_ValueInBar == value)
                    return;
                _ValueInBar = value;
                OnPropertyChanged(nameof(ValueInBar));
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
                case U.Bar:
                    Value = ValueInBar;
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
                    ValueInBar = valueInPa / 100000;
                    break;
                case U.inWG:
                    double valueInInWG = Value;
                    ValueInInWG = valueInInWG;
                    ValueInPa = Converter.ConvertPressureDropFrom_InWG_ToPa(valueInInWG);
                    ValueInBar = Converter.ConvertPressureDropFrom_InWG_ToPa(valueInInWG)/100000;
                    ValueInKPa = Converter.ConvertPressureDropFrom_InWG_ToPa(valueInInWG) / 1000;
                    ValueInPSI = Converter.ConvertPressureDropFrom_Pa_To_PSI(Converter.ConvertPressureDropFrom_InWG_ToPa(valueInInWG));
                    ValueInFtWG = Converter.ConvertPressureDropFrom_kPa_To_FtWG(ValueInKPa);
                    break;
                case U.kPa:
                    double valueInKPa = Value;
                    ValueInKPa = valueInKPa;
                    ValueInPa = valueInKPa * 1000;
                    ValueInBar = valueInKPa / 100;
                    ValueInInWG = Converter.ConvertPressureDropFrom_Pa_To_INWG(valueInKPa * 1000);
                    ValueInPSI = Converter.ConvertPressureDropFrom_Pa_To_PSI(valueInKPa * 1000);
                    ValueInFtWG = Converter.ConvertPressureDropFrom_kPa_To_FtWG(ValueInKPa);
                    break;
                case U.PSI:
                    double valueInPSI = Value;
                    ValueInPSI = valueInPSI;
                    ValueInPa = Converter.ConvertPressureDropFrom_PSI_ToPa(valueInPSI);
                    ValueInBar = Converter.ConvertPressureDropFrom_PSI_ToPa(valueInPSI)/100000;
                    ValueInInWG = Converter.ConvertPressureDropFrom_Pa_To_INWG(Converter.ConvertPressureDropFrom_PSI_ToPa(valueInPSI));
                    ValueInKPa = ValueInPa / 1000;
                    ValueInFtWG = Converter.ConvertPressureDropFrom_kPa_To_FtWG(ValueInKPa);
                    break;
                case U.ftWG:
                    double valueInFtWG = Value;
                    ValueInFtWG = valueInFtWG;
                    ValueInPa = Converter.ConvertPressureFrom_FtWG_To_kPa(valueInFtWG) * 1000;
                    ValueInBar = Converter.ConvertPressureFrom_FtWG_To_kPa(valueInFtWG) /100;
                    ValueInInWG = Converter.ConvertPressureDropFrom_Pa_To_INWG(Converter.ConvertPressureFrom_FtWG_To_kPa(valueInFtWG) * 1000);
                    ValueInKPa = ValueInPa / 1000;
                    ValueInPSI = Converter.ConvertPressureDropFrom_Pa_To_PSI(Converter.ConvertPressureFrom_FtWG_To_kPa(valueInFtWG) * 1000);
                    break;
                case U.Bar:
                    double valueInBar = Value;
                    ValueInBar = valueInBar;
                    ValueInPa = valueInBar * 100000;
                    ValueInFtWG = Converter.ConvertPressureDropFrom_kPa_To_FtWG(valueInBar * 100) ;
                    ValueInInWG = Converter.ConvertPressureDropFrom_Pa_To_INWG(valueInBar * 100000);
                    ValueInKPa = valueInBar*100;
                    ValueInPSI = Converter.ConvertPressureDropFrom_Pa_To_PSI(valueInBar * 100000);
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
                case U.Bar:
                    return ValueInBar;
                    break;
                case U.ftWG:
                    return ValueInFtWG;
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
                ValueInPSI,
                ValueInBar
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


        public static List<string> GetUnits()
        {
            var list = new List<string>();
            list.Add(U.Pa);
            list.Add(U.inWG);
            list.Add(U.PSI);

            return list;
        }

        public static List<string> GetWaterUnits()
        {
            var list = new List<string>();
            list.Add(U.kPa);
            list.Add(U.ftWG);
            list.Add(U.Bar);
            list.Add(U.PSI);

            return list;
        }

        public static List<string> AllUnits { get; set; } = GetUnits();

        public static List<string> AllUnits_Water { get; set; } = GetWaterUnits();


        public const string Name = nameof(PressureDropITem);


        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public static PressureDropITem Parse(string s, IFormatProvider formatProvider)//default value to be added to distinquish between water and air pressure drops,,, same as air flow item.
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.kPa);
            }
            else
            {
                Regex regex = new Regex(@"\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, U.kPa);
                }

                return Factory.Create(0, U.kPa);
            }
        }
    }
}
