using System.Collections;
using PixelCrew.Components.Health;
using PixelCrew.Creatures.Boss.Knight;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class BossHealthWidget : MonoBehaviour
    {
        [SerializeField] private HealthComponent _health;
        [SerializeField] private ProgressBarWidget _hpBar;
        [SerializeField] private CanvasGroup _canvas, _group;
        [SerializeField] private Image _cdIcon;

        private KnightAI _knight;
        private float _maxHealth;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            if (_knight != null)
            {
                _knight.OnShieldUse += OnUsePerk;
                _knight = FindObjectOfType<KnightAI>().GetComponent<KnightAI>();
            }

            _maxHealth = _health.Health;

            _trash.Retain(_health._onChange.Subscribe(OnHealthChanged));
            _trash.Retain(_health._onDie.Subscribe(HideUI));
        }

        [ContextMenu("Show")]
        public void ShowUI()
        {
            this.LerpAnimated(0, 1, 1, SetAlpha);
        }

        private void SetAlpha(float alpha)
        {
            _canvas.alpha = alpha;

            if (_group != null)
                _group.alpha = alpha;
        }

        [ContextMenu("Hide")]
        public void HideUI()
        {
            this.LerpAnimated(1, 0, 1, SetAlpha);
        }

        private void OnHealthChanged(int hp)
        {
            _hpBar.SetProgress(hp / _maxHealth);
        }

        private void OnUsePerk(Cooldown cooldown)
        {
            StartCoroutine(CooldownTimer(cooldown));
        }

        private IEnumerator CooldownTimer(Cooldown cooldown)
        {
            var waitForAndOfFrame = new WaitForEndOfFrame();
            while (!cooldown.IsReady)
            {
                _cdIcon.fillAmount = cooldown.RemainingTime / cooldown.Value;
                yield return waitForAndOfFrame;
            }

            _cdIcon.fillAmount = 0;
        }

        private void OnDestroy()
        {
            if (_knight != null)
                _knight.OnShieldUse -= OnUsePerk;

            _trash.Dispose();
        }
    }
}