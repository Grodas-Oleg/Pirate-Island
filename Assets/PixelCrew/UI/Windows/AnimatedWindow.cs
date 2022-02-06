using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;

namespace PixelCrew.UI.Windows
{
    public class AnimatedWindow : MonoBehaviour
    {
        private Animator _animator;
        private static readonly int Show = Animator.StringToHash("Show");
        private static readonly int Hide = Animator.StringToHash("Hide");

        protected virtual void Start()
        {
            AnalyticsEvent.ScreenVisit(gameObject.name);

            _animator = GetComponent<Animator>();

            _animator.SetTrigger(Show);
        }

        public void Close()
        {
            _animator.SetTrigger(Hide);
        }

        protected virtual void OnCloseAnimationComplete()
        {
            Destroy(gameObject);

            var button = GameObject.FindWithTag("StartButton");
            var buttonPause = GameObject.FindWithTag("PauseButton");

            if (buttonPause != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(buttonPause);
            }
            else if (button != null)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(button);
            }
        }
    }
}