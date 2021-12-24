using System;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.UI.Widgets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace PixelCrew.UI.Windows.Localization
{
    public class LocaleItemWidget : MonoBehaviour, IItemRenderer<LocalInfo>
    {
        [SerializeField] private Text _text;
        [SerializeField] private GameObject _selector;
        [SerializeField] private SelectLocale _onSelected;

        private LocalInfo _data;

        private void Start()
        {
            LocalizationManager.I.OnLocalChanged += UpdateSelection;
        }

        public void SetData(LocalInfo localInfo, int index)
        {
            _data = localInfo;
            UpdateSelection();
            _text.text = localInfo.LocaleId.ToUpper();
        }

        private void UpdateSelection()
        {
            var selected = LocalizationManager.I.LocaleKey == _data.LocaleId;
            _selector.SetActive(selected);
        }

        public void OnSelected()
        {
            _onSelected?.Invoke(_data.LocaleId);
        }

        private void OnDestroy()
        {
            LocalizationManager.I.OnLocalChanged -= UpdateSelection;

        }
    }

    [Serializable]
    public class SelectLocale : UnityEvent<string>
    {
    }

    public class LocalInfo
    {
        public string LocaleId;
    }
}