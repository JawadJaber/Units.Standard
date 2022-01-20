using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Units.Standard
{
    public class TemperatureDifferenceItem : INotifyPropertyChanged, IUnit, ITemperatureDifference, ILiquidizable, IComparable, IComparable<TemperatureDifferenceItem>
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

        public static TemperatureDifferenceItem Create(TemperatureDifferenceItem item)
        {
            return  Factory.Create(item.Value, item.Unit); 
        }

        #endregion

        #region IUnits


        [JsonProperty("Unit")]
        private string _Unit { get; set; }

        [JsonIgnore]
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

        [JsonProperty("Value")]
        private double _Value { get; set; }

        [JsonIgnore]
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


        public int CompareTo(object obj)
        {
            if (obj is TemperatureDifferenceItem)
            {
                return this.CompareTo((TemperatureDifferenceItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(TemperatureDifferenceItem other)
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
            list.Add(U.C);
            list.Add(U.F);

            return list;
        }


        public const string Name = nameof(TemperatureDifferenceItem);

        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public static TemperatureDifferenceItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.C);
            }
            else
            {
                Regex regex = new Regex(@"\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, U.C);
                }

                return Factory.Create(0, U.C);
            }
        }

        public static string OwnerUnitPropertyName = "TempUnit";

        public static TemperatureDifferenceItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return TemperatureDifferenceItem.Factory.Create(r, unit);
                }
                else
                {
                    return TemperatureDifferenceItem.Factory.Create(r, U.C);
                }

            }
            else
            {
                Regex regex = new Regex(@"\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    if (!string.IsNullOrWhiteSpace(unit))
                    {
                        return TemperatureDifferenceItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return TemperatureDifferenceItem.Factory.Create(v, U.C);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return TemperatureDifferenceItem.Factory.Create(0, unit);
                }
                else
                {
                    return TemperatureDifferenceItem.Factory.Create(0, U.C);
                }


            }
        }

    }
}
