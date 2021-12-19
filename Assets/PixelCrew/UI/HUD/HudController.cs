using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils;
using UnityEngine;

namespace PixelCrew.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _session.Data.HP.OnChanged += OnHealthChanged;

            OnHealthChanged(_session.Data.HP.Value, 0);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = DefsFacade.I.Player.MaxHealth;
            var value = (float) newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        public void OnPause()
        {
            WindowUtils.CreateWindow("UI/PauseMenuWindow");
        }

        private void OnDestroy()
        {
            _session.Data.HP.OnChanged -= OnHealthChanged;
        }
    }
}