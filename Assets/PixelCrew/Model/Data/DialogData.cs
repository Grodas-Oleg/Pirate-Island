using System;
using PixelCrew.Model.Definitions.Repositories.Dialog;
using UnityEngine;

namespace PixelCrew.Model.Data
{
    [Serializable]
    public struct DialogData
    {
        [SerializeField] private Sentence[] _sentences;
        [SerializeField] private DialogType _type;
        public Sentence[] Sentences => _sentences;

        public DialogType Type => _type;
    }

    [Serializable]
    public struct Sentence
    {
        [AvatarId] [SerializeField] private string _icon;
        [SerializeField] private string _value;
        [SerializeField] private Side _side;

        public string Icon => _icon;
        public string Value => _value;
        public Side Side => _side;
    }

    public enum Side
    {
        Left,
        Right
    }

    public enum DialogType
    {
        Simple,
        Personalized
    }
}