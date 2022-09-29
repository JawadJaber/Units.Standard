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
    public interface IVelocityItem
    {
        double ValueInFtPerMin { get; set; }
        double ValueInMPS { get; set; }

        string Unit { get; set; }

       
    }


    public interface IDefaultParameter
    {
        string DefaultParameter { get; set; }
    }

    [Serializable]
    public class VelocityItem : IUnit, IVelocityItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<VelocityItem>, IDefaultParameter
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
            if (Unit == U.FtPerMin)
            {
                Value = ValueInFtPerMin;
            }
            else
            {
                Value = ValueInMPS;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.FtPerMin)
            {
                double valueInFtPerMin = Value;
                ValueInFtPerMin = valueInFtPerMin;
                ValueInMPS = Converter.ConvertSpeedFrom_FtPerMin_To_MPS(valueInFtPerMin);
            }
            else
            {
                double valueInMPS = Value;
                ValueInMPS = valueInMPS;
                ValueInFtPerMin = Converter.ConvertSpeedFrom_MPS_To_FtPerMin(valueInMPS);
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





        [JsonProperty("StringValue")]
        private string _StringValue { get; set; }
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
                this.UpdateValueWhenStringValueChanged(this.DefaultParameter);
            }
        }


        private double _ValueInLbPerFt3 { get; set; }
        public double ValueInFtPerMin
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
                OnPropertyChanged(nameof(ValueInFtPerMin));
            }
        }

        private double _ValueInKgPerM3 { get; set; }
        public double ValueInMPS
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
                OnPropertyChanged(nameof(ValueInMPS));
            }
        }


        public string ValueAsString => Value == 0 ? "-" : Value.ToString("N1");



        public static VelocityItem Parse(string s, IFormatProvider formatProvider)
        {
            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return VelocityItem.Factory.Create(r, U.MPS);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);
                if (isNumber)
                {
                    return VelocityItem.Factory.Create(v, U.MPS);
                }

                return VelocityItem.Factory.Create(0, U.MPS);
            }
        }

        public static string OwnerUnitPropertyName = "VelocityUnit";

        public static VelocityItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return VelocityItem.Factory.Create(r, unit);
                }
                else
                {
                    return VelocityItem.Factory.Create(r, U.MPS);
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
                        return VelocityItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return VelocityItem.Factory.Create(v, U.MPS);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return VelocityItem.Factory.Create(0, unit);
                }
                else
                {
                    return VelocityItem.Factory.Create(0, U.MPS);
                }


            }
        }

        public VelocityItem(double valueInFtPerMin, double valueInMPS, string unit)
        {

            if ((unit == U.FtPerMin) && valueInMPS != Converter.ConvertSpeedFrom_FtPerMin_To_MPS(valueInFtPerMin))
            {
                valueInMPS = Converter.ConvertSpeedFrom_FtPerMin_To_MPS(valueInFtPerMin);
            }

            if (unit == U.MPS && valueInFtPerMin != Converter.ConvertSpeedFrom_MPS_To_FtPerMin(valueInMPS))
            {
                valueInFtPerMin = Converter.ConvertSpeedFrom_MPS_To_FtPerMin(valueInMPS);
            }

            this.ValueInMPS = valueInMPS;
            this.ValueInFtPerMin = valueInFtPerMin;
            this.Unit = unit;
        }

        protected VelocityItem()
        {

        }

        protected VelocityItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        protected VelocityItem(double value, string unit, string parameter)
        {
            Unit = unit;
            Value = value;
            DefaultParameter = parameter;
        }

        public static class Factory
        {
            public static VelocityItem Create(double value, string unit) { return new VelocityItem(value, unit); }

            public static VelocityItem Create(double value, string unit, string parameter) { return new VelocityItem(value, unit, parameter); }
        }


        public static VelocityItem Create(VelocityItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInMPS,
                ValueInFtPerMin
            };
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(DefaultParameter))
            {
                if(Value == 0)
                {
                    return DefaultParameter;
                }
            }

            return $"{Value.ToString("N2")} {Unit}";
        }


        public int CompareTo(object obj)
        {
            if (obj is VelocityItem)
            {
                return this.CompareTo((VelocityItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(VelocityItem other)
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
            list.Add(U.MPS);
            list.Add(U.FtPerMin);

            return list;
        }


        public const string Name = nameof(VelocityItem);

        public static List<string> AllUnits { get; set; } = GetUnits();


        #region IDefaultParameter



        private string _DefaultParameter { get; set; } = string.Empty;
        public string DefaultParameter
        {
            get
            {
                return _DefaultParameter;
            }
            set
            {
                if (_DefaultParameter == value)
                    return;
                _DefaultParameter = value;
                OnPropertyChanged(nameof(DefaultParameter));
            }
        }


        #endregion

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.StringValue = Value.ToString();
        }

    }




}
