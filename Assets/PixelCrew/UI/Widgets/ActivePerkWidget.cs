using PixelCrew.Model;
using PixelCrew.Model.Definitions.Repositories;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class ActivePerkWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _cdIcon;
        private GameSession _session;

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
        }

        private void Update()
        {
            var cooldown = _session.PerksModel.Cooldown;
            _cdIcon.fillAmount = cooldown.RemainingTime / cooldown.Value;
        }

        public void Set(PerkDef perkDef)
        {
            _icon.sprite = perkDef.Icon;
            // _cdIcon.sprite = perkDef.Icon;
        }
    }
}