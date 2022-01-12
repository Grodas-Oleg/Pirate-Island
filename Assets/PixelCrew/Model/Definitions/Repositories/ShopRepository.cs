using System;
using PixelCrew.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Repositories
{
    [CreateAssetMenu(menuName = "Defs/ShopItems", fileName = "ShopItems")]
    public class ShopRepository : DefRepository<ShopDef>
    {
    }

    [Serializable]
    public struct ShopDef : IHaveId
    {
        [InventoryId] [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _info;
        [SerializeField] private ItemWithCount _price;
        public string Id => _id;
        public Sprite Icon => _icon;
        public string Info => _info;
        public ItemWithCount Price => _price;
    }
}