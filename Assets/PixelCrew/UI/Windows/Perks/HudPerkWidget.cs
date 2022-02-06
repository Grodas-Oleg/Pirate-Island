using System.Collections;
using PixelCrew.Creatures.Hero;
using PixelCrew.Model;
using PixelCrew.Model.Models;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Perks
{
    public class HudPerkWidget : MonoBehaviour
    {
        [SerializeField] private string _perkId;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _cdIcon;
        [SerializeField] private GameObject _container;

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;
        private Hero _hero;

        private void Start()
        {
            _session = GameSession.Instance;
            var sprite = PerksModel.PerkSprite(_perkId);
            _hero = GameObject.FindWithTag("Player").GetComponent<Hero>();
            _icon.sprite = sprite;

            _trash.Retain(_session.PerksModel.Subscribe(OnPerkUnlocked));
            _hero.OnPerkUsed += OnUsePerk;

            OnPerkUnlocked();
        }

        private void OnUsePerk(string perkId, Cooldown cooldown)
        {
            if (!string.Equals(perkId, _perkId)) return;

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

        private void OnPerkUnlocked()
        {
            _container.SetActive(_session.Data.Perks.IsUnlocked(_perkId));
        }

        private void OnDestroy()
        {
            _trash.Dispose();
            _hero.OnPerkUsed -= OnUsePerk;
        }
    }
}