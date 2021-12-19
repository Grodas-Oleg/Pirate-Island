using System;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.ColliderBased
{
    public class ColliderCheck : LayerCheck
    {
        [SerializeField] private OnCollide _onCollide;

        private Collider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            _isTochingLayer = _collider.IsTouchingLayers(_layer);
            _onCollide.Invoke(gameObject);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            _isTochingLayer = _collider.IsTouchingLayers(_layer);
        }
        [Serializable]
        public class OnCollide : UnityEvent<GameObject>
        {
        }

    }
}