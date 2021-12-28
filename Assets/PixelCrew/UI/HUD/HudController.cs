using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;
        [SerializeField] private ActivePerkWidget _activePerk;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _trash.Retain(_session.Data.HP.SubscribeAndInvoke(OnHealthChanged));
            _trash.Retain(_session.PerksModel.Subscribe(OnPerkChanged));

            OnPerkChanged();
        }

        private void OnPerkChanged()
        {
            var usedPerkId = _session.PerksModel.Used;
            var hasPerk = !string.IsNullOrEmpty(usedPerkId);
            if (hasPerk)
            {
                _activePerk.Set(DefsFacade.I.Perks.Get(usedPerkId));
            }

            _activePerk.gameObject.SetActive(hasPerk);
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
            _trash.Dispose();
        }
    }
}