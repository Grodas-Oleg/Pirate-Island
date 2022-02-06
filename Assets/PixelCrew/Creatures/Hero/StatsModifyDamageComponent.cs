using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PixelCrew.Creatures.Hero
{
    public class StatsModifyDamageComponent : MonoBehaviour
    {
        [SerializeField] private HealthChangeComponent _damageValue;
        private bool _critical;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;
        private float _damage;

        private void Start()
        {
            _session = GameSession.Instance;

            _damageValue.SetDelta((int) -ModifyDamage(), _critical);

            _trash.Retain(_session.StatsModel.Subscribe(SetDamage));
            SetDamage();
        }

        private void SetDamage()
        {
            _damageValue.SetDelta((int) -ModifyDamage(), _critical);
        }

        private float ModifyDamage()
        {
            var go = gameObject.tag;
            var perkCriticalChance = _session.StatsModel.GetValue(StatId.CriticalChance);
            var perkCriticalDamage = _session.StatsModel.GetValue(StatId.CriticalDamage);
            switch (go)
            {
                case "RangeAttack":
                    var rangeValue = _session.StatsModel.GetValue(StatId.RangeDamage);
                    _damage = (int) rangeValue;
                    break;
                case "CannonAttack":
                    var cannonValue = _session.StatsModel.GetValue(StatId.CanonDamage);
                    _damage = (int) cannonValue;
                    break;
            }

            if (!(Random.value * 100 <= perkCriticalChance)) return _damage;
            _damage *= perkCriticalDamage;
            _critical = true;

            return _damage;
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}