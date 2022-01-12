using System;
using PixelCrew.Model.Data;
using PixelCrew.Utils.Disposables;

namespace PixelCrew.Model.Models
{
    public class BigInventoryModel : IDisposable
    {
        private readonly PlayerData _data;
        public InventoryItemData[] Inventory { get; private set; }
        public event Action OnChanged;

        public BigInventoryModel(PlayerData data)
        {
            _data = data;
            Inventory = _data.Inventory.GetAll();
            _data.Inventory.OnChanged += OnChangedInventory;
        }

        public IDisposable Subscribe(Action call)
        {
            OnChanged += call;
            return new ActionDisposable(() => OnChanged -= call);
        }

        private void OnChangedInventory(string id, int value)
        {
            Inventory = _data.Inventory.GetAll();
            OnChanged?.Invoke();
        }

        public void Dispose()
        {
            _data.Inventory.OnChanged -= OnChangedInventory;
        }
    }
}