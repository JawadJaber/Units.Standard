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
    public class FinsSpacingItem : IFinsSpacing, INotifyPropertyChanged, IUnit, ILiquidizable, IComparable, IComparable<FinsSpacingItem>
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


        #region IFinsSpacing



        private double _ValueInFPI { get; set; }
        public double ValueInFPI
        {
            get
            {
                return _ValueInFPI;
            }
            set
            {
                if (_ValueInFPI == value)
                    return;
                _ValueInFPI = value;
                OnPropertyChanged(nameof(ValueInFPI));
            }
        }


        private double _ValueInFPMM { get; set; }
        public double ValueInFPMM
        {
            get
            {
                return _ValueInFPMM;
            }
            set
            {
                if (_ValueInFPMM == value)
                    return;
                _ValueInFPMM = value;
                OnPropertyChanged(nameof(ValueInFPMM));
            }
        }


        private double _ValueInFPM { get; set; }
        public double ValueInFPM
        {
            get
            {
                return _ValueInFPM;
            }
            set
            {
                if (_ValueInFPM == value)
                    return;
                _ValueInFPM = value;
                OnPropertyChanged(nameof(ValueInFPM));
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
                case U.FPI:
                    Value = ValueInFPI;
                    break;
                case U.FPM:
                    Value = ValueInFPM;
                    break;
                case U.FPMM:
                    Value = ValueInFPMM;
                    break;
                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {


            switch (Unit)
            {
                case U.FPI:
                    double valueInFPI = Value;
                    ValueInFPI = valueInFPI;
                    ValueInFPM = Converter.Convert_FinsPerInch_To_FinsPerMeter(valueInFPI);
                    ValueInFPMM = Converter.Convert_FinsPerInch_To_FinsPerMeter(valueInFPI) / 1000;
                    break;
                case U.FPM:
                    double valueInFPM = Value;
                    ValueInFPM = valueInFPM;
                    ValueInFPI = Converter.Convert_FinsPerMeter_To_FinsPerInch(valueInFPM);
                    ValueInFPMM = valueInFPM / 1000;
                    break;
                case U.FPMM:
                    double valueInFPMM = Value;
                    ValueInFPMM = valueInFPMM;
                    ValueInFPI = Converter.Convert_FinsPerMeter_To_FinsPerInch(valueInFPMM * 1000);
                    ValueInFPM = valueInFPMM * 1000;
                    break;
                default:
                    break;
            }

          
        }



        #endregion


        #region Construction

        public FinsSpacingItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }


        public static class Factory
        {
            public static FinsSpacingItem Create(double value, string unit) { return new FinsSpacingItem(value, unit); }
        }

        public static FinsSpacingItem Create(FinsSpacingItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value,
                Unit,
                ValueInFPI,
                ValueInFPM,
                ValueInFPMM
            };
        }

        #endregion


        public int CompareTo(object obj)
        {
            if (obj is FinsSpacingItem)
            {
                return this.CompareTo((FinsSpacingItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(FinsSpacingItem other)
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
            list.Add(U.FPI);
            list.Add(U.FPM);
            list.Add(U.FPMM);
            return list;
        }


        public const string Name = nameof(FinsSpacingItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public static FinsSpacingItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.FPM);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, U.FPM);
                }

                return Factory.Create(0, U.FPM);
            }
        }


        public static string OwnerUnitPropertyName = "FinSpacingUnit";

        public static FinsSpacingItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return FinsSpacingItem.Factory.Create(r, unit);
                }
                else
                {
                    return FinsSpacingItem.Factory.Create(r, U.FPM);
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
                        return FinsSpacingItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return FinsSpacingItem.Factory.Create(v, U.FPM);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return FinsSpacingItem.Factory.Create(0, unit);
                }
                else
                {
                    return FinsSpacingItem.Factory.Create(0, U.FPM);
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
