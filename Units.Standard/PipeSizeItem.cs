using DotLiquid;
using StdHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Units.Standard
{
    public interface IPipeSizeItem
    {
        string ValueInMM { get; set; }
        string ValueInInch { get; set; }
    }

    public class PipeSizeTableItem : IPipeSizeItem
    {
        public string ValueInMM { get; set; } = "";
        public string ValueInInch { get; set; } = "";

        public static List<PipeSizeTableItem> AllData = new List<PipeSizeTableItem>(GetData());

        public static List<PipeSizeTableItem> GetData()
        {
            var list = new List<PipeSizeTableItem>(); ;
            //list.Add(new PipeSizeTableItem() { ValueInMM = "6", ValueInInch = "1/8" });
            //list.Add(new PipeSizeTableItem() { ValueInMM = "8", ValueInInch = "1/4" });
            //list.Add(new PipeSizeTableItem() { ValueInMM = "10", ValueInInch = "3/8" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "15", ValueInInch = "1/2" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "20", ValueInInch = "3/4" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "25", ValueInInch = "1" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "32", ValueInInch = "1 1/4" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "40", ValueInInch = "1 1/2" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "50", ValueInInch = "2" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "65", ValueInInch = "2 1/2" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "80", ValueInInch = "3" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "90", ValueInInch = "3 1/2" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "100", ValueInInch = "4" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "125", ValueInInch = "5" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "150", ValueInInch = "6" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "200", ValueInInch = "8" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "250", ValueInInch = "10" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "300", ValueInInch = "12" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "350", ValueInInch = "14" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "400", ValueInInch = "16" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "450", ValueInInch = "18" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "500", ValueInInch = "20" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "550", ValueInInch = "22" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "600", ValueInInch = "24" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "650", ValueInInch = "26" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "700", ValueInInch = "28" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "750", ValueInInch = "30" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "800", ValueInInch = "32" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "850", ValueInInch = "34" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "900", ValueInInch = "36" });
            list.Add(new PipeSizeTableItem() { ValueInMM = "950", ValueInInch = "38" });



            return list;
        }

        public static List<string> GetUnits()
        {
            var list = new List<string>();
            list.Add(U.mm);
            list.Add(U.inch);

            return list;
        }

        public const string Name = nameof(PipeSizeTableItem);

        public static List<string> AllUnits { get; set; } = GetUnits();


    }

    public class PipeSizeItem : IPipeSizeItem, INotifyPropertyChanged, ILiquidizable, IComparable, IComparable<PipeSizeItem>
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


        #region IUnit

        private string _Unit { get; set; }
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

        private string _Value { get; set; }
        public string Value
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
                if(Value != null)
                {
                    if (Value.Contains(U.mm))
                    {
                        Value = Value.Replace(U.mm, "");
                        Value = Value.Replace(" ", "");
                    }

                    if (Value.Contains(U.inch))
                    {
                        Value = Value.Replace(U.inch, "");
                        Value = Value.Replace(" ", "");
                    }
                }
              

                UpdateWhenValueChanged();
            }
        }





        private string _ValueInMM { get; set; } = string.Empty;
        public string ValueInMM
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


        private string _ValueInInch { get; set; } = string.Empty;
        public string ValueInInch
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



        public void UpdateWhenUnitChanged()
        {
            if (Unit == U.mm)
            {
                Value = ValueInMM;
            }
            else if (Unit == U.inch)
            {
                Value = ValueInInch;
            }


        }

        public void UpdateWhenValueChanged()
        {
            if (Unit == U.mm)
            {
                string valueImMM = Value;
                ValueInMM = valueImMM;
                ValueInInch = PipeSizeTableItem.AllData.Where(x => x.ValueInMM == valueImMM).FirstOrDefault()?.ValueInInch;
            }
            else if (Unit == U.inch)
            {
                string valueInInch = Value;
                ValueInInch = valueInInch;
                ValueInMM = PipeSizeTableItem.AllData.Where(x => x.ValueInInch == valueInInch).FirstOrDefault()?.ValueInMM;
            }

        }

        public object ToLiquid()
        {
            return new
            {
                ValueInMM,
                ValueInInch,
                Value,
                Unit
            };

        }

        public int CompareTo(object obj)
        {
            var obj_cast = obj as PipeSizeItem;
            if (obj_cast != null)
            {
                return CompareTo(obj_cast);
            }
            else
            {
                return 0;
            }
        }

        public int CompareTo(PipeSizeItem other)
        {
            if (this.ValueInInch == other.ValueInInch || this.ValueInMM == other.ValueInMM)
            {
                return 1;
            }
            else
            {
                return 0;
            }

        }

        #endregion

        #region Construction

        public static class Factory
        {
            public static PipeSizeItem Create(string value, string unit) { return new PipeSizeItem(value, unit); }
        }



        public PipeSizeItem()
        {

        }

        public PipeSizeItem(string value, string unit)
        {
            Unit = unit;
            Value = value;
        }



        #endregion

        public static List<string> GetUnits()
        {
            var list = new List<string>();
            list.Add(U.mm);
            list.Add(U.inch);

            return list;
        }

        public const string Name = nameof(PipeSizeItem);

        public static List<string> AllUnits { get; set; } = GetUnits();


        public static PipeSizeItem Create(PipeSizeItem item)
        {
            return PipeSizeItem.Factory.Create(item.Value, item.Unit);
        }

        public override string ToString()
        {
            return Value + " " + Unit;
        }

        public static PipeSizeItem Parse(string s, IFormatProvider formatProvider)
        {

            var dValue = PipeSizeTableItem.AllData.Where(xi => xi.ValueInMM == s || xi.ValueInInch == s).FirstOrDefault();
            if (dValue != null)
            {
                var ss_value = PipeSizeTableItem.AllData.Where(xi => xi.ValueInMM == s).FirstOrDefault();
                if (ss_value != null)
                {
                    return Factory.Create(ss_value.ValueInMM, U.mm);
                }
                else
                {
                    return Factory.Create(ss_value.ValueInInch, U.inch);
                }

                
            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {
                    return Factory.Create(s, U.mm);
                }

                return Factory.Create(PipeSizeTableItem.AllData.First().ValueInMM, U.mm);
            }
        }



        public static string OwnerUnitPropertyName = "PipeSizingUnit";

        public static PipeSizeItem Parse(string s, IHashable hashable, IFormatProvider formatProvider)
        {


            var dValue = PipeSizeTableItem.AllData.Where(xi => xi.ValueInMM == s || xi.ValueInInch == s).FirstOrDefault();
            if (dValue != null)
            {
                var ss_value = PipeSizeTableItem.AllData.Where(xi => xi.ValueInMM == s).FirstOrDefault();
                if (ss_value != null)
                {
                    return Factory.Create(ss_value.ValueInMM, U.mm);
                }
                else
                {
                    return Factory.Create(ss_value.ValueInInch, U.inch);
                }


            }
            else
            {
                Regex regex = new Regex(@"\d+(.)?\d+");
                Match match = regex.Match(s);

                var isNumber = double.TryParse(match.Value, out double v);

                if (isNumber)
                {

                    var unit = hashable.GetHashableUnit(OwnerUnitPropertyName);

                    if (!string.IsNullOrWhiteSpace(unit))
                    {
                        return PipeSizeItem.Factory.Create(s, unit);
                    }
                    else
                    {
                        return PipeSizeItem.Factory.Create(s, U.mm);
                    }

                  
                }

                return Factory.Create(PipeSizeTableItem.AllData.First().ValueInMM, U.mm);
            }



          
        }

    }
}
