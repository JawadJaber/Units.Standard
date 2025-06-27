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
    public interface IRefrigerantMassFlow
    {
        double ValueInIbPerHr { get; set; }
        double ValueInKgPerHr { get; set; }
        double ValueInKgPerS { get; set; }
        double ValueInGPerS { get; set; }
        string Unit { get; set; }
    }

    public class RefrigerantMassFlowItem : IUnit, IRefrigerantMassFlow, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<RefrigerantMassFlowItem>
    {
        public RefrigerantMassFlowItem(double valueInIbPerHr, double valueInKgPerHr, double valueInGramPerSec,double valueInKgPerSec, string unit)
        {
            //if (unit == U.IbPerHr)
            //{
            //    ValueInIbPerHr = valueInIbPerHr;
            //    ValueInKgPerHr = Converter.ConvertMassFlowRateFrom_IbPerHr_To_KgPerHr(valueInIbPerHr);
            //    ValueInKgPerS = Converter.ConvertMassFlowRateFrom_IbPerHr_To_KgPerHr(valueInIbPerHr)/3600.0;
            //    ValueInGPerS = Converter.ConvertMassFlowRateFrom_IbPerHr_To_GPerS(valueInKgPerHr);

            //    Unit = unit;
            //}
            //else if (unit == U.KgPerHr)
            //{
            //    ValueInKgPerHr = valueInKgPerHr;
            //    ValueInKgPerS = valueInKgPerHr/3600.0;
            //    ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(valueInIbPerHr);
            //    ValueInGPerS = valueInKgPerHr / 3.6;
            //    Unit = unit;
            //}
            //else if (unit == U.GPerSec)
            //{
            //    ValueInKgPerS = valueInKgPerSec;
            //    ValueInKgPerHr = valueInKgPerSec * 3600.0;

            //    //ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_GPerS_To_IbPerHr(valueInGramPerSec);

            //    //Unit = unit;
            //}
            //else if (unit == U.KgPerS)
            //{
            //    ValueInGPerS = valueInGramPerSec;
            //    ValueInKgPerHr = valueInGramPerSec * 3.6;
            //    ValueInKgPerS = valueInGramPerSec * 3.6 / 3600.0;
            //    ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_GPerS_To_IbPerHr(valueInGramPerSec);

            //    Unit = unit;
            //}


            if (unit == U.IbPerHr)
            {
                ValueInIbPerHr = valueInIbPerHr;
                ValueInKgPerHr = Converter.ConvertMassFlowRateFrom_IbPerHr_To_KgPerHr(valueInIbPerHr);
                ValueInKgPerS = ValueInKgPerHr / 3600.0;
                ValueInGPerS = ValueInKgPerHr * 1000.0 / 3600.0;
            }
            else if (unit == U.KgPerHr)
            {
                ValueInKgPerHr = valueInKgPerHr;
                ValueInKgPerS = valueInKgPerHr / 3600.0;
                ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(valueInKgPerHr);
                ValueInGPerS = valueInKgPerHr * 1000.0 / 3600.0;
            }
            else if (unit == U.GPerSec)
            {
                ValueInGPerS = valueInGramPerSec;
                ValueInKgPerS = valueInGramPerSec / 1000.0;
                ValueInKgPerHr = ValueInKgPerS * 3600.0;
                ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(ValueInKgPerHr);
            }
            else if (unit == U.KgPerS)
            {
                ValueInKgPerS = valueInKgPerSec;
                ValueInKgPerHr = valueInKgPerSec * 3600.0;
                ValueInGPerS = valueInKgPerSec * 1000.0;
                ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(ValueInKgPerHr);
            }


        }

        public RefrigerantMassFlowItem()
        {

        }

        public RefrigerantMassFlowItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        public static class Factory
        {
            public static RefrigerantMassFlowItem Create(double value, string unit) { return new RefrigerantMassFlowItem(value, unit); }
        }

        public static RefrigerantMassFlowItem Create(RefrigerantMassFlowItem item)
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



        [JsonProperty("ValueInKgPerS")]
        private double _ValueInKgPerS { get; set; } = 0;
        [JsonIgnore]
        public double ValueInKgPerS
        {
            get
            {
                return _ValueInKgPerS;
            }
            set
            {
                if (_ValueInKgPerS == value)
                    return;
                _ValueInKgPerS = value;
                OnPropertyChanged(nameof(ValueInKgPerS));
            }
        }



        [JsonProperty("ValueInGPerS")]
        private double _ValueInGPerS { get; set; }
        [JsonIgnore]
        public double ValueInGPerS
        {
            get
            {
                return _ValueInGPerS;
            }
            set
            {
                if (_ValueInGPerS == value)
                    return;
                _ValueInGPerS = value;
                OnPropertyChanged(nameof(ValueInGPerS));
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
            else if (Unit == U.GPerSec)
            {
                Value = ValueInGPerS;
            }
            else if (Unit == U.KgPerS)
            {
                Value = ValueInKgPerS;
            }

        }

        //public void UpdateWhenValueChanged()
        //{
        //    if (Unit == U.IbPerHr || Unit == "IbPerHr")
        //    {
        //        double valueInIbPerHr = Value;
        //        ValueInIbPerHr = valueInIbPerHr;
        //        ValueInKgPerHr = Converter.ConvertMassFlowRateFrom_IbPerHr_To_KgPerHr(valueInIbPerHr);
        //        ValueInGPerS = Converter.ConvertMassFlowRateFrom_IbPerHr_To_GPerS(valueInIbPerHr);

        //    }

        //    else if (Unit == U.KgPerHr)
        //    {
        //        double valueInKgPerHr = Value;
        //        ValueInKgPerHr = valueInKgPerHr;
        //        ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(valueInKgPerHr);
        //        ValueInGPerS = valueInKgPerHr / 3.6;
        //    }

        //    else if (Unit == U.GPerSec)
        //    {
        //        double valueInGPerSec = Value;
        //        ValueInGPerS = valueInGPerSec;
        //        ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_GPerS_To_IbPerHr(valueInGPerSec);
        //        ValueInKgPerHr = valueInGPerSec * 3.6;
        //    }
        //    else if (Unit == U.KgPerS)
        //    {
        //        double valueInKgPerSec = Value;
        //        ValueInKgPerS = valueInKgPerSec;
        //        ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_GPerS_To_IbPerHr(valueInKgPerSec);
        //        ValueInKgPerHr = valueInKgPerSec * 3.6;
        //    }


        //}

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.IbPerHr || Unit == "IbPerHr")
            {
                double valueInIbPerHr = Value;
                ValueInIbPerHr = valueInIbPerHr;
                ValueInKgPerHr = Converter.ConvertMassFlowRateFrom_IbPerHr_To_KgPerHr(valueInIbPerHr);
                ValueInKgPerS = ValueInKgPerHr / 3600.0;
                ValueInGPerS = ValueInKgPerHr * 1000.0 / 3600.0;
            }
            else if (Unit == U.KgPerHr)
            {
                double valueInKgPerHr = Value;
                ValueInKgPerHr = valueInKgPerHr;
                ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(valueInKgPerHr);
                ValueInKgPerS = valueInKgPerHr / 3600.0;
                ValueInGPerS = valueInKgPerHr * 1000.0 / 3600.0;
            }
            else if (Unit == U.GPerSec)
            {
                double valueInGPerSec = Value;
                ValueInGPerS = valueInGPerSec;
                ValueInKgPerS = valueInGPerSec / 1000.0;
                ValueInKgPerHr = ValueInKgPerS * 3600.0;
                ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(ValueInKgPerHr);
            }
            else if (Unit == U.KgPerS)
            {
                double valueInKgPerSec = Value;
                ValueInKgPerS = valueInKgPerSec;
                ValueInKgPerHr = valueInKgPerSec * 3600.0;
                ValueInGPerS = valueInKgPerSec * 1000.0;
                ValueInIbPerHr = Converter.ConvertMassFlowRateFrom_KgPerHr_To_IbPerHr(ValueInKgPerHr);
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
                ValueInKgPerHr,
                ValueInGPerS
            };
        }

        public int CompareTo(object obj)
        {
            if (obj is RefrigerantMassFlowItem)
            {
                return this.CompareTo((RefrigerantMassFlowItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(RefrigerantMassFlowItem other)
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
            list.Add(U.GPerSec);
            list.Add(U.KgPerS);

            return list;
        }

        public static List<string> GetProofUnits()
        {
            var list = new List<string>();
            list.Add(U.IbPerHr);
            list.Add(U.KgPerHr);
            list.Add(U.GPerSec);

            return list;
        }


        public const string Name = nameof(RefrigerantMassFlowItem);

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


        public static RefrigerantMassFlowItem Parse(string s, IFormatProvider formatProvider)
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

        public static string OwnerUnitPropertyName = "RefrigerantMassFlowUnit";

        public static RefrigerantMassFlowItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return RefrigerantMassFlowItem.Factory.Create(r, unit);
                }
                else
                {
                    return RefrigerantMassFlowItem.Factory.Create(r, U.KgPerHr);
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
                        return RefrigerantMassFlowItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return RefrigerantMassFlowItem.Factory.Create(v, U.KgPerHr);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return RefrigerantMassFlowItem.Factory.Create(0, unit);
                }
                else
                {
                    return RefrigerantMassFlowItem.Factory.Create(0, U.KgPerHr);
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
