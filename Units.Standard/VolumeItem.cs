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
    public class VolumeItem : INotifyPropertyChanged, IUnit, IVolume, ILiquidizable, IComparable, IComparable<VolumeItem>
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

        #region IVolume



        private double _ValueInCubicInch { get; set; }
        public double ValueInCubicInch
        {
            get
            {
                return _ValueInCubicInch;
            }
            set
            {
                if (_ValueInCubicInch == value)
                    return;
                _ValueInCubicInch = value;
                OnPropertyChanged(nameof(ValueInCubicInch));
            }
        }


        private double _ValueInCubicMeter { get; set; }
        public double ValueInCubicMeter
        {
            get
            {
                return _ValueInCubicMeter;
            }
            set
            {
                if (_ValueInCubicMeter == value)
                    return;
                _ValueInCubicMeter = value;
                OnPropertyChanged(nameof(ValueInCubicMeter));
            }
        }





        private double _ValueInCubicFeet { get; set; }
        public double ValueInCubicFeet
        {
            get
            {
                return _ValueInCubicFeet;
            }
            set
            {
                if (_ValueInCubicFeet == value)
                    return;
                _ValueInCubicFeet = value;
                OnPropertyChanged(nameof(ValueInCubicFeet));
            }
        }

        #endregion

        #region IUnits


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
            switch (Unit)
            {
                case U.CubIn:
                    Value = ValueInCubicInch;
                    break;
                case U.CubM:
                    Value = ValueInCubicMeter;
                    break;
                case U.CubFt:
                    Value = ValueInCubicFeet;
                    break;

                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.CubIn)
            {
                double valueInCuInch = Value;
                ValueInCubicInch = valueInCuInch;
                ValueInCubicMeter = Converter.ConvertVolumeFrom_CuIn_To_CuM(valueInCuInch);
                ValueInCubicFeet = Converter.ConvertVolumeFrom_CuInch_To_CuFt(valueInCuInch);
            }
            else if (Unit == U.CubM)
            {
                double valueInCuM = Value;
                ValueInCubicMeter = valueInCuM;
                ValueInCubicInch = Converter.ConvertVolumeFrom_CuM_To_CuInch(valueInCuM);
                ValueInCubicFeet = Converter.ConvertVolumeFrom_CuM_To_CuFt(valueInCuM);
            }
            else if(Unit == U.CubFt)
            {
                double valueInCubFt = Value;
                ValueInCubicFeet = valueInCubFt;
                ValueInCubicMeter = Converter.ConvertVolumeFrom_CuFt_To_CuM(valueInCubFt);
                ValueInCubicInch = Converter.ConvertVolumeFrom_CuFt_To_CuInch(valueInCubFt);
            }


        }


        #endregion

        #region Construction

        public VolumeItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        public VolumeItem()
        {

        }

        public static class Factory
        {
            public static VolumeItem Create(double value, string unit) { return new VolumeItem(value, unit); }
        }

        public static VolumeItem Create(VolumeItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value,
                Unit,
                ValueInCubicInch,
                ValueInCubicMeter,
                ValueInCubicFeet
            };
        }

        #endregion


        public int CompareTo(object obj)
        {
            if (obj is VolumeItem)
            {
                return this.CompareTo((VolumeItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(VolumeItem other)
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
            list.Add(U.CubFt);
            list.Add(U.CubIn);
            list.Add(U.CubM);
            return list;
        }


        public const string Name = nameof(VolumeItem);

        public static List<string> AllUnits { get; set; } = GetUnits();
       

        public override string ToString()
        {
            return Value.ToString("N2") + " " + Unit;
        }


        public static VolumeItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.CubM);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, U.CubM);
                }

                return Factory.Create(0, U.CubM);
            }
        }


        public static string OwnerUnitPropertyName = "VolUnit";

        public static VolumeItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return VolumeItem.Factory.Create(r, unit);
                }
                else
                {
                    return VolumeItem.Factory.Create(r, U.CubM);
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
                        return VolumeItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return VolumeItem.Factory.Create(v, U.CubM);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return VolumeItem.Factory.Create(0, unit);
                }
                else
                {
                    return VolumeItem.Factory.Create(0, U.CubM);
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
