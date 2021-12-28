using System;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.HUD.Dialogs
{
    public class OptionDialogController : MonoBehaviour
    {
        [SerializeField] private GameObject _content;
        [SerializeField] private Text _contentText;
        [SerializeField] private Transform _optionsContainer;
        [SerializeField] private OptionItemWidget _prefab;

        private DataGroup<OptionData, OptionItemWidget> _dataGroup;

        private void Start()
        {
            _dataGroup = new DataGroup<OptionData, OptionItemWidget>(_prefab, _optionsContainer);
        }

        public void OnOptionsSelected(OptionData selectedOption)
        {
            selectedOption.OnSelect.Invoke();
            _content.SetActive(false);
        }

        public void Show(OptionDialogData data)
        {
            _content.SetActive(true);
            _contentText.text = data.DialogText;

            _dataGroup.SetData(data.Options);
        }
    }

    [Serializable]
    public class OptionDialogData
    {
        public string _dialogText;
        public OptionData[] Options;

        public string DialogText => LocalizationManager.I.Localize(_dialogText);
    }

    [Serializable]
    public class OptionData
    {
        public string _text;
        public UnityEvent OnSelect;
        
        public string Text => LocalizationManager.I.Localize(_text);
    }
}