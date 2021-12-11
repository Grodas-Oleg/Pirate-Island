using UnityEngine;

namespace PixelCrew.Components.ColliderBased
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] protected LayerMask _layer;
        [SerializeField] protected bool _isTochingLayer;
        public bool IsTouchingLayer => _isTochingLayer;
    }
}