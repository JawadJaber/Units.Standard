using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Units.Standard
{
    public class WaterFlowItem : IUnit, IWaterFlow, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<WaterFlowItem>
    {
        public WaterFlowItem(double valueInGPM, double valueInMPS, double valueInLPS, string unit)
        {
            if (unit == U.GPM)
            {
                ValueInGPM = valueInGPM;
                ValueInM3PS = Converter.ConvertWaterFlowFrom_GMP_To_M3PS(valueInGPM);
                ValueInLPS = Converter.ConvertWaterFlowFrom_M3PS_To_LPS(ValueInM3PS);
                Unit = unit;
            }
            else if (unit == U.M3PS)
            {
                ValueInM3PS = valueInMPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_M3PS_To_GPM(valueInMPS);
                ValueInLPS = Converter.ConvertWaterFlowFrom_M3PS_To_LPS(valueInMPS);
                Unit = unit;
            }
            else if (unit == U.LPS || unit == U.LpS)
            {
                ValueInLPS = valueInLPS;
                ValueInM3PS = Converter.ConvertWaterFlowFrom_LPS_To_M3PS(valueInLPS);
                ValueInGPM = Converter.ConvertWaterFlowFrom_M3PS_To_GPM(ValueInM3PS);
                Unit = unit;
            }

        }

        private WaterFlowItem()
        {

        }

        private WaterFlowItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        public static class Factory
        {
            public static WaterFlowItem Create(double value, string unit) { return new WaterFlowItem(value, unit); }
        }

        public static WaterFlowItem Create(WaterFlowItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }


        #region Properties

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


        private double _ValueInGPM { get; set; }
        public double ValueInGPM
        {
            get
            {
                return _ValueInGPM;
            }
            set
            {
                if (_ValueInGPM == value)
                    return;
                _ValueInGPM = value;
                OnPropertyChanged(nameof(ValueInGPM));
            }
        }

        private double _ValueInLPS { get; set; }
        public double ValueInLPS
        {
            get
            {
                return _ValueInLPS;
            }
            set
            {
                if (_ValueInLPS == value)
                    return;
                _ValueInLPS = value;
                OnPropertyChanged(nameof(ValueInLPS));
            }
        }

        private double _ValueInMPS { get; set; }
        public double ValueInM3PS
        {
            get
            {
                return _ValueInMPS;
            }
            set
            {
                if (_ValueInMPS == value)
                    return;
                _ValueInMPS = value;
                OnPropertyChanged(nameof(ValueInM3PS));
            }
        }

        #endregion

        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.GPM || Unit == "GPM")
            {
                Value = ValueInGPM;
            }
            else if (Unit == U.LPS || Unit == U.LpS)
            {
                Value = ValueInLPS;
            }
            else if (Unit == U.M3PS)
            {
                Value = ValueInM3PS;
            }

        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.GPM || Unit == "GPM")
            {
                double valueInGPM = Value;
                ValueInGPM = valueInGPM;
                ValueInM3PS = Converter.ConvertWaterFlowFrom_GMP_To_M3PS(valueInGPM);
                ValueInLPS = Converter.ConvertWaterFlowFrom_M3PS_To_LPS(Converter.ConvertWaterFlowFrom_GMP_To_M3PS(valueInGPM));
            }
            else if (Unit == U.LPS || Unit == U.LpS)
            {
                double valueInLPS = Value;
                ValueInLPS = valueInLPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_M3PS_To_GPM(Converter.ConvertWaterFlowFrom_LPS_To_M3PS(valueInLPS));
                ValueInM3PS = Converter.ConvertWaterFlowFrom_LPS_To_M3PS(valueInLPS);
            }
            else if (Unit == U.M3PS)
            {
                double valueInMPS = Value;
                ValueInM3PS = valueInMPS;
                ValueInGPM = Converter.ConvertWaterFlowFrom_M3PS_To_GPM(valueInMPS);
                ValueInLPS = Converter.ConvertWaterFlowFrom_M3PS_To_LPS(valueInMPS);
            }

        }



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
        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N1"),
                Unit,
                ValueInGPM,
                ValueInLPS,
                ValueInM3PS
            };
        }

        public int CompareTo(object obj)
        {
            if (obj is WaterFlowItem)
            {
                return this.CompareTo((WaterFlowItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(WaterFlowItem other)
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
            list.Add(U.GPM);
            list.Add(U.LpS);
            list.Add(U.LPS);
            list.Add(U.M3PS);

            return list;
        }


        public const string Name = nameof(WaterFlowItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToString("N2") + " " + Unit;
        }


        public static WaterFlowItem Parse(string s, IFormatProvider formatProvider)
        {
            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return Factory.Create(r, U.LpS);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);
                if (isNumber)
                {
                    return Factory.Create(v, U.LpS);
                }

                return Factory.Create(0, U.LpS);
            }
        }

        public static string OwnerUnitPropertyName = "WaterFlowUnit";

        public static WaterFlowItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return WaterFlowItem.Factory.Create(r, unit);
                }
                else
                {
                    return WaterFlowItem.Factory.Create(r, U.LpS);
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
                        return WaterFlowItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return WaterFlowItem.Factory.Create(v, U.LpS);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return WaterFlowItem.Factory.Create(0, unit);
                }
                else
                {
                    return WaterFlowItem.Factory.Create(0, U.LpS);
                }


            }
        }


    }
}
