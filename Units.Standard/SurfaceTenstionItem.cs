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

    public interface ISurfaceTenstionItem
    {
        double ValueInDynePerCM { get; set; }
        double ValueInNewtonPerM { get; set; }

        string Unit { get; set; }
    }

    public class SurfaceTenstionItem : IUnit, ISurfaceTenstionItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<SurfaceTenstionItem>
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
            if (Unit == U.DynePerCM)
            {
                Value = ValueInDynePerCM;
            }
            else
            {
                Value = ValueInNewtonPerM;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.DynePerCM)
            {
                double valueInF = Value;
                ValueInDynePerCM = valueInF;
                ValueInNewtonPerM = Converter.ConvertDensityFrom_DynePerCM_To_NewtonPerM(valueInF);
            }
            else
            {
                double valueInC = Value;
                ValueInNewtonPerM = valueInC;
                ValueInDynePerCM = Converter.ConvertDensityFrom_NewtonPerM_To_DynePerCM(valueInC);
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


        private double _ValueInDynePerCM { get; set; }
        public double ValueInDynePerCM
        {
            get
            {
                return _ValueInDynePerCM;
            }
            set
            {
                if (_ValueInDynePerCM == value)
                    return;
                _ValueInDynePerCM = value;
                OnPropertyChanged(nameof(ValueInDynePerCM));
            }
        }

        private double _ValueInNewtonPerM { get; set; }
        public double ValueInNewtonPerM
        {
            get
            {
                return _ValueInNewtonPerM;
            }
            set
            {
                if (_ValueInNewtonPerM == value)
                    return;
                _ValueInNewtonPerM = value;
                OnPropertyChanged(nameof(ValueInNewtonPerM));
            }
        }

        public SurfaceTenstionItem(double valueInDynePerCM, double valueInNewtonPerM, string unit)
        {

            if ((unit == U.DynePerCM) && valueInNewtonPerM != Converter.ConvertDensityFrom_DynePerCM_To_NewtonPerM(valueInDynePerCM))
            {
                valueInNewtonPerM = Converter.ConvertDensityFrom_DynePerCM_To_NewtonPerM(valueInDynePerCM);
            }

            if (unit == U.NewtonPerM && valueInDynePerCM != Converter.ConvertDensityFrom_NewtonPerM_To_DynePerCM(valueInNewtonPerM))
            {
                valueInDynePerCM = Converter.ConvertDensityFrom_NewtonPerM_To_DynePerCM(valueInNewtonPerM);
            }

            this.ValueInNewtonPerM = valueInNewtonPerM;
            this.ValueInDynePerCM = valueInDynePerCM;
            this.Unit = unit;
        }

        private SurfaceTenstionItem()
        {

        }

        private SurfaceTenstionItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static SurfaceTenstionItem Create(double value, string unit) { return new SurfaceTenstionItem(value, unit); }
        }


        public static SurfaceTenstionItem Create(SurfaceTenstionItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInNewtonPerM,
                ValueInDynePerCM
            };
        }


        public int CompareTo(object obj)
        {
            if (obj is SurfaceTenstionItem)
            {
                return this.CompareTo((SurfaceTenstionItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(SurfaceTenstionItem other)
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
            list.Add(U.DynePerCM);
            list.Add(U.NewtonPerM);
          
            return list;
        }


        public const string Name = nameof(SurfaceTenstionItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public static SurfaceTenstionItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.NewtonPerM);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(v, U.NewtonPerM);
                }

                return Factory.Create(0, U.NewtonPerM);
            }
        }


        public static string OwnerUnitPropertyName = "PressureDropUnit";

        public static SurfaceTenstionItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return SurfaceTenstionItem.Factory.Create(r, unit);
                }
                else
                {
                    return SurfaceTenstionItem.Factory.Create(r, U.NewtonPerM);
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
                        return SurfaceTenstionItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return SurfaceTenstionItem.Factory.Create(v, U.NewtonPerM);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return SurfaceTenstionItem.Factory.Create(0, unit);
                }
                else
                {
                    return SurfaceTenstionItem.Factory.Create(0, U.NewtonPerM);
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
