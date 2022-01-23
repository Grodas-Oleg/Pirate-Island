using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget _healthBar;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _session = GameSession.Instance;
            _trash.Retain(_session.Data.HP.SubscribeAndInvoke(OnHealthChanged));
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
            var value = (float) newValue / maxHealth;
            _healthBar.SetProgress(value);
        }

        public void OnPause()
        {
            WindowUtils.CreateWindow("UI/PauseMenuWindow");
        }

        public void OnStat()
        {
            WindowUtils.CreateWindow("UI/PlayerStatsWindow");
        }
        
        public void OnControlsGuide()
        {
            WindowUtils.CreateWindow("UI/ControlsGuideWindow");
        }

        public void OnInventory()
        {
            WindowUtils.CreateWindow("UI/BigInventoryWindow");
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}