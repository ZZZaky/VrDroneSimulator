namespace FDS.Drone.Inputs
{
    public struct DroneAngles
    {
        #region Fields

        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float Roll { get; set; }

        #endregion

        #region Constructors

        public DroneAngles(float pitch, float yaw, float roll)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }

        #endregion
    }
}
