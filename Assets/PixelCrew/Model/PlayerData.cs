using System;
using UnityEngine;

namespace PixelCrew.Model
{
    [Serializable]
    public class PlayerData
    {
        public int Coin;
        public int HP;
        public bool IsArmed;
        public float ThrowSwordQuantity;

        public PlayerData Clone()
        {
            var json = JsonUtility.ToJson(this);
            return JsonUtility.FromJson<PlayerData>(json);
        }
    } 
}