using System;
using PixelCrew.Components.GoBased;
using PixelCrew.Model;
using UnityEngine;
using UnityEngine.Events;

namespace PixelCrew.Components.LevelManagement
{
    [RequireComponent(typeof(SpawnComponent))]
    public class CheckPointComponent : MonoBehaviour
    {
        [SerializeField] private string _id;
        [SerializeField] private SpawnComponent _heroSpawner;
        [SerializeField] private UnityEvent _setCheked;
        [SerializeField] private UnityEvent _setUncheked;

        public string Id => _id;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            if (_session.IsChecked(_id))
                _setCheked?.Invoke();
            else
                _setUncheked?.Invoke();
        }

        public void Check()
        {
            _session.SetChecked(_id);
            _setCheked?.Invoke();
        }

        public void SpawnHero()
        {
            _heroSpawner.Spawn();
        }
    }
}