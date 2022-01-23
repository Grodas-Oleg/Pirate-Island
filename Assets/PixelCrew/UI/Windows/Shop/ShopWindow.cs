using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.Model.Definitions.Repositories;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Shop
{
    public class ShopWindow : AnimatedWindow
    {
        [SerializeField] private Button _buyButton;
        [SerializeField] private ItemWidget _price;
        [SerializeField] private Text _info;
        [SerializeField] private Transform _itemsContainer;

        private PredefinedDataGroup<ShopDef, ShopWidget> _dataGroup;
        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private GameSession _session;

        protected override void Start()
        {
            base.Start();

            _dataGroup = new PredefinedDataGroup<ShopDef, ShopWidget>(_itemsContainer);
            _session = GameSession.Instance;
            
            _trash.Retain(_session.ShopModel.Subscribe(OnItemChanged));
            _trash.Retain(_buyButton.onClick.Subscribe(OnBuy));

            OnItemChanged();
        }

        private void OnItemChanged()
        {
            _dataGroup.SetData(DefsFacade.I.Shop.All);

            var selected = _session.ShopModel.InterfaceSelection.Value;
            _buyButton.interactable = _session.ShopModel.CanBuy(selected);

            var def = DefsFacade.I.Shop.Get(selected);
            _price.SetData(def.Price);

            _info.text = LocalizationManager.I.Localize(def.Info);
        }

        private void OnBuy()
        {
            var selected = _session.ShopModel.InterfaceSelection.Value;
            _session.ShopModel.Buy(selected);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}