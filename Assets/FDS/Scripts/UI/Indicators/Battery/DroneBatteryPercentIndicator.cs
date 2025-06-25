namespace FDS.UI.Indicators.Battery
{
    public class DroneBatteryPercentIndicator : DroneBatteryIndicator
    {
        #region LifeCycle

        private void OnEnable()
        {
            battery.OnBatteryLevelChanged += ChangeTextColor;
        }

        private void OnDisable()
        {
            battery.OnBatteryLevelChanged -= ChangeTextColor;
        }

        private void Update()
        {
            _label.text = $"{battery.Percent.ToString("F")}%";
        }

        #endregion

        #region PrivateMethods

        private void ChangeTextColor(bool isBatteryLow)
        {
            if (isBatteryLow)
            {
                _label.color = lowBatteryColor;
            }
            else
            {
                _label.color = _defaultColor;
            }
        }

        #endregion
    }
}
