using PixelCrew.Model;
using PixelCrew.Model.Definitions.Repositories;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Shop
{
    public class ShopWidget : MonoBehaviour, IItemRenderer<ShopDef>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private GameObject _isSelected;

        private GameSession _session;
        private ShopDef _data;

        private void Start()
        {
            _session = GameSession.Instance;
            UpdateView();
        }

        public void SetData(ShopDef data, int index)
        {
            _data = data;

            if (_session != null)
                UpdateView();
        }

        private void UpdateView()
        {
            _icon.sprite = _data.Icon;
            _isSelected.SetActive(_session.ShopModel.InterfaceSelection.Value == _data.Id);
        }

        public void OnSelect()
        {
            _session.ShopModel.InterfaceSelection.Value = _data.Id;
        }
    }
}