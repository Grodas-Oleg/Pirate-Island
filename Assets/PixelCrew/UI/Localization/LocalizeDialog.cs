using PixelCrew.Model.Data;
using PixelCrew.Model.Definitions.Localization;
using UnityEngine;

namespace PixelCrew.UI.Localization
{
    public class LocalizeDialog : AbstractLocalizeComponent
    {
        [SerializeField] private string _key;
        private Sentence[] _sentences;
        protected override void Awake()
        {
            _sentences = gameObject.GetComponent<Sentence[]>();
            base.Awake();
        }

        protected override void Localize()
        {
            var localized = LocalizationManager.I.Localize(_key);

            foreach (var sentence in _sentences)
            {
                // sentence.Value = localized;
            }
        }
    }
}