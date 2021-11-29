using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.Components
{
    public class CoinCounter : MonoBehaviour
    {
        Text _counterText;
        public static int totalCoins;

        void Start()
        {
            _counterText = GetComponent<Text>();
        }

        void Update()
        {
            _counterText.text = totalCoins.ToString();
        }
    }
}

