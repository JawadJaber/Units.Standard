//using DotLiquid;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using StdHelpers;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Runtime.Serialization;
//using System.Text;
//using System.Text.RegularExpressions;

//namespace Units.Standard
//{
//    public interface ICurrencyItem
//    {
//        double ValueInJD { get; set; }
//        double ValueInSAR { get; set; }
//        double ValueInUSD { get; set; }
//        double ValueInEUR { get; set; }
//        string Unit { get; set; }
//        double Value { get; set; }
//    }
//    public class CurrencyItem: IUnit, ICurrencyItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<CurrencyItem>
//    {
//        #region NotifiedPropertyChanged
//        public event PropertyChangedEventHandler PropertyChanged;
//        protected void OnPropertyChanged(string name)
//        {
//            PropertyChangedEventHandler handler = PropertyChanged;
//            if (handler != null)
//            {
//                handler(this, new PropertyChangedEventArgs(name));
//            }
//        }

//        #endregion

//        public CurrencyItem()
//        {
                
//        }

//        #region ICurrencyItem





//        [JsonProperty("ValueInJD")]
//        private double _ValueInJD { get; set; } 
//        [JsonIgnore]
//        public double ValueInJD
//        {
//            get
//            {
//                return _ValueInJD;
//            }
//            set
//            {
//                if (_ValueInJD == value)
//                    return;
//                _ValueInJD = value;
//                OnPropertyChanged(nameof(ValueInJD));
//            }
//        }


//        [JsonProperty("ValueInSAR")]
//        private double _ValueInSAR { get; set; } 
//        [JsonIgnore]
//        public double ValueInSAR
//        {
//            get
//            {
//                return _ValueInSAR;
//            }
//            set
//            {
//                if (_ValueInSAR == value)
//                    return;
//                _ValueInSAR = value;
//                OnPropertyChanged(nameof(ValueInSAR));
//            }
//        }


//        [JsonProperty("ValueInUSD")]
//        private double _ValueInUSD { get; set; } 
//        [JsonIgnore]
//        public double ValueInUSD
//        {
//            get
//            {
//                return _ValueInUSD;
//            }
//            set
//            {
//                if (_ValueInUSD == value)
//                    return;
//                _ValueInUSD = value;
//                OnPropertyChanged(nameof(ValueInUSD));
//            }
//        }


//        [JsonProperty("ValueInEUR")]
//        private double _ValueInEUR { get; set; } 
//        [JsonIgnore]
//        public double ValueInEUR
//        {
//            get
//            {
//                return _ValueInEUR;
//            }
//            set
//            {
//                if (_ValueInEUR == value)
//                    return;
//                _ValueInEUR = value;
//                OnPropertyChanged(nameof(ValueInEUR));
//            }
//        }



//        [JsonProperty("Unit")]
//        private string _Unit { get; set; } = string.Empty;
//        [JsonIgnore]
//        public string Unit
//        {
//            get
//            {
//                return _Unit;
//            }
//            set
//            {
//                if (_Unit == value)
//                    return;
//                _Unit = value;
//                OnPropertyChanged(nameof(Unit));
//                UpdateWhenUnitChanged();
//            }
//        }

//        private void UpdateWhenUnitChanged()
//        {
//            throw new NotImplementedException();
//        }

//        [JsonProperty("Value")]
//        private double _Value { get; set; }
//        [JsonIgnore]
//        public double Value
//        {
//            get
//            {
//                return _Value;
//            }
//            set
//            {
//                if (_Value == value)
//                    return;
//                _Value = value;
//                OnPropertyChanged(nameof(Value));
//                UpdateWhenValueChanged();
//            }
//        }

//        private void UpdateWhenValueChanged()
//        {
//            if (Unit == U.USD)
//            {
//                double valueInUSD = Value;
//                ValueInUSD = valueInUSD;
                
//            }
//            else
//            {
//                double valueInC = Value;
//                ValueInKgPerM3 = valueInC;
//                ValueInLbPerFt3 = Converter.ConvertDensityFrom_KgPerM3_To_LbPerFt3(valueInC);
//            }
//        }


//        #endregion


//        #region MyRegion


//        #endregion


//        private CurrencyItem(double value, string unit)
//        {
//            Unit = unit;
//            Value = value;
//            StringValue = value.ToString();
//        }

//        public static class Factory
//        {
//            public static CurrencyItem Create(double value, string unit) { return new CurrencyItem(value, unit); }
//        }

//        public static CurrencyItem Create(CurrencyItem item)
//        {
//            return Factory.Create(item.Value, item.Unit);
//        }

//        public object ToLiquid()
//        {
//            return new
//            {
//                Value = Value.ToString(),
//                Unit,
//                ValueInJD,
//                ValueInSAR,
//                ValueInUSD,
//                ValueInEUR,
               
//            };
//        }

//        public int CompareTo(object obj)
//        {
//            if (obj is CurrencyItem)
//            {
//                return this.CompareTo((CurrencyItem)obj);
//            }
//            else
//            {
//                return 0;
//            }


//        }

//        public int CompareTo(CurrencyItem other)
//        {
//            if (this != null && other != null)
//            {
//                return this.Value.CompareTo(other.Value);
//            }
//            else
//            {
//                return 0;
//            }

//        }



//        public static List<string> GetUnits()
//        {
//            var list = new List<string>();
//            list.Add(U.LbPerFt3);
//            list.Add(U.KgPerM3);

//            return list;
//        }


//        public const string Name = nameof(CurrencyItem);

//        public static List<string> AllUnits { get; set; } = GetUnits();

//        public override string ToString()
//        {
//            return Value.ToString("N0") + " " + Unit;
//        }

//        public static CurrencyItem Parse(string s, IFormatProvider formatProvider)
//        {

//            var dValue = double.TryParse(s, out double r);
//            if (dValue)
//            {
//                return Factory.Create(r, U.KgPerM3);
//            }
//            else
//            {
//                Regex regex = new Regex(@"\d+(.)?\d+");
//                Match match = regex.Match(s);

//                var isNumber = double.TryParse(match.Value, out double v);

//                if (isNumber)
//                {
//                    return Factory.Create(v, U.KgPerM3);
//                }

//                return Factory.Create(0, U.KgPerM3);
//            }
//        }



//        public static string OwnerUnitPropertyName = "DensityUnit";

//        public static CurrencyItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
//        {

//            var dValue = double.TryParse(s, out double r);
//            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

//            if (dValue)
//            {
//                if (!string.IsNullOrWhiteSpace(unit))
//                {
//                    return CurrencyItem.Factory.Create(r, unit);
//                }
//                else
//                {
//                    return CurrencyItem.Factory.Create(r, U.KgPerM3);
//                }

//            }
//            else
//            {
//                Regex regex = new Regex(@"\d+(.)?\d+");
//                Match match = regex.Match(s);

//                var isNumber = double.TryParse(match.Value, out double v);

//                if (isNumber)
//                {
//                    if (!string.IsNullOrWhiteSpace(unit))
//                    {
//                        return CurrencyItem.Factory.Create(v, unit);
//                    }
//                    else
//                    {
//                        return CurrencyItem.Factory.Create(v, U.KgPerM3);
//                    }
//                }

//                if (!string.IsNullOrWhiteSpace(unit))
//                {
//                    return CurrencyItem.Factory.Create(0, unit);
//                }
//                else
//                {
//                    return CurrencyItem.Factory.Create(0, U.KgPerM3);
//                }


//            }
//        }

//        [JsonProperty("StringValue")]
//        private string _StringValue { get; set; } = string.Empty;
//        [JsonIgnore]
//        public string StringValue
//        {
//            get
//            {
//                return _StringValue;
//            }
//            set
//            {
//                if (_StringValue == value)
//                    return;
//                _StringValue = value;
//                OnPropertyChanged(nameof(StringValue));
//                this.UpdateValueWhenStringValueChanged("");
//            }
//        }

//        [OnDeserialized]
//        internal void OnDeserializedMethod(StreamingContext context)
//        {
//            this.StringValue = Value.ToString();
//        }

//    }
//}
