using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.UI.Windows.BindingsMenu
{
    public class RebindingSaveLoad : MonoBehaviour
    {
        [SerializeField] private InputActionAsset actions;

        public void OnEnable()
        {
            var rebinds = PlayerPrefs.GetString("rebinds");
            if (!string.IsNullOrEmpty(rebinds))
                actions.LoadFromJson(rebinds);
        }

        public void OnDisable()
        {
            var rebinds = actions.ToJson();
            PlayerPrefs.SetString("rebinds", rebinds);
        }
    }
}