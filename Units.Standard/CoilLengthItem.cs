using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace Units.Standard
{
    public class CoilLengthItem : INotifyPropertyChanged, IUnit, ICoilLength, ILiquidizable, IComparable, IComparable<CoilLengthItem>
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

        #region ICoilLength



        private double _ValueInInch { get; set; }
        public double ValueInInch
        {
            get
            {
                return _ValueInInch;
            }
            set
            {
                if (_ValueInInch == value)
                    return;
                _ValueInInch = value;
                OnPropertyChanged(nameof(ValueInInch));
            }
        }



        [JsonProperty("ValueInFt")]
        private double _ValueInFt { get; set; }
        [JsonIgnore]
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


        private double _ValueInMM { get; set; }
        public double ValueInMM
        {
            get
            {
                return _ValueInMM;
            }
            set
            {
                if (_ValueInMM == value)
                    return;
                _ValueInMM = value;
                OnPropertyChanged(nameof(ValueInMM));
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




        [JsonProperty("ValueInCM")]
        private double _ValueInCM { get; set; }
        [JsonIgnore]
        public double ValueInCM
        {
            get
            {
                return _ValueInCM;
            }
            set
            {
                if (_ValueInCM == value)
                    return;
                _ValueInCM = value;
                OnPropertyChanged(nameof(ValueInCM));
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
                case U.inch:
                    Value = ValueInInch;
                    break;
                case U.mm:
                    Value = ValueInMM;
                    break;
                case U.m:
                    Value = ValueInM;
                    break;
                case U.ft:
                    Value = ValueInFt;
                    break;
                case U.cm:
                    Value = ValueInCM;
                    break;

                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.inch)
            {
                double valueInInch = Value;
                ValueInInch = valueInInch;
                ValueInMM = Converter.ConvertLengthFrom_In_To_M(valueInInch) * 1000.0;
                ValueInCM = Converter.ConvertLengthFrom_In_To_M(valueInInch) * 100.0;
                ValueInM = Converter.ConvertLengthFrom_In_To_M(valueInInch);
                ValueInFt = Converter.ConvertLengthFrom_Inch_To_Ft(valueInInch);
            }
            else if (Unit == U.m)
            {
                double valueInM = Value;
                ValueInM = valueInM;
                ValueInInch = Converter.ConvertLengthFrom_M_To_In(valueInM);
                ValueInMM = valueInM * 1000.0;
                ValueInCM = valueInM * 100.0;
                ValueInFt = Converter.ConvertLengthFrom_M_To_Ft(valueInM);
            }
            else if (Unit == U.mm)
            {
                double valueInMM = Value;
                ValueInMM = valueInMM;
                ValueInCM = valueInMM / 10.0;
                ValueInInch = Converter.ConvertLengthFrom_M_To_In(valueInMM / 1000.0);
                ValueInM = valueInMM / 1000.0;
                ValueInFt = Converter.ConvertLengthFrom_M_To_Ft(valueInMM / 1000.0);
            }
            else if (Unit == U.ft)
            {
                double valueInFt = Value;
                ValueInFt = valueInFt;
                ValueInInch = Converter.ConvertLengthFrom_Ft_To_Inch(valueInFt);
                ValueInM = Converter.ConvertLengthFrom_Ft_To_M(valueInFt);
                ValueInMM = Converter.ConvertLengthFrom_Ft_To_M(valueInFt) * 1000.0;
                ValueInCM = Converter.ConvertLengthFrom_Ft_To_M(valueInFt) * 100.0;


            }
            else if (Unit == U.cm)
            {
                double valueInCm = Value;
                ValueInFt = Converter.ConvertLengthFrom_M_To_Ft(valueInCm / 100.0);
                ValueInInch = Converter.ConvertLengthFrom_M_To_In(valueInCm / 100.0);
                ValueInM = valueInCm / 100.0;
                ValueInMM = valueInCm * 10.0;
                ValueInCM = valueInCm;
            }
        }


        #endregion

        #region Construction

        public CoilLengthItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        public CoilLengthItem()
        {

        }

        public static class Factory
        {
            public static CoilLengthItem Create(double value, string unit) { return new CoilLengthItem(value, unit); }
        }

        public static CoilLengthItem Create(CoilLengthItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value,
                Unit,
                ValueInInch,
                ValueInMM,
                ValueInM
            };
        }

        #endregion


        public int CompareTo(object obj)
        {
            if (obj is CoilLengthItem)
            {
                return this.CompareTo((CoilLengthItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(CoilLengthItem other)
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
            list.Add(U.inch);
            list.Add(U.mm);
            list.Add(U.m);
            return list;
        }


        public const string Name = nameof(CoilLengthItem);

        public static List<string> AllUnits { get; set; } = GetUnits();
        public static List<string> FtInchMMUnits { get; set; } = new List<string>() { U.mm, U.ft, U.inch };
        public static List<string> InchMMUnits { get; set; } = new List<string>() { U.mm, U.inch };
        public static List<string> FtMeterUnits { get; set; } = new List<string>() { U.m, U.ft };
        public static List<string> MeterCMUnits { get; set; } = new List<string>() { U.m, U.cm };
        public static List<string> MeterCMFtInchUnits { get; set; } = new List<string>() { U.m, U.cm,U.mm,U.inch,U.ft };

        public override string ToString()
        {
            if(this.Unit == U.cm)
            {
                return Value.ToString("N2") + " " + Unit;
            }
            else
            {
                return Value.ToString("N2") + " " + Unit;
            }
           
        }


        public static CoilLengthItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, DefaultUnit.Instance.DefaultDimUnit);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, DefaultUnit.Instance.DefaultDimUnit);
                }

                return Factory.Create(0, DefaultUnit.Instance.DefaultDimUnit);
            }
        }


        public static string OwnerUnitPropertyName = "DimUnit";

        public static CoilLengthItem Parse(string s, IHashable hashable,IRandomHashCode randomHashCode, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName,randomHashCode.RandomHashCode);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return CoilLengthItem.Factory.Create(r, unit);
                }
                else
                {
                    return CoilLengthItem.Factory.Create(r, DefaultUnit.Instance.DefaultDimUnit);
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
                        return CoilLengthItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return CoilLengthItem.Factory.Create(v, DefaultUnit.Instance.DefaultDimUnit);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return CoilLengthItem.Factory.Create(0, unit);
                }
                else
                {
                    return CoilLengthItem.Factory.Create(0, DefaultUnit.Instance.DefaultDimUnit);
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
