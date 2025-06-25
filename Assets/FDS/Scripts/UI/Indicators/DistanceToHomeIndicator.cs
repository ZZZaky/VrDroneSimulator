using System.Collections;
using System.Collections.Generic;

namespace FDS.UI.Indicators
{
    public class DistanceToHomeIndicator : DronePropertyIndicator
    {
        #region LifeCylce

        private void Update()
        {
            _label.text = (drone.Home - drone.BodyTransform.position).magnitude.ToString("F");
        }

        #endregion
    }
}
