using System;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Repositories.Dialog
{
    [CreateAssetMenu(menuName = "Defs/DialogAvatars", fileName = "DialogAvatars")]
    public class DialogAvatarRepository : DefRepository<AvatarDef>
    {
#if UNITY_EDITOR
        public AvatarDef[] AvatarForEditor => _collection;
#endif
    }

    [Serializable]
    public struct AvatarDef : IHaveId
    {
        [SerializeField] private string _id;
        [SerializeField] private Sprite _icon;
        public string Id => _id;
        public Sprite Icon => _icon;
    }
}