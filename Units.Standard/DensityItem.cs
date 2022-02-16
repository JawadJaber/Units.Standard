using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Units.Standard
{
    public interface IDensityItem
    {
        double ValueInLbPerFt3 { get; set; }
        double ValueInKgPerM3 { get; set; }

        string Unit { get; set; }
    }

    public class DensityItem : IUnit, IDensityItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<DensityItem>
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
            if (Unit == U.LbPerFt3)
            {
                Value = ValueInLbPerFt3;
            }
            else
            {
                Value = ValueInKgPerM3;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.LbPerFt3)
            {
                double valueInF = Value;
                ValueInLbPerFt3 = valueInF;
                ValueInKgPerM3 = Converter.ConvertDensityFrom_LbPerFt3_To_KgPerM3(valueInF);
            }
            else
            {
                double valueInC = Value;
                ValueInKgPerM3 = valueInC;
                ValueInLbPerFt3 = Converter.ConvertDensityFrom_KgPerM3_To_LbPerFt3(valueInC);
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
        public double ValueInLbPerFt3
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
                OnPropertyChanged(nameof(ValueInLbPerFt3));
            }
        }

        private double _ValueInKgPerM3 { get; set; }
        public double ValueInKgPerM3
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
                OnPropertyChanged(nameof(ValueInKgPerM3));
            }
        }

        public DensityItem(double valueInLbPerFt3, double valueInKgPerM3, string unit)
        {

            if ((unit == U.LbPerFt3) && valueInKgPerM3 != Converter.ConvertDensityFrom_LbPerFt3_To_KgPerM3(valueInLbPerFt3))
            {
                valueInKgPerM3 = Converter.ConvertDensityFrom_LbPerFt3_To_KgPerM3(valueInLbPerFt3);
            }

            if (unit == U.KgPerM3 && valueInLbPerFt3 != Converter.ConvertDensityFrom_KgPerM3_To_LbPerFt3(valueInKgPerM3))
            {
                valueInLbPerFt3 = Converter.ConvertDensityFrom_KgPerM3_To_LbPerFt3(valueInKgPerM3);
            }

            this.ValueInKgPerM3 = valueInKgPerM3;
            this.ValueInLbPerFt3 = valueInLbPerFt3;
            this.Unit = unit;
        }

        private DensityItem()
        {

        }

        private DensityItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static DensityItem Create(double value, string unit) { return new DensityItem(value, unit); }
        }


        public static DensityItem Create(DensityItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInKgPerM3,
                ValueInLbPerFt3
            };
        }

        public int CompareTo(object obj)
        {
            if (obj is DensityItem)
            {
                return this.CompareTo((DensityItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(DensityItem other)
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
            list.Add(U.LbPerFt3);
            list.Add(U.KgPerM3);

            return list;
        }


        public const string Name = nameof(DensityItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public static DensityItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.KgPerM3);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, U.KgPerM3);
                }

                return Factory.Create(0, U.KgPerM3);
            }
        }



        public static string OwnerUnitPropertyName = "DensityUnit";

        public static DensityItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return DensityItem.Factory.Create(r, unit);
                }
                else
                {
                    return DensityItem.Factory.Create(r, U.KgPerM3);
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
                        return DensityItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return DensityItem.Factory.Create(v, U.KgPerM3);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return DensityItem.Factory.Create(0, unit);
                }
                else
                {
                    return DensityItem.Factory.Create(0, U.KgPerM3);
                }


            }
        }

    }

}
