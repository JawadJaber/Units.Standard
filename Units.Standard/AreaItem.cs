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
    public interface IAreaItem
    {
        double ValueInSqM { get; set; }
        double ValueInSqFt { get; set; }
        double ValueInSqIn { get; set; }
    }
    public class AreaItem : IAreaItem, IUnit, ILiquidizable,INotifyPropertyChanged,IComparable,IComparable<AreaItem>
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



        #region IAreaItem






        private double _ValueInSqM { get; set; } = 0;
        public double ValueInSqM
        {
            get
            {
                return _ValueInSqM;
            }
            set
            {
                if (_ValueInSqM == value)
                    return;
                _ValueInSqM = value;
                OnPropertyChanged(nameof(ValueInSqM));
            }
        }


        private double _ValueInSqFt { get; set; } = 0;
        public double ValueInSqFt
        {
            get
            {
                return _ValueInSqFt;
            }
            set
            {
                if (_ValueInSqFt == value)
                    return;
                _ValueInSqFt = value;
                OnPropertyChanged(nameof(ValueInSqFt));
            }
        }


        private double _ValueInSqIn { get; set; } = 0;
        public double ValueInSqIn
        {
            get
            {
                return _ValueInSqIn;
            }
            set
            {
                if (_ValueInSqIn == value)
                    return;
                _ValueInSqIn = value;
                OnPropertyChanged(nameof(ValueInSqIn));
            }
        }


        #endregion

        #region IUnit



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




        #endregion

        #region Construction

        public AreaItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        public static class Factory
        {
            public static AreaItem Create(double value, string unit) { return new AreaItem(value, unit); }
        }


        public static AreaItem Create(AreaItem item)
        {
            return AreaItem.Factory.Create(item.Value, item.Unit);
        }

        #endregion

        public object ToLiquid()
        {
            throw new NotImplementedException();
        }

        public void UpdateWhenUnitChanged()
        {
            switch (Unit)
            {
                case U.SqM:
                    Value = ValueInSqM;
                    break;
                case U.SqFt:
                    Value = ValueInSqFt;
                    break;
                case U.SqIn:
                    Value = ValueInSqIn;
                    break;
                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            switch (Unit)
            {
                case U.SqM:
                    double valueInSqM = Value;
                    ValueInSqM = valueInSqM;
                    ValueInSqFt = Converter.ConvertAreaFrom_SqM_To_SqFt(valueInSqM);
                    ValueInSqIn = Converter.ConvertAreaFrom_SqM_To_SqIn(valueInSqM);
                    break;
                case U.SqIn:
                    double valueInAqIn = Value;
                    ValueInSqIn = valueInAqIn;
                    ValueInSqFt = Converter.ConvertAreaFrom_SqIn_To_SqFt(valueInAqIn);
                    ValueInSqM = Converter.ConvertAreaFrom_SqIn_To_SqM(valueInAqIn);
                    break;
                case U.SqFt:
                    double valueInSqFt = Value;
                    ValueInSqFt = valueInSqFt;
                    ValueInSqIn = Converter.ConvertAreaFrom_SqFt_To_SqIn(valueInSqFt);
                    ValueInSqM = Converter.ConvertAreaFrom_SqFt_To_SqM(valueInSqFt);
                    break;
                default:
                    break;
            }

          
        }


        public int CompareTo(object obj)
        {
            if (obj is AreaItem)
            {
                return this.CompareTo((AreaItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(AreaItem other)
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
            list.Add(U.SqFt);
            list.Add(U.SqM);
            list.Add(U.SqIn);
            return list;
        }


        public const string Name = nameof(AreaItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N2") + " " + Unit;
        }


        public static AreaItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return AreaItem.Factory.Create(r, U.SqM);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return AreaItem.Factory.Create(v, U.SqM);
                }

                return AreaItem.Factory.Create(0, U.SqM);
            }
        }


        public static string OwnerUnitPropertyName = "AreaUnit";

        public static AreaItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return AreaItem.Factory.Create(r, unit);
                }
                else
                {
                    return AreaItem.Factory.Create(r, U.LpS);
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
                        return AreaItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return AreaItem.Factory.Create(v, U.SqM);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return AreaItem.Factory.Create(0, unit);
                }
                else
                {
                    return AreaItem.Factory.Create(0, U.SqM);
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
