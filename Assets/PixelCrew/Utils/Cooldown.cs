using System;
using UnityEngine;

namespace PixelCrew.Utils
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField] private float _value;
        private float _timesUp;

        public float Value
        {
            get => _value;
            set => _value = value;
        }

        public void Reset()
        {
            _timesUp = Time.time + _value;
        }

        public float TimeLast => Mathf.Max(_timesUp - Time.deltaTime, 0);

        public bool IsReady => _timesUp <= Time.time;
    }
}