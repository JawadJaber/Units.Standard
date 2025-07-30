using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Units.Standard
{

    public interface IComment
    {
        string Comment { get; set; }
    }

    public class TemperatureItem : IUnit, ITemperature, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<TemperatureItem>
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

        public TemperatureItem()
        {

        }

        public void UpdateIfZero()
        {
            if(this.Value == 0)
            {
                UpdateWhenValueChanged();
            }
            
        }

        private TemperatureItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
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

        public int CompareTo(object obj)
        {
            if (obj is TemperatureItem)
            {
                return this.CompareTo((TemperatureItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(TemperatureItem other)
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


        public const string Name = nameof(TemperatureItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N1") + " " + Unit;
        }

        public static TemperatureItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.C);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
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

        public static TemperatureItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return TemperatureItem.Factory.Create(r, unit);
                }
                else
                {
                    return TemperatureItem.Factory.Create(r, DefaultUnit.Instance.DefaultAirTemperatureUnit);
                }

            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    if (!string.IsNullOrWhiteSpace(unit))
                    {
                        return TemperatureItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return TemperatureItem.Factory.Create(v, DefaultUnit.Instance.DefaultAirTemperatureUnit);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return TemperatureItem.Factory.Create(0, unit);
                }
                else
                {
                    return TemperatureItem.Factory.Create(0, DefaultUnit.Instance.DefaultAirTemperatureUnit);
                }


            }
        }


        public static TemperatureItem Parse(string s, IHashable hashable,IRandomHashCode randomHashCode, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName,randomHashCode.RandomHashCode);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return TemperatureItem.Factory.Create(r, unit);
                }
                else
                {
                    return TemperatureItem.Factory.Create(r, DefaultUnit.Instance.DefaultAirTemperatureUnit);
                }

            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    if (!string.IsNullOrWhiteSpace(unit))
                    {
                        return TemperatureItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return TemperatureItem.Factory.Create(v, DefaultUnit.Instance.DefaultAirTemperatureUnit);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return TemperatureItem.Factory.Create(0, unit);
                }
                else
                {
                    return TemperatureItem.Factory.Create(0, U.C);
                }


            }
        }


        private string _Comment { get; set; } = string.Empty;
        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                if (_Comment == value)
                    return;
                _Comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        [JsonProperty("StringValue")]
        private string _StringValue { get; set; } = string.Empty;
        [JsonIgnore]
        public string StringValue
        {
            get
            {
                return _StringValue;
            }
            set
            {
                if (_StringValue == value)
                    return;
                _StringValue = value;
                OnPropertyChanged(nameof(StringValue));
                this.UpdateValueWhenStringValueChanged("");
            }
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.StringValue = Value.ToString();
        }
    }


}



