using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace PixelCrew.Creatures.Hero.Features
{
    public class HeroFlashLight : MonoBehaviour
    {
        [SerializeField] private float _consumePerSecond;
        [SerializeField] private Light2D _light;

        private float _defaultIntensity;
        private GameSession _session;

        private void Start()
        {
            _defaultIntensity = _light.intensity;
            _session = GameSession.Instance;
        }

        private void Update()
        {
            var consumed = Time.deltaTime * _consumePerSecond;
            var currentValue = _session.Data.Fuel.Value;
            var nextValue = currentValue - consumed;
            nextValue = Mathf.Max(nextValue, 0);
            _session.Data.Fuel.Value = nextValue;

            var progress = Mathf.Clamp(nextValue / 20, 0, 1);
            _light.intensity = _defaultIntensity * progress;
        }
    }
}