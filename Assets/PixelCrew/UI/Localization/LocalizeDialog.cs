using PixelCrew.Components.Dialogs;
using PixelCrew.Model.Definitions.Localization;
using UnityEngine;

namespace PixelCrew.UI.Localization
{
    public class LocalizeDialog : AbstractLocalizeComponent
    {
        [SerializeField] private string[] _keys;
        private ShowDialogComponent _sentences;
        protected override void Awake()
        {
            _sentences = gameObject.GetComponent<ShowDialogComponent>();
            base.Awake();
        }

        protected override void Localize()
        {
            var sentences =_sentences.Data.Sentences;
            foreach (var key in _keys)
            {
                var localize = LocalizationManager.I.Localize(key);
            }
        }
    }
}