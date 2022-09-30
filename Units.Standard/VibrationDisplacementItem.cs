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

    public interface IVibrationDisplacement
    {
        double ValueInMils { get; set; }
        double ValueInMM { get; set; }
    }

    public class VibrationDisplacementItem : INotifyPropertyChanged, IUnit, IVibrationDisplacement, ILiquidizable, IComparable, IComparable<VibrationDisplacementItem>
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

        #region IVibrationDisplacement



        private double _ValueInMils { get; set; }
        public double ValueInMils
        {
            get
            {
                return _ValueInMils;
            }
            set
            {
                if (_ValueInMils == value)
                    return;
                _ValueInMils = value;
                OnPropertyChanged(nameof(ValueInMils));
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
                case U.mils:
                    Value = ValueInMils;
                    break;
                case U.mm:
                    Value = ValueInMM;
                    break;
              

                default:
                    break;
            }
        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.mils)
            {
                double valueInMils = Value;
                ValueInMils = valueInMils;
                ValueInMM = Converter.ConvertLengthFrom_Mils_To_MM(valueInMils);
                
            }
            else
            {
                double valueInMM = Value;
                ValueInMM = valueInMM;
                ValueInMils = Converter.ConvertLengthFrom_MM_To_Mils(valueInMM);
                //check this in inlets_vibrations..
                
            }

           
        }


        #endregion

        #region Construction

        public VibrationDisplacementItem(double value, string unit)
        {
            Unit = unit;
            Value = value;
            StringValue = value.ToString();
        }

        public VibrationDisplacementItem()
        {

        }

        public static class Factory
        {
            public static VibrationDisplacementItem Create(double value, string unit) { return new VibrationDisplacementItem(value, unit); }
        }

        public static VibrationDisplacementItem Create(VibrationDisplacementItem item)
        {
            return Factory.Create(item.Value, item.Unit);
        }

        public object ToLiquid()
        {
            return new
            {
                Value,
                Unit,
                ValueInMils,
                ValueInMM,
                
            };
        }

        #endregion


        public int CompareTo(object obj)
        {
            if (obj is VibrationDisplacementItem)
            {
                return this.CompareTo((VibrationDisplacementItem)obj);
            }
            else
            {
                return 0;
            }


        }

        public int CompareTo(VibrationDisplacementItem other)
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
            list.Add(U.mm);
            list.Add(U.mils);
           
            return list;
        }


        public const string Name = nameof(VibrationDisplacementItem);

        public static List<string> AllUnits { get; set; } = GetUnits();

        public override string ToString()
        {
            return Value.ToDouble_SigFig(2) + " " + Unit;
        }


        public static VibrationDisplacementItem Parse(string s, IFormatProvider formatProvider)
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


        public static string OwnerUnitPropertyName = "VibrationDisplacementUnit";

        public static VibrationDisplacementItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {

            var dValue = double.TryParse(s, out double r);
            var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

            if (dValue)
            {
                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return VibrationDisplacementItem.Factory.Create(r, unit);
                }
                else
                {
                    return VibrationDisplacementItem.Factory.Create(r, U.mm);
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
                        return VibrationDisplacementItem.Factory.Create(v, unit);
                    }
                    else
                    {
                        return VibrationDisplacementItem.Factory.Create(v, U.mm);
                    }
                }

                if (!string.IsNullOrWhiteSpace(unit))
                {
                    return VibrationDisplacementItem.Factory.Create(0, unit);
                }
                else
                {
                    return VibrationDisplacementItem.Factory.Create(0, U.mm);
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
