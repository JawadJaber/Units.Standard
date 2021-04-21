

namespace Units.Standard
{
    public interface IUnit
    {
        string Unit { get; set; }
        double Value { get; set; }

        #region IUnit Methods
        void UpdateWhenUnitChanged();
        void UpdateWhenValueChanged();

        #endregion

        string ToString();

    }
}
