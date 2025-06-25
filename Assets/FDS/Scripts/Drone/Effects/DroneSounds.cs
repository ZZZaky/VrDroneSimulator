using FDS.Drone.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FDS.Drone.Effects
{
    [RequireComponent(typeof(AudioSource))]
    public class DroneSounds : MonoBehaviour
    {
        #region Fields

        [SerializeField]
        private DroneEngine engine;

        [SerializeField]
        private float averageAngularVelocity;

        [SerializeField]
        private float pitchChangeSpeed;

        [SerializeField]
        private float pitchDelta;

        [SerializeField]
        private float velocityToPitchCoefficient;

        private AudioSource _audioSource;

        #endregion

        #region LyfeCycle

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            var audioPitch = Mathf.Clamp((engine.AngularVelocity - averageAngularVelocity) * velocityToPitchCoefficient, -pitchDelta, pitchDelta);
            _audioSource.pitch = Mathf.Lerp(_audioSource.pitch, 1 + audioPitch, pitchChangeSpeed);
        }

        #endregion
    }
}
