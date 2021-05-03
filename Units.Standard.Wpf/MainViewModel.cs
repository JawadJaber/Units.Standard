using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Units.Standard.Wpf
{
    public interface IMainViewModel
    {
        CapacityItem Capacity { get; set; }
        TemperatureItem Temperature { get; set; }
        CoilLengthItem Length { get; set; }
        PressureDropITem Pressure { get; set; }
        AirFlowItem AirFlow { get; set; }
        WaterFlowItem WaterFlow { get; set; }
        WeightItem Weight { get; set; }
        AreaItem Area { get; set; }

    }

    public class MainViewModel:INotifyPropertyChanged
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

        public static List<string> CapacityUnits { get; set; } = CapacityItem.GetUnits();
        public static List<string> TempUnits { get; set; } = TemperatureItem.GetUnits();
        public static List<string> LengthUnits { get; set; } = CoilLengthItem.GetUnits();
        public static List<string> PressureUnits { get; set; } = PressureDropITem.GetUnits();
        public static List<string> AirFlowUnits { get; set; } = AirFlowItem.GetUnits();
        public static List<string> WaterFlowUnits { get; set; } = WaterFlowItem.GetUnits();
        public static List<string> WeightUnits { get; set; } = WeightItem.GetUnits();
        public static List<string> AreaUnits { get; set; } = AreaItem.GetUnits();


        public MainViewModel()
        {

        }

        #region Properties



        private CapacityItem _Capacity { get; set; } =  CapacityItem.Factory.Create(100,U.TR);
        public CapacityItem Capacity
        {
            get
            {
                return _Capacity;
            }
            set
            {
                if (_Capacity == value)
                    return;
                _Capacity = value;
                OnPropertyChanged(nameof(Capacity));
            }
        }


        private TemperatureItem _Temperature { get; set; } =  TemperatureItem.Factory.Create(30,U.C);
        public TemperatureItem Temperature
        {
            get
            {
                return _Temperature;
            }
            set
            {
                if (_Temperature == value)
                    return;
                _Temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }


        private CoilLengthItem _Length { get; set; } =  CoilLengthItem.Factory.Create(100,U.mm);
        public CoilLengthItem Length
        {
            get
            {
                return _Length;
            }
            set
            {
                if (_Length == value)
                    return;
                _Length = value;
                OnPropertyChanged(nameof(Length));
            }
        }


        private PressureDropITem _Pressure { get; set; } =  PressureDropITem.Factory.Create(20,U.Pa);
        public PressureDropITem Pressure
        {
            get
            {
                return _Pressure;
            }
            set
            {
                if (_Pressure == value)
                    return;
                _Pressure = value;
                OnPropertyChanged(nameof(Pressure));
            }
        }


        private AirFlowItem _AirFlow { get; set; } =  AirFlowItem.Factory.Create(400,U.CFM);
        public AirFlowItem AirFlow
        {
            get
            {
                return _AirFlow;
            }
            set
            {
                if (_AirFlow == value)
                    return;
                _AirFlow = value;
                OnPropertyChanged(nameof(AirFlow));
            }
        }


        private WaterFlowItem _WaterFlow { get; set; } =  WaterFlowItem.Factory.Create(300,U.LpS);
        public WaterFlowItem WaterFlow
        {
            get
            {
                return _WaterFlow;
            }
            set
            {
                if (_WaterFlow == value)
                    return;
                _WaterFlow = value;
                OnPropertyChanged(nameof(WaterFlow));
            }
        }


        private WeightItem _Weight { get; set; } =  WeightItem.Factory.Create(40,U.Kg);
        public WeightItem Weight
        {
            get
            {
                return _Weight;
            }
            set
            {
                if (_Weight == value)
                    return;
                _Weight = value;
                OnPropertyChanged(nameof(Weight));
            }
        }


        private AreaItem _Area { get; set; } =  AreaItem.Factory.Create(30,U.SqM);
        public AreaItem Area
        {
            get
            {
                return _Area;
            }
            set
            {
                if (_Area == value)
                    return;
                _Area = value;
                OnPropertyChanged(nameof(Area));
            }
        }


        #endregion

    }
}
