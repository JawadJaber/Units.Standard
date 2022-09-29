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
    public class CapacityItem : IUnit, INotifyPropertyChanged, ICapacity, ILiquidizable, IComparable, IComparable<CapacityItem>
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


        public static CapacityItem Create(CapacityItem item)
        {
            return CapacityItem.Factory.Create(item.Value, item.Unit);
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
                ValueWithUnit = $"{Value.ToString("N2")} {Unit}"

            };
        }


        public int CompareTo(object obj)
        {
            if (obj is CapacityItem)
            {
                return this.CompareTo((CapacityItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(CapacityItem other)
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
            list.Add(U.kW);
            list.Add(U.MBH);
            list.Add(U.TR);
            return list;
        }


        public const string Name = nameof(CapacityItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("#.##") + " " + Unit;
        }

        public static CapacityItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.TR);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, U.TR);
                }

                return Factory.Create(0, U.TR);
            }
        }


        public static string OwnerUnitPropertyName = "CapacityUnit";

        public static CapacityItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return CapacityItem.Factory.Create(r, unit);
                }
                else
                {
                    return CapacityItem.Factory.Create(r, U.LpS);
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
                        return CapacityItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return CapacityItem.Factory.Create(v, U.TR);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return CapacityItem.Factory.Create(0, unit);
                }
                else
                {
                    return CapacityItem.Factory.Create(0, U.TR);
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
