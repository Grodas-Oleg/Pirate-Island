using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PixelCrew.UI.Pause
{
    public class PauseWindow : AnimatedWindow
    {
        public static bool _paused = false;
        public GameObject _pauseMenu;

        private void Update()
        {
            if (Keyboard.current[Key.Escape].isPressed)
            {
                if (_paused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void Resume()
        {
            _pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            _paused = false;
        }

        private void Pause()
        {
            _pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            _paused = true;
        }
    }
}