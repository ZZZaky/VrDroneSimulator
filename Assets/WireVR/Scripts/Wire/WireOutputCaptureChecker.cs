using System.Collections.Generic;
using Tools;
using WireVR.General;

namespace WireVR.Wires
{
    public class WireOutputCaptureChecker : Singleton<WireOutputCaptureChecker>
    {
        #region Fields

        private List<HandObjectTracker> _handObjectTrackers = new List<HandObjectTracker>();

        #endregion

        #region Public Methods

        public bool IsGrabbed(WireOutput wireOutput)
        {
            SetUpHandTrackers();

            foreach(var handObjectTracker in _handObjectTrackers)
                if (handObjectTracker.IsLastCapturedObject(wireOutput))
                {
                    return true;
                }

            return false;
        }

        #endregion

        #region Private Methods

        private void SetUpHandTrackers()
        {
            if (_handObjectTrackers.Count == 0)
            {
                foreach(var handObjectTracker in FindObjectsOfType<HandObjectTracker>())
                _handObjectTrackers.Add(handObjectTracker);
            }
        }

        #endregion
    }
}
