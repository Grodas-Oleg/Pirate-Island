using PixelCrew.Model;
using PixelCrew.Model.Data;
using PixelCrew.UI.Widgets;
using PixelCrew.Utils.Disposables;
using UnityEngine;

namespace PixelCrew.UI.Windows.BigInventory
{
    public class BigInventoryWindow : AnimatedWindow
    {
        [SerializeField] private Transform _container;
        [SerializeField] private BigInventoryItemWidget _prefab;

        private readonly CompositeDisposable _trash = new CompositeDisposable();
        private GameSession _session;

        private DataGroup<InventoryItemData, BigInventoryItemWidget> _dataGroup;

        protected override void Start()
        {
            base.Start();
            _dataGroup = new DataGroup<InventoryItemData, BigInventoryItemWidget>(_prefab, _container);
            _session = GameSession.Instance;
            _trash.Retain(_session.BigInventory.Subscribe(Rebuild));
            Rebuild();
        }

        private void Rebuild()
        {
            var inventory = _session.BigInventory.Inventory;
            _dataGroup.SetData(inventory);
        }

        private void OnDestroy()
        {
            _trash.Dispose();
        }
    }
}