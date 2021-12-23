using PixelCrew.Model.Definitions.Repositories;
using PixelCrew.Model.Definitions.Repositories.Dialog;
using PixelCrew.Model.Definitions.Repositories.Items;
using UnityEngine;

namespace PixelCrew.Model.Definitions
{
    [CreateAssetMenu(menuName = "Defs/DefsFacade", fileName = "DefsFacade")]
    public class DefsFacade : ScriptableObject
    {
        [SerializeField] private ItemsRepository _items;
        [SerializeField] private PotionRepository _potions;
        [SerializeField] private ThrowableRepository _throwableItems;
        [SerializeField] private PlayerDef _player;
        [SerializeField] private DialogAvatarRepository _avatar;

        public ItemsRepository Items => _items;

        public PotionRepository Potions => _potions;

        public ThrowableRepository ThrowableItems => _throwableItems;
        public PlayerDef Player => _player;
        public DialogAvatarRepository Avatar => _avatar;

        private static DefsFacade _instance;
        public static DefsFacade I => _instance == null ? LoadDefs() : _instance;

        private static DefsFacade LoadDefs()
        {
            return _instance = Resources.Load<DefsFacade>("DefsFacade");
        }
    }
}