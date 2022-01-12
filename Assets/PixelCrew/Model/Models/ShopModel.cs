using System;
using PixelCrew.Model.Data;
using PixelCrew.Model.Data.Properties;
using PixelCrew.Model.Definitions;
using PixelCrew.Utils.Disposables;

namespace PixelCrew.Model.Models
{
    public class ShopModel : IDisposable
    {
        private readonly PlayerData _data;
        public readonly StringProperty InterfaceSelection = new StringProperty();

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        public event Action OnChanged;

        public ShopModel(PlayerData data)
        {
            _data = data;
            InterfaceSelection.Value = DefsFacade.I.Shop.All[0].Id;

            _trash.Retain(InterfaceSelection.Subscribe((x, y) => OnChanged?.Invoke()));
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        public void Buy(string itemId)
        {
            var def = DefsFacade.I.Shop.Get(itemId);
            var isEnoughResources = _data.Inventory.IsEnough(def.Price);

            if (!isEnoughResources) return;
            _data.Inventory.Remove(def.Price.ItemId, def.Price.Count);
            AddItem(itemId);

            OnChanged?.Invoke();
        }

        private void AddItem(string itemId)
        {
            _data.Inventory.Add(itemId, 1);
        }

        public bool CanBuy(string itemId)
        {
            var def = DefsFacade.I.Shop.Get(itemId);
            return _data.Inventory.IsEnough(def.Price);
        }

        public void Dispose()
        {
            _trash.Dispose();
        }
    }
}