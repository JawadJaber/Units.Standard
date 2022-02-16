using DotLiquid;
using Newtonsoft.Json;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Units.Standard
{
    public class AirFlowItem : IUnit, IAirFlow, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<AirFlowItem>
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


        #region Properties

        private double _ValueInCFM { get; set; }
        public double ValueInCFM
        {
            get
            {
                return _ValueInCFM;
            }
            set
            {
                if (_ValueInCFM == value)
                    return;
                _ValueInCFM = value;
                OnPropertyChanged(nameof(ValueInCFM));
            }
        }

        private double _ValueInCMH { get; set; }
        public double ValueInCMH
        {
            get
            {
                return _ValueInCMH;
            }
            set
            {
                if (_ValueInCMH == value)
                    return;
                _ValueInCMH = value;
                OnPropertyChanged(nameof(ValueInCMH));
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

        public static AirFlowItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            if (dValue)
            {
                return AirFlowItem.Factory.Create(r, U.LpS);
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return AirFlowItem.Factory.Create(v, U.LpS);
                }

                return AirFlowItem.Factory.Create(0, U.LpS);
            }
        }


        public static AirFlowItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit("AirFlowUnit");

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return AirFlowItem.Factory.Create(r, unit);
                }
                else
                {
                    return AirFlowItem.Factory.Create(r, U.LpS);
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
                        return AirFlowItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return AirFlowItem.Factory.Create(v, U.LpS);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return AirFlowItem.Factory.Create(0, unit);
                }
                else
                {
                    return AirFlowItem.Factory.Create(0, U.LpS);
                }

               
            }
        }

        public AirFlowItem()
        {
            Unit = U.LpS;
            Value = 0;
        }

        public AirFlowItem(double valueInCFM, double valueInMPS, double valueInCMH, double valueInLPS, string unit)
        {
            if (unit == U.CFM)
            {
                ValueInCFM = valueInCFM;
                ValueInM3PS = Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM);
                ValueInCMH = Converter.ConvertAirFlowFrom_M3PS_To_CMH(Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM));
                ValueInLPS = Converter.ConvertAirFlowFrom_M3PS_To_LPS(Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM));
                Unit = unit;
            }
            else if (unit == U.M3PS)
            {
                ValueInM3PS = valueInMPS;
                ValueInCFM = Converter.ConvertAirFlowFrom_M3PS_To_CFM(valueInMPS);
                ValueInCMH = Converter.ConvertAirFlowFrom_M3PS_To_CMH(valueInMPS);
                ValueInLPS = Converter.ConvertAirFlowFrom_M3PS_To_LPS(valueInMPS);
                Unit = unit;
            }
            else if (unit == U.CMH)
            {
                ValueInCMH = valueInCMH;
                ValueInCFM = Converter.ConvertAirFlowFrom_M3PS_To_CFM(Converter.ConvertAirFlowFrom_CMH_To_M3PS(valueInCMH));
                ValueInLPS = Converter.ConvertAirFlowFrom_M3PS_To_LPS(Converter.ConvertAirFlowFrom_CMH_To_M3PS(valueInCMH));
                ValueInM3PS = Converter.ConvertAirFlowFrom_CMH_To_M3PS(valueInCMH);
                Unit = unit;
            }
            else if (unit == U.LPS || unit == U.LpS)
            {
                ValueInLPS = valueInLPS;
                ValueInCFM = Converter.ConvertAirFlowFrom_M3PS_To_CFM(Converter.ConvertAirFlowFrom_LPS_To_M3PS(valueInLPS));
                ValueInCMH = Converter.ConvertAirFlowFrom_M3PS_To_CMH(Converter.ConvertAirFlowFrom_LPS_To_M3PS(valueInLPS));
                ValueInM3PS = Converter.ConvertAirFlowFrom_LPS_To_M3PS(valueInLPS);
                Unit = unit;
            }
        }



        public AirFlowItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
        }

        #endregion


        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.CFM)
            {
                Value = ValueInCFM;
            }
            else if (Unit == U.CMH)
            {
                Value = ValueInCMH;
            }
            else if (Unit == U.M3PS)
            {
                Value = ValueInM3PS;
            }
            else if (Unit == U.LPS || Unit == U.LpS)
            {
                Value = ValueInLPS;
            }

        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.CFM)
            {
                double valueInCFM = Value;
                ValueInCFM = valueInCFM;
                ValueInCMH = Converter.ConvertAirFlowFrom_M3PS_To_CMH(Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM));
                ValueInLPS = Converter.ConvertAirFlowFrom_M3PS_To_LPS(Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM));
                ValueInM3PS = Converter.ConvertAirFlowFrom_CFM_To_MPS(valueInCFM);

            }
            else if (Unit == U.CMH)
            {
                double valueInCMH = Value;
                ValueInCMH = valueInCMH;
                ValueInCFM = Converter.ConvertAirFlowFrom_M3PS_To_CFM(Converter.ConvertAirFlowFrom_CMH_To_M3PS(valueInCMH));
                ValueInLPS = Converter.ConvertAirFlowFrom_M3PS_To_LPS(Converter.ConvertAirFlowFrom_CMH_To_M3PS(valueInCMH));
                ValueInM3PS = Converter.ConvertAirFlowFrom_CMH_To_M3PS(valueInCMH);
            }
            else if (Unit == U.LPS || Unit == U.LpS)
            {
                double valueInLPS = Value;
                ValueInLPS = valueInLPS;
                ValueInCMH = Converter.ConvertAirFlowFrom_M3PS_To_CMH(Converter.ConvertAirFlowFrom_LPS_To_M3PS(valueInLPS));
                ValueInCFM = Converter.ConvertAirFlowFrom_M3PS_To_CFM(Converter.ConvertAirFlowFrom_LPS_To_M3PS(valueInLPS));
                ValueInM3PS = Converter.ConvertAirFlowFrom_LPS_To_M3PS(valueInLPS);
            }
            else if (Unit == U.M3PS)
            {
                double valueInMPS = Value;
                ValueInM3PS = valueInMPS;
                ValueInCMH = Converter.ConvertAirFlowFrom_M3PS_To_CMH(valueInMPS);
                ValueInLPS = Converter.ConvertAirFlowFrom_M3PS_To_LPS(valueInMPS);
                ValueInCFM = Converter.ConvertAirFlowFrom_M3PS_To_CFM(valueInMPS);
            }
        }

        public static class Factory
        {
            public static AirFlowItem Create(double value, string unit) { return new AirFlowItem(value, unit); }
        }

        public static AirFlowItem Create(AirFlowItem item)
        {
            return AirFlowItem.Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value = Value.ToString("N2"),
                Unit,
                ValueInCFM,
                ValueInCMH,
                ValueInLPS,
                ValueInM3PS
            };
        }



        public override string ToString()
        {
            return Value.ToString("N0") + " " + Unit;
        }

        public int CompareTo(object obj)
        {
            if (obj is AirFlowItem)
            {
                return this.CompareTo((AirFlowItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(AirFlowItem other)
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
            list.Add(U.CFM);
            list.Add(U.LpS);
            list.Add(U.LPS);
            list.Add(U.M3PS);
            list.Add(U.CMH);
            return list;
        }


        public const string Name = nameof(AirFlowItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

    }
}
