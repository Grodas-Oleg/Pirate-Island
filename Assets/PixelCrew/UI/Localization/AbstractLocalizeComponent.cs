using PixelCrew.Model.Definitions.Localization;
using UnityEngine;

namespace PixelCrew.UI.Localization
{
    public abstract class AbstractLocalizeComponent : MonoBehaviour
    {
        protected virtual void Awake()
        {
            LocalizationManager.I.OnLocalChanged += OnLocalChanged;
            Localize();
        }

        private void OnLocalChanged()
        {
            Localize();
        }

        protected abstract void Localize();
        

        private void OnDestroy()
        {
            LocalizationManager.I.OnLocalChanged -= OnLocalChanged;
        }
    }
}