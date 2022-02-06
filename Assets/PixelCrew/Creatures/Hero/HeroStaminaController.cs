using System.Collections;
using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class HeroStaminaController : MonoBehaviour
    {
        private int _maxStamina;
        private Coroutine _regen;
        private int _currentStamina;
        private GameSession _session;

        private void Awake()
        {
            _session = GameSession.Instance;
        }

        public void SetStamina(int stamina)
        {
            _session.Data.Stamina.Value = stamina;
        }

        public void UseStamina(int value)
        {
            var maxStamina = _session.StatsModel.GetValue(StatId.Stamina);
            _maxStamina = (int) maxStamina;
            if (_session.Data.Stamina.Value - value < 0) return;

            _session.Data.Stamina.Value -= value;
            if (_regen != null)
                StopCoroutine(_regen);

            _regen = StartCoroutine(RegenStamina());
        }

        private IEnumerator RegenStamina()
        {
            var waitForSeconds = new WaitForSeconds(0.08f);
            yield return new WaitForSeconds(2);
            while (_session.Data.Stamina.Value < _maxStamina)
            {
                _session.Data.Stamina.Value += _maxStamina / 50;
                yield return waitForSeconds;
            }

            _regen = null;
        }
    }
}