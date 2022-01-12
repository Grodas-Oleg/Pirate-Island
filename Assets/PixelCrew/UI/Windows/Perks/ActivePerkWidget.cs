using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Models;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Perks
{
    public class ActivePerkWidget : MonoBehaviour
    {
        [SerializeField] private string _perkId;
        [SerializeField] private Image _icon;
        [SerializeField] private Image _cdIcon;
        [SerializeField] private GameObject _container;

        private readonly Cooldown _cooldown = new Cooldown();
        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var sprite = PerksModel.PerkSprite(_perkId);
            _icon.sprite = sprite;
            
            _trash.Retain(_session.PerksModel.Subscribe(OnPerkUnlocked));
            OnPerkUnlocked();
        }

        private void Update()
        {
            // var cooldown = _session.PerksModel.Cooldown;
            var cooldown = _session.PerksModel.UsePerk(_perkId);
            _cooldown.Value = cooldown;
            _cdIcon.fillAmount = _cooldown.RemainingTime / cooldown;
        }

        private void OnPerkUnlocked()
        {
            _container.SetActive(_session.Data.Perks.IsUnlocked(_perkId));
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}