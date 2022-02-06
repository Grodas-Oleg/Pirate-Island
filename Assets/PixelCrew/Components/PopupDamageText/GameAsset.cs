using UnityEngine;

namespace PixelCrew.Components.PopupDamageText
{
    public class GameAsset : MonoBehaviour
    {
        private static GameAsset _i;

        public static GameAsset I
        {
            get
            {
                if (_i == null) _i = Instantiate(Resources.Load<GameAsset>("UI/GameAsset"));
                return _i;
            }
        }

        public Transform damagePopup;
    }
}