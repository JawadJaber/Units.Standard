using DotLiquid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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

        private double _Value { get; set; }
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
    }
   
}
