using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FDS.UI.Indicators
{
    public class FlyingHeightIndicator : DronePropertyIndicator
    {
        #region LifeCycle

        private void Update()
        {
            var currentY = Mathf.Abs(drone.BodyTransform.position.y - drone.Home.y);
            _label.text = currentY.ToString("F");
        }

        #endregion
    }
}