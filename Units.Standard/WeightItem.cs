﻿using DotLiquid;
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
    public interface IWeightItem
    {
        double ValueInLb { get; set; }
        double ValueInKg { get; set; }

        string Unit { get; set; }
    }

    public class WeightItem : IUnit, IWeightItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<WeightItem>
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
            if (Unit == U.Lb)
            {
                Value = ValueInLb;
            }
            else
            {
                Value = ValueInKg;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.Lb)
            {
                double valueInLB = Value;
                ValueInLb = valueInLB;
                ValueInKg = Converter.ConvertWeightFrom_LB_To_KG(valueInLB);
            }
            else
            {
                double valueInKG = Value;
                ValueInKg = valueInKG;
                ValueInLb = Converter.ConvertWeightFrom_KG_To_LB(valueInKG);
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

        private double _ValueInLbPerFt3 { get; set; }
        public double ValueInLb
        {
            get
            {
                return _ValueInLbPerFt3;
            }
            set
            {
                if (_ValueInLbPerFt3 == value)
                    return;
                _ValueInLbPerFt3 = value;
                OnPropertyChanged(nameof(ValueInLb));
            }
        }

        private double _ValueInKgPerM3 { get; set; }
        public double ValueInKg
        {
            get
            {
                return _ValueInKgPerM3;
            }
            set
            {
                if (_ValueInKgPerM3 == value)
                    return;
                _ValueInKgPerM3 = value;
                OnPropertyChanged(nameof(ValueInKg));
            }
        }

        public WeightItem(double valueInLb, double valueInKg, string unit)
        {

            if ((unit == U.Lb) && valueInKg != Converter.ConvertWeightFrom_LB_To_KG(valueInLb))
            {
                valueInKg = Converter.ConvertWeightFrom_LB_To_KG(valueInLb);
            }

            if (unit == U.Kg && valueInLb != Converter.ConvertWeightFrom_KG_To_LB(valueInKg))
            {
                valueInLb = Converter.ConvertWeightFrom_KG_To_LB(valueInKg);
            }

            this.ValueInKg = valueInKg;
            this.ValueInLb = valueInLb;
            this.Unit = unit;
        }

        private WeightItem()
        {

        }

        private WeightItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        public static class Factory
        {
            public static WeightItem Create(double value, string unit) { return new WeightItem(value, unit); }
        }

        public static WeightItem Create(WeightItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInKg,
                ValueInLb
            };
        }


        public int CompareTo(object obj)
        {
            if (obj is WeightItem)
            {
                return this.CompareTo((WeightItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(WeightItem other)
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
            list.Add(U.Kg);
            list.Add(U.Lb);

            return list;
        }


        public const string Name = nameof(WeightItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public static WeightItem Parse(string s, IFormatProvider formatProvider)
        {
            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.Kg);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);
                if (isNumber)
                {
                    return Factory.Create(v, U.Kg);
                }

                return Factory.Create(0, U.Kg);
            }
        }

        public static string OwnerUnitPropertyName = "WeightUnit";

        public static WeightItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return WeightItem.Factory.Create(r, unit);
                }
                else
                {
                    return WeightItem.Factory.Create(r, U.Kg);
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
                        return WeightItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return WeightItem.Factory.Create(v, U.Kg);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return WeightItem.Factory.Create(0, unit);
                }
                else
                {
                    return WeightItem.Factory.Create(0, U.Kg);
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
