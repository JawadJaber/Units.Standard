using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Units.Standard.UnitTest
{
    [TestClass]
    public class DensityItemTests
    {
        [TestMethod]
        public void DensityItem_TestMethod()
        {
            DensityItem item = DensityItem.Factory.Create(1000,U.KgPerM3);
            TestUnitClass(item, new string[] { U.KgPerM3, U.LbPerFt3 }, 1000, 9);
        }


        [TestMethod]
        public void Viscosity_TestMethod()
        {
            ViscosityItem item = ViscosityItem.Factory.Create(1000, U.cP);
            TestUnitClass(item, new string[] { U.KgPerMS, U.cP }, 1000, 9);
        }

        [TestMethod]
        public void Sufice_TestMethod()
        {
            SurfaceTenstionItem item = SurfaceTenstionItem.Factory.Create(1, U.NewtonPerM);
            TestUnitClass(item, new string[] { U.NewtonPerM, U.DynePerCM }, 9, 9);
        }


        [TestMethod]
        public void TempItem_TestMethod()
        {
            TemperatureItem item = new TemperatureItem(80, 24, U.C);
            TestUnitClass(item, new string[] { U.C, U.F }, 24,90);

        }


        [TestMethod]
        public void Velocity_TestMethod()
        {
            VelocityItem item = VelocityItem.Factory.Create(1000, U.FPM);
            TestUnitClass(item, new string[] { U.FPM, U.MPS }, 1000, 9);
        }


        [TestMethod]
        public void Weight_TestMethod()
        {
            WeightItem item = WeightItem.Factory.Create(1000, U.Lb);
            TestUnitClass(item, new string[] { U.Kg, U.Lb }, 1000, 9);
        }

        private static void TestUnitClass(IUnit item, string[] units, double v1, double v2)
        {
            
            foreach (var unit in units)
            {
                item.Unit = unit;
                Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine();

            item.Value += v1;

            foreach (var unit in units)
            {
                item.Unit = unit;
                Console.WriteLine($"{JsonConvert.SerializeObject(item)}");
                Console.WriteLine();
            }

        }
    }
}
