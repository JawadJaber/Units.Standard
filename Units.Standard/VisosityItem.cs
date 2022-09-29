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
    public interface IViscosityItem
    {
        double ValueIncP { get; set; }
        double ValueInKgPerMS { get; set; }

        string Unit { get; set; }
    }

    public class ViscosityItem : IUnit, IViscosityItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<ViscosityItem>
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
            if (Unit == U.cP)
            {
                Value = ValueIncP;
            }
            else
            {
                Value = ValueInKgPerMS;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.cP)
            {
                double valueInF = Value;
                ValueIncP = valueInF;
                ValueInKgPerMS = Converter.ConvertViscosityFrom_cP_To_KgPerMS(valueInF);
            }
            else
            {
                double valueInC = Value;
                ValueInKgPerMS = valueInC;
                ValueIncP = Converter.ConvertViscosityFrom_KgPerMS_To_cP(valueInC);
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


        private double _ValueIncP { get; set; }
        public double ValueIncP
        {
            get
            {
                return _ValueIncP;
            }
            set
            {
                if (_ValueIncP == value)
                    return;
                _ValueIncP = value;
                OnPropertyChanged(nameof(ValueIncP));
            }
        }

        private double _ValueInKgPerMS { get; set; }
        public double ValueInKgPerMS
        {
            get
            {
                return _ValueInKgPerMS;
            }
            set
            {
                if (_ValueInKgPerMS == value)
                    return;
                _ValueInKgPerMS = value;
                OnPropertyChanged(nameof(ValueInKgPerMS));
            }
        }

        public ViscosityItem(double valueIncP, double valueInKgPerMS, string unit)
        {

            if ((unit == U.cP) && valueInKgPerMS != Converter.ConvertViscosityFrom_cP_To_KgPerMS(valueIncP))
            {
                valueInKgPerMS = Converter.ConvertViscosityFrom_cP_To_KgPerMS(valueIncP);
            }

            if (unit == U.KgPerMS && valueIncP != Converter.ConvertViscosityFrom_KgPerMS_To_cP(valueInKgPerMS))
            {
                valueIncP = Converter.ConvertViscosityFrom_KgPerMS_To_cP(valueInKgPerMS);
            }

            this.ValueInKgPerMS = valueInKgPerMS;
            this.ValueIncP = valueIncP;
            this.Unit = unit;
        }

        private ViscosityItem()
        {

        }

        private ViscosityItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static ViscosityItem Create(double value, string unit) { return new ViscosityItem(value, unit); }
        }


        public static ViscosityItem Create(ViscosityItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.To10ToPowerX(),
                Unit,
                ValueInKgPerMS,
                ValueIncP
            };
        }


        public int CompareTo(object obj)
        {
            if (obj is ViscosityItem)
            {
                return this.CompareTo((ViscosityItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(ViscosityItem other)
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
            list.Add(U.cP);
            list.Add(U.KgPerMS);

            return list;
        }


        public const string Name = nameof(ViscosityItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public static ViscosityItem Parse(string s, IFormatProvider formatProvider)
        {
            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.KgPerMS);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);
                if (isNumber)
                {
                    return Factory.Create(v, U.KgPerMS);
                }

                return Factory.Create(0, U.KgPerMS);
            }
        }


        public static string OwnerUnitPropertyName = "ViscosityUnit";

        public static ViscosityItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return ViscosityItem.Factory.Create(r, unit);
                }
                else
                {
                    return ViscosityItem.Factory.Create(r, U.KgPerMS);
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
                        return ViscosityItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return ViscosityItem.Factory.Create(v, U.KgPerMS);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return ViscosityItem.Factory.Create(0, unit);
                }
                else
                {
                    return ViscosityItem.Factory.Create(0, U.KgPerMS);
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
