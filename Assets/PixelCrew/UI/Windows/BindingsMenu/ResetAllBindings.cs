using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.UI.Windows.BindingsMenu
{
    public class ResetAllBindings : MonoBehaviour
    {
        [SerializeField] private InputActionAsset inputActions;

        public void ResetBindingsAll()
        {
            foreach (InputActionMap map in inputActions.actionMaps)
            {
                map.RemoveAllBindingOverrides();
            }

            // PlayerPrefs.DeleteKey("rebinds");
        }
    }
}