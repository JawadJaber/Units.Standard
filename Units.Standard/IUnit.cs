

using Newtonsoft.Json.Linq;

namespace Units.Standard
{
    public interface IUnit
    {
        string Unit { get; set; }
        double Value { get; set; }

        string StringValue { get; set; }

        #region IUnit Methods
        void UpdateWhenUnitChanged();
        void UpdateWhenValueChanged();

        #endregion

        string ToString();

    }


    public static class IUnitExtMethods
    {
        public static void UpdateValueWhenStringValueChanged(this IUnit unit, string DefaultParameter)
        {
            var isDouble = double.TryParse(unit.StringValue, out double d);
            if (isDouble)
            {
                if (d != 0)
                {
                    unit.Value = d;
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(DefaultParameter))
                    {
                        unit.StringValue = DefaultParameter;
                    }
                    else
                    {
                        unit.Value = 0;
                    }
                }
            }
            else
            {
                unit.StringValue = "";
                unit.Value = 0;
            }
        }
    }
}
