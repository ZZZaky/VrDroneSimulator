namespace FDS.Quadrocopter
{
    public struct QuadrocopterEnginesPower
    {
        #region Fields

        public readonly float ForwardLeftPower;
        public readonly float ForwardRightPower;
        public readonly float BackwardLeftPower;
        public readonly float BackwardRightPower;

        #endregion

        #region Constructors

        public QuadrocopterEnginesPower(float forwardLeftPower, float forwardRightPower, float backwardLeftPower, float backwardRightPower)
        {
            ForwardLeftPower = forwardLeftPower;
            ForwardRightPower = forwardRightPower;
            BackwardLeftPower = backwardLeftPower;
            BackwardRightPower = backwardRightPower;
        }

        #endregion
    }
}
