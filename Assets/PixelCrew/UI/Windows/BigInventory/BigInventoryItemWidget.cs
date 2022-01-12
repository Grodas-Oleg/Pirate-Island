using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repositories.Items;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.BigInventory
{
    public class BigInventoryItemWidget : MonoBehaviour, IItemRenderer<InventoryItemData>
    {
        [SerializeField] private Image _icon;
        [SerializeField] private Image _panel;
        [SerializeField] private Text _value;
        public void SetData(InventoryItemData item, int index)
        {
            var def = DefsFacade.I.Items.Get(item.Id);
            _icon.sprite = def.Icon;
            _value.text = def.HasTag(ItemTag.Stackable) ? $"x{item.Value.ToString()}" : string.Empty;
        }
    }
}