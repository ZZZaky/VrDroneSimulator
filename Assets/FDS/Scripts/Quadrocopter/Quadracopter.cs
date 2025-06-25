using FDS.Drone.Components;
using UnityEngine;

namespace FDS.Quadrocopter
{
    public class Quadracopter : FDS.Drone.Drone
    {
        #region Fields

        [SerializeField]
        private DroneEngine forwardLeftEngine;

        [SerializeField]
        private DroneEngine forwardRightEngine;

        [SerializeField]
        private DroneEngine backwardLeftEngine;

        [SerializeField]
        private DroneEngine backwardRightEngine;

        [SerializeField]
        private QuadrocopterController quadrocopterController;

        #endregion

        #region Protected Methods

        protected override void Stabilize()
        {
            var newEnginesPower = quadrocopterController.GetEnginesPower();
            forwardLeftEngine.SetPower(newEnginesPower.ForwardLeftPower);
            forwardRightEngine.SetPower(newEnginesPower.ForwardRightPower);
            backwardLeftEngine.SetPower(newEnginesPower.BackwardLeftPower);
            backwardRightEngine.SetPower(newEnginesPower.BackwardRightPower);
        }

        protected override void StopEngines()
        {
            forwardLeftEngine.SetPower(0);
            forwardRightEngine.SetPower(0);
            backwardLeftEngine.SetPower(0);
            backwardRightEngine.SetPower(0);
        }

        #endregion
    }
}
