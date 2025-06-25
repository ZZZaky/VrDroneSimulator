namespace FDS.Drone.Inputs.Converters
{
    public class ValueMappingInputConverter : DroneInputConverter
    {
        #region Public Methods

        public override float GetConvertedAxisValue(float inputAxis)
        {
            return (inputAxis + 1) / 2;
        }

        #endregion
    }
}
