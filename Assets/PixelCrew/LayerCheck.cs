using UnityEngine;

namespace PixelCrew
{
    public class LayerCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _layer;
        [SerializeField] private bool _isTochingLayer;
        private Collider2D _collider;

        public bool IsTouchingLayer => _isTochingLayer;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _isTochingLayer = _collider.IsTouchingLayers(_layer);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isTochingLayer = _collider.IsTouchingLayers(_layer);
        }
    }
}