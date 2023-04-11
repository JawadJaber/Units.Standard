using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Units.Standard
{
    public interface IFrequencyItem
    {
        double ValueInHZ { get; set; }
        double ValueInRPM { get; set; }
        double ValueInRPS { get; set; }
        string Unit { get; set; }
    }

    public class FrequencyItem: IUnit, IFrequencyItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<FrequencyItem>
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

        #region IFrequencyItem


        [JsonProperty("ValueInHZ")]
        private double _ValueInHZ { get; set; } 
        [JsonIgnore]
        public double ValueInHZ
        {
            get
            {
                return _ValueInHZ;
            }
            set
            {
                if (_ValueInHZ == value)
                    return;
                _ValueInHZ = value;
                OnPropertyChanged(nameof(ValueInHZ));
            }
        }


        [JsonProperty("ValueInRPM")]
        private double _ValueInRPM { get; set; } 
        [JsonIgnore]
        public double ValueInRPM
        {
            get
            {
                return _ValueInRPM;
            }
            set
            {
                if (_ValueInRPM == value)
                    return;
                _ValueInRPM = value;
                OnPropertyChanged(nameof(ValueInRPM));
            }
        }


        [JsonProperty("ValueInRPS")]
        private double _ValueInRPS { get; set; } 
        [JsonIgnore]
        public double ValueInRPS
        {
            get
            {
                return _ValueInRPS;
            }
            set
            {
                if (_ValueInRPS == value)
                    return;
                _ValueInRPS = value;
                OnPropertyChanged(nameof(ValueInRPS));
            }
        }


        [JsonProperty("Unit")]
        private string _Unit { get; set; } = string.Empty;
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

        public string StringValue { get; set; }



        #endregion


        #region Construction

        public static FrequencyItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return FrequencyItem.Factory.Create(r, U.RPM);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return FrequencyItem.Factory.Create(v, U.RPM);
                }

                return FrequencyItem.Factory.Create(0, U.RPM);
            }
        }


        public static FrequencyItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit("FrequencyUnit");

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return FrequencyItem.Factory.Create(r, unit);
                }
                else
                {
                    return FrequencyItem.Factory.Create(r, U.RPM);
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
                        return FrequencyItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return FrequencyItem.Factory.Create(v, U.RPM);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return FrequencyItem.Factory.Create(0, unit);
                }
                else
                {
                    return FrequencyItem.Factory.Create(0, U.RPM);
                }


            }
        }

        public FrequencyItem()
        {
            Unit = U.RPS;
            Value = 0;


        }

       


        public FrequencyItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }


        public static class Factory
        {
            public static FrequencyItem Create(double value, string unit) { return new FrequencyItem(value, unit); }
        }

        public static FrequencyItem Create(FrequencyItem item)
        {
            return FrequencyItem.Factory.Create(item.Value, item.Unit);
        }
        #endregion

        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.HZ)
            {
                Value = ValueInHZ;
            }
            else if (Unit == U.RPM)
            {
                Value = ValueInRPM;
            }
            else if (Unit == U.RPS)
            {
                Value = ValueInRPS;
            }
          
        }

        public void UpdateWhenValueChanged()
        {
             if (Unit == U.HZ)
            {
                double valueInHZ = Value;
                ValueInHZ = valueInHZ;
                ValueInRPM = Converter.ConvertAreaFrom_HZ_To_RPM(valueInHZ);
                ValueInRPS = valueInHZ;
            }
            else if (Unit == U.RPM)
            {
                double valueInRPM = Value;
                ValueInRPM = valueInRPM;
                ValueInHZ = Converter.ConvertAreaFrom_RPM_To_HZ(valueInRPM);
                ValueInRPS = Converter.ConvertAreaFrom_RPM_To_HZ(valueInRPM);
            }
            else if (Unit == U.RPS)
            {
                double valueInRPS = Value;
                ValueInRPS = valueInRPS;
                ValueInHZ = valueInRPS;
                ValueInRPM = Converter.ConvertAreaFrom_HZ_To_RPM(valueInRPS);
            }
          

        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInHZ,
                ValueInRPM,
                ValueInRPS,
                
            };
        }

        public int CompareTo(object obj)
        {
            if (obj is FrequencyItem)
            {
                return this.CompareTo((FrequencyItem)obj);
            }
            else
            {
                return 0;
            }
        }

        public int CompareTo(FrequencyItem other)
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
            list.Add(U.HZ);
            list.Add(U.RPS);
            list.Add(U.RPM);
         
            return list;
        }


        public const string Name = nameof(FrequencyItem);

        public static List<string> AllUnits { get; set; } = GetUnits();


        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.StringValue = Value.ToString();
        }

        public override string ToString()
        {
            return $"{Value} {Unit}";
        }

    }
}
