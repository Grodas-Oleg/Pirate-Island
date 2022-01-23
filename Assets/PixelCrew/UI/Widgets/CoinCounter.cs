using PixelCrew.Model;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private Text _value;

        private int CoinsCount => GameSession.Instance.Data.Inventory.Count("Coin");
        private GameSession _session;

        private void Update()
        {
            Count();
        }

        private void Count()
        {
            _value.text = $"x{CoinsCount.ToString()}";
        }
    }
}