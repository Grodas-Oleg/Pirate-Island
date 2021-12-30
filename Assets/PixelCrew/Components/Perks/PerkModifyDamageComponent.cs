using System;
using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PixelCrew.Components.Perks
{
    public class PerkModifyDamageComponent : MonoBehaviour
    {
        [SerializeField] private HealthChangeComponent _damageValue;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public event Action<StatId> OnChanged;

        private GameSession _session;
        private int _damage;
        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.StatsModel.Subscribe(() => OnChanged?.Invoke(StatId.Damage)));
            _trash.Retain(_session.StatsModel.Subscribe(() => OnChanged?.Invoke(StatId.RangeDamage)));
            _trash.Retain(_session.StatsModel.Subscribe(() => OnChanged?.Invoke(StatId.CriticalChance)));
            _damageValue._value = -ModifyDamage();
        }

        private int ModifyDamage()
        {
            var go = gameObject.tag;
            var rangeValue = _session.StatsModel.GetValue(StatId.RangeDamage);
            var meleeValue = _session.StatsModel.GetValue(StatId.Damage);

            switch (go)
            {
                case "MeleeAttack":
                    _damage = (int) meleeValue;
                    break;
                case "RangeAttack":
                    _damage = (int) rangeValue;
                    break;
            }

            var perkCriticalChance = _session.StatsModel.GetValue(StatId.CriticalChance);
            var chance = Random.Range(1f, 100f);
            if (!(chance < perkCriticalChance))
                return _damage;

            _damage *= 2;
            return _damage;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}