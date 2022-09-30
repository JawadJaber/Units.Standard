using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Units.Standard
{
    public class AltitudeItem : IUnit, IAltitude, INotifyPropertyChanged, ILiquidizable,IComparable,IComparable<AltitudeItem>
    {
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
            if (Unit == U.m)
            {
                Value = ValueInM;
            }
            else if (Unit == U.ft)
            {
                Value = ValueInFt;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.m)
            {
                double valueInM = Value;
                ValueInM = valueInM;
                ValueInFt = Converter.ConvertLengthFrom_M_To_Ft(valueInM);

            }
            else if (Unit == U.ft)
            {
                double valueInFt = Value;
                ValueInFt = valueInFt;
                ValueInM = Converter.ConvertLengthFrom_Ft_To_M(valueInFt);
            }

          
        }

        private double _ValueInM { get; set; }
        public double ValueInM
        {
            get
            {
                return _ValueInM;
            }
            set
            {
                if (_ValueInM == value)
                    return;
                _ValueInM = value;
                OnPropertyChanged(nameof(ValueInM));
            }
        }

        private double _ValueInFt { get; set; }
        public double ValueInFt
        {
            get
            {
                return _ValueInFt;
            }
            set
            {
                if (_ValueInFt == value)
                    return;
                _ValueInFt = value;
                OnPropertyChanged(nameof(ValueInFt));
            }
        }

        #region Construction

        public AltitudeItem()
        {

        }

        public AltitudeItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        public static class Factory
        {
            public static AltitudeItem Create(double value, string unit) { return new AltitudeItem(value, unit); }
        }


        public static AltitudeItem Create(AltitudeItem item)
        {
            return AltitudeItem.Factory.Create(item.Value, item.Unit);
        }

        #endregion


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
                Unit,
                Value = Value.ToString("2"),
                ValueInFt,
                ValueInM,

            };
        }


        public int CompareTo(object obj)
        {
            if (obj is AltitudeItem)
            {
                return this.CompareTo((AltitudeItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(AltitudeItem other)
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
            list.Add(U.ft);
            list.Add(U.m);
            return list;
        }


        public const string Name = nameof(AltitudeItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public static AltitudeItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return AltitudeItem.Factory.Create(r, U.m);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return AltitudeItem.Factory.Create(v, U.m);
                }

                return AltitudeItem.Factory.Create(0, U.m);
            }
        }


        public static string OwnerUnitPropertyName = "AltitudeUnit";

        public static AltitudeItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return AltitudeItem.Factory.Create(r, unit);
                }
                else
                {
                    return AltitudeItem.Factory.Create(r, U.m);
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
                        return AltitudeItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return AltitudeItem.Factory.Create(v, U.m);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return AltitudeItem.Factory.Create(0, unit);
                }
                else
                {
                    return AltitudeItem.Factory.Create(0, U.m);
                }


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
