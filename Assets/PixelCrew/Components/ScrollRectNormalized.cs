using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.Components
{
    public class ScrollRectNormalized : MonoBehaviour
    {
        [SerializeField] private ScrollRect rect;

        private void Start()
        {
            rect.verticalNormalizedPosition = 1.5f;
        }
    }
}