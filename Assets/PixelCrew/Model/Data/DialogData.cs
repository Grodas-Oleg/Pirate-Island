using System;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Repositories.Dialog;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public class DialogData
    {
        [AvatarId] [SerializeField] private string _id;
        [SerializeField] private string[] _sentences;
        public string[] Sentences => _sentences;
        public string Id => _id;
    }
}