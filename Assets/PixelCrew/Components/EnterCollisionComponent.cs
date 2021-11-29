using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components
{
    public class EnterCollisionComponent : MonoBehaviour
    {

        [SerializeField] private string[] _tags;
        [SerializeField] private EnterEvent _action;

        private void OnCollisionEnter2D(Collision2D other)
        {
            foreach (var _tag in _tags)
            {
                if (other.gameObject.CompareTag(_tag))
                {
                    _action?.Invoke(other.gameObject);
                }
            }
        }

        [Serializable]

        public class EnterEvent : UnityEvent<GameObject>
        {

        }

    }
}

