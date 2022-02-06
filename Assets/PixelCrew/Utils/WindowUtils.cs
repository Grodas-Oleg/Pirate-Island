using UnityEngine;
using UnityEngine.EventSystems;

namespace PixelCrew.Utils
{
    public static class WindowUtils
    {
        public static void CreateWindow(string resourcePath)
        {
            var window = Resources.Load<GameObject>(resourcePath);
            var canvas = GameObject.FindWithTag("HudCanvas").GetComponent<Canvas>();

            Object.Instantiate(window, canvas.transform);

            var buttonPause = GameObject.FindWithTag("PauseButton");
            var button = GameObject.FindWithTag("DefaultButton");
            if (button != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(button);
            }
            else if (buttonPause != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(buttonPause);
            }
        }
    }
}