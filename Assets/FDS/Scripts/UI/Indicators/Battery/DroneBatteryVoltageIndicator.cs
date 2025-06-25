namespace FDS.UI.Indicators.Battery
{
    public class DroneBatteryVoltageIndicator : DroneBatteryIndicator
    {
        #region LifeCycle

        private void Update()
        {
            _label.text = $"{battery.Voltage.ToString("F")}V";
        }

        #endregion
    }
}
