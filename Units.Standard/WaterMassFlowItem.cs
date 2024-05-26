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

    public interface IWaterMassFlow
    {
        double ValueInIbPerHr { get; set; }
        double ValueInKgPerHr { get; set; }
        string Unit { get; set; }
    }

    public class WaterMassFlowItem : IUnit, IWaterMassFlow, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<WaterMassFlowItem>
    {
        public WaterMassFlowItem(double valueInIbPerHr, double valueInKgPerHr, string unit)
        {
            if (unit == U.IbPerHr)
            {
                ValueInIbPerHr = valueInIbPerHr;
                ValueInKgPerHr = Converter.ConvertMassFlowRateFrom_IbPerHr_To_KgPerHr(valueInIbPerHr);
                Unit = unit;
            }
            else if (unit == U.KgPerHr)
            {
                ValueInKgPerHr = valueInKgPerHr;
                ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(valueInIbPerHr);
                
                Unit = unit;
            }
            

        }

        public WaterMassFlowItem()
        {

        }

        public WaterMassFlowItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        public static class Factory
        {
            public static WaterMassFlowItem Create(double value, string unit) { return new WaterMassFlowItem(value, unit); }
        }

        public static WaterMassFlowItem Create(WaterMassFlowItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }


        #region Properties

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


        private double _ValueInIbPerHr { get; set; }
        public double ValueInIbPerHr
        {
            get
            {
                return _ValueInIbPerHr;
            }
            set
            {
                if (_ValueInIbPerHr == value)
                    return;
                _ValueInIbPerHr = value;
                OnPropertyChanged(nameof(ValueInIbPerHr));
            }
        }

      

        private double _ValueInMPS { get; set; }
        public double ValueInKgPerHr
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
                OnPropertyChanged(nameof(ValueInKgPerHr));
            }
        }

        #endregion

        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.IbPerHr || Unit == "IbPerHr")
            {
                Value = ValueInIbPerHr;
            }
            else if (Unit == U.KgPerHr)
            {
                Value = ValueInKgPerHr;
            }

        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.IbPerHr || Unit == "IbPerHr")
            {
                double valueInIbPerHr = Value;
                ValueInIbPerHr = valueInIbPerHr;
                ValueInKgPerHr = Converter.ConvertMassFlowRateFrom_IbPerHr_To_KgPerHr(valueInIbPerHr);
                
            }
           
            else if (Unit == U.KgPerHr)
            {
                double valueInKgPerHr = Value;
                ValueInKgPerHr = valueInKgPerHr;
                ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(valueInKgPerHr);
                
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
                Value = Value.ToString("N1"),
                Unit,
                ValueInIbPerHr,
                ValueInKgPerHr
            };
        }

        public int CompareTo(object obj)
        {
            if (obj is WaterMassFlowItem)
            {
                return this.CompareTo((WaterMassFlowItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(WaterMassFlowItem other)
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
            list.Add(U.IbPerHr);
            list.Add(U.KgPerHr);

            return list;
        }

        public static List<string> GetProofUnits()
        {
            var list = new List<string>();
            list.Add(U.IbPerHr);
            list.Add(U.KgPerHr);

            return list;
        }


        public const string Name = nameof(WaterMassFlowItem);

        public static List<string> AllUnits { get; set; } = GetUnits();
        public static List<string> AllProofUnits { get; set; } = GetProofUnits();

        public override string ToString()
        {
            if (Unit == U.KgPerHr)
            {

                return Value.ToString("N5") + " " + Unit;
            }
            else
            {
                return Value.ToString("N2") + " " + Unit;

            }
        }


        public static WaterMassFlowItem Parse(string s, IFormatProvider formatProvider)
        {
            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.KgPerHr);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);
                if (isNumber)
                {
                    return Factory.Create(v, U.KgPerHr);
                }

                return Factory.Create(0, U.KgPerHr);
            }
        }

        public static string OwnerUnitPropertyName = "WaterMassFlowUnit";

        public static WaterMassFlowItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return WaterMassFlowItem.Factory.Create(r, unit);
                }
                else
                {
                    return WaterMassFlowItem.Factory.Create(r, U.KgPerHr);
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
                        return WaterMassFlowItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return WaterMassFlowItem.Factory.Create(v, U.KgPerHr);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return WaterMassFlowItem.Factory.Create(0, unit);
                }
                else
                {
                    return WaterMassFlowItem.Factory.Create(0, U.KgPerHr);
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
