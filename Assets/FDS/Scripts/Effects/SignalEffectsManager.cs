using FDS.Drone.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace FDS.Effects
{
    public class SignalEffectsManager : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private SignalPowerProvider signalPowerProvider;

        [SerializeField]
        private List<SignalEffect> effects;

        private float _lastPower = float.MinValue;

        #endregion

        #region LifeCycle
        
        private void Update()
        {
            var power = signalPowerProvider.GetSignalPower();
            if (power != _lastPower)
            {
                foreach (var data in effects)
                    data.UpdateEffect(signalPowerProvider.GetSignalPower());
            }
            _lastPower = power;
        }

        #endregion
    }
}
