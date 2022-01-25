using PixelCrew.Model;
using UnityEngine;
using UnityEngine.UI;

namespace PixelCrew.UI.Widgets
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] private Text _value;

        private void Awake()
        {
            _value.text = $"x{GameSession.Instance.Data.Inventory.Count("Coin")}";
            GameSession.Instance.Data.Inventory.OnChanged += Count;
        }

        private void Count(string id, int value)
        {
            if (!string.Equals(id, "Coin")) return;

            _value.text = $"x{value}";
        }
    }
}