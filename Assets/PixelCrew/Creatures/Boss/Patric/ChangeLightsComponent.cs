using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace PixelCrew.Creatures.Boss.Patric
{
    public class ChangeLightsComponent : MonoBehaviour
    {
        [SerializeField] private Light2D[] _lights;

        [ColorUsage(true)] [SerializeField] private Color _color;

        [ContextMenu("Change")]
        public void SetColor()
        {
            SetColor(_color);
        }

        private void SetColor(Color color)
        {
            foreach (var light in _lights)
            {
                light.color = _color;
            }
        }
    }
}