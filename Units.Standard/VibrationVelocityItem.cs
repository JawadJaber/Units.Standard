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
    public interface IVibrationVelocityItem
    {
        double ValueInInchesPerSec { get; set; }
        double ValueInMMPerSec { get; set; }

        string Unit { get; set; }
    }


   

    [Serializable]
    public class VibrationVelocityItem : IUnit, IVibrationVelocityItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<VibrationVelocityItem>, IDefaultParameter
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
            if (Unit == U.InchPerSec)
            {
                Value = ValueInInchesPerSec;
            }
            else
            {
                Value = ValueInMMPerSec;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.InchPerSec)
            {
                double valueInInchPerSec = Value;
                ValueInInchesPerSec = valueInInchPerSec;
                ValueInMMPerSec = Converter.ConvertSpeedFrom_InchPerSec_To_MMPS(valueInInchPerSec);
            }
            else
            {
                double valueInMMPS = Value;
                ValueInMMPerSec = valueInMMPS;
                ValueInInchesPerSec = Converter.ConvertSpeedFrom_MMPS_To_InchPerSec(valueInMMPS);
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
        public double ValueInInchesPerSec
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
                OnPropertyChanged(nameof(ValueInInchesPerSec));
            }
        }

        private double _ValueInKgPerM3 { get; set; }
        public double ValueInMMPerSec
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
                OnPropertyChanged(nameof(ValueInMMPerSec));
            }
        }


        public string ValueAsString => Value == 0 ? "-" : Value.ToString("N1");



        public static VibrationVelocityItem Parse(string s, IFormatProvider formatProvider)
        {
            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return VibrationVelocityItem.Factory.Create(r, U.MPS);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);
                if (isNumber)
                {
                    return VibrationVelocityItem.Factory.Create(v, U.MPS);
                }

                return VibrationVelocityItem.Factory.Create(0, U.MPS);
            }
        }


        public static string OwnerUnitPropertyName = "VibrationVelocityUnit";

        public static VibrationVelocityItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return VibrationVelocityItem.Factory.Create(r, unit);
                }
                else
                {
                    return VibrationVelocityItem.Factory.Create(r, U.MPS);
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
                        return VibrationVelocityItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return VibrationVelocityItem.Factory.Create(v, U.MPS);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return VibrationVelocityItem.Factory.Create(0, unit);
                }
                else
                {
                    return VibrationVelocityItem.Factory.Create(0, U.MPS);
                }


            }
        }



        protected VibrationVelocityItem()
        {

        }

        protected VibrationVelocityItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        protected VibrationVelocityItem(double value, string unit, string parameter)
        {
            Unit = unit;
            Value = value;
            DefaultParameter = parameter;
            StringValue = value.ToString();
        }

        public static class Factory
        {
            public static VibrationVelocityItem Create(double value, string unit) { return new VibrationVelocityItem(value, unit); }

            public static VibrationVelocityItem Create(double value, string unit, string parameter) { return new VibrationVelocityItem(value, unit, parameter); }
        }


        public static VibrationVelocityItem Create(VibrationVelocityItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInMMPerSec,
                ValueInInchesPerSec
            };
        }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(DefaultParameter))
            {
                if (Value == 0)
                {
                    return DefaultParameter;
                }
            }

            return $"{Value.ToDouble_SigFig(2)} {Unit}";
        }


        public int CompareTo(object obj)
        {
            if (obj is VibrationVelocityItem)
            {
                return this.CompareTo((VibrationVelocityItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(VibrationVelocityItem other)
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
            list.Add(U.MMPerSec);
            list.Add(U.InchPerSec);

            return list;
        }


        public const string Name = nameof(VibrationVelocityItem);

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
                this.UpdateValueWhenStringValueChanged(this.DefaultParameter);
            }
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            this.StringValue = Value.ToString();
        }
    }




}
