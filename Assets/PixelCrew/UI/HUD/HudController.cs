using PixelCrew.Model;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.UI.HUD.Dialogs;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.UI.HUD
{
    public class HudController : MonoBehaviour
    {
        [SerializeField] private ProgressBarWidget healthBar;
        [SerializeField] private ProgressBarWidget fuelBar;
        [SerializeField] private ProgressBarWidget staminaBar;
        [SerializeField] private InputAction pause;
        private bool _paused;

        private GameSession _session;
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _session = GameSession.Instance;
            _trash.Retain(_session.Data.HP.SubscribeAndInvoke(OnHealthChanged));
            _trash.Retain(_session.Data.Fuel.SubscribeAndInvoke(OnFuelChanged));
            _trash.Retain(_session.Data.Stamina.SubscribeAndInvoke(OnStaminaChanged));

            pause.performed += _ => OnPause();
        }

        private void OnEnable()
        {
            pause.Enable();
        }

        private void OnDisable()
        {
            pause.Disable();
        }

        private void OnFuelChanged(float newValue, float oldValue)
        {
            const int maxFuel = 100;
            var value = newValue / maxFuel;
            fuelBar.SetProgress(value);
        }

        private void OnHealthChanged(int newValue, int oldValue)
        {
            var maxHealth = _session.StatsModel.GetValue(StatId.Hp);
            var value = newValue / maxHealth;
            healthBar.SetProgress(value);
        }

        private void OnStaminaChanged(int newValue, int oldValue)
        {
            var maxStamina = _session.StatsModel.GetValue(StatId.Stamina);
            var value = newValue / maxStamina;
            staminaBar.SetProgress(value);
        }

        public void OnPause()
        {
            _paused = !_paused;
            if (_paused)
                WindowUtils.CreateWindow("UI/PauseMenuWindow");
            else
                Destroy(GameObject.FindWithTag("PauseMenu"));
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