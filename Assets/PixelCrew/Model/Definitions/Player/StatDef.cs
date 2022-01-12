using System;
using PixelCrew.Model.Definitions.Repositories;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Player
{
    [Serializable]
    public struct StatDef
    {
        [SerializeField] private string _name;
        [SerializeField] private StatId _id;
        [SerializeField] private Sprite _icon;
        [SerializeField] private StatLevelDef[] _levels;

        public StatId ID => _id;
        public string Name => _name;
        public Sprite Icon => _icon;
        public StatLevelDef[] Levels => _levels;
    }

    [Serializable]
    public struct StatLevelDef
    {
        [SerializeField] private float _value;
        [SerializeField] private ItemWithCount price;

        public float Value => _value;
        public ItemWithCount Price => price;
    }

    public enum StatId
    {
        Hp,
        Speed,
        RangeDamage,
        CriticalChance,
        CriticalDamage,
        Damage,
        CanonDamage
    }
}