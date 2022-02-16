using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
                ValueInM = Converter.ConvertLengthFrom_In_To_M(valueInInch);
            }
            else if(Unit == U.m)
            {
                double valueInM = Value;
                ValueInM = valueInM;
                ValueInInch = Converter.ConvertLengthFrom_M_To_In(valueInM);
                ValueInM = valueInM * 1000.0;
            }
            else 
            {
                double valueInMM = Value;
                ValueInMM = valueInMM;
                ValueInInch = Converter.ConvertLengthFrom_M_To_In(valueInMM / 1000.0);
                ValueInM = valueInMM / 1000.0;
            }
        }


        #endregion

        #region Construction

        public CoilLengthItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
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

        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }


        public static CoilLengthItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.mm);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, U.mm);
                }

                return Factory.Create(0, U.mm);
            }
        }


        public static string OwnerUnitPropertyName = "DimUnit";

        public static CoilLengthItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return CoilLengthItem.Factory.Create(r, unit);
                }
                else
                {
                    return CoilLengthItem.Factory.Create(r, U.mm);
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
                        return CoilLengthItem.Factory.Create(v, U.mm);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return CoilLengthItem.Factory.Create(0, unit);
                }
                else
                {
                    return CoilLengthItem.Factory.Create(0, U.mm);
                }


            }
        }

    }
}
