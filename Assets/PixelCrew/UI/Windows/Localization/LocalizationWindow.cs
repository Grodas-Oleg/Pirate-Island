using System.Collections.Generic;
using PixelCrew.Model.Definitions.Localization;
using PixelCrew.UI.Widgets;
using UnityEngine;

namespace PixelCrew.UI.Windows.Localization
{
    public class LocalizationWindow : AnimatedWindow
    {
        [SerializeField] private Transform _container;
        [SerializeField] private LocaleItemWidget _prefab;

        private DataGroup<LocalInfo, LocaleItemWidget> _dataGroup;

        private readonly string[] _supportedLocales = {"en", "ru"};

        protected override void Start()
        {
            base.Start();
            _dataGroup = new DataGroup<LocalInfo, LocaleItemWidget>(_prefab, _container);
            _dataGroup.SetData(ComposeData());
        }

        private List<LocalInfo> ComposeData()
        {
            var data = new List<LocalInfo>();
            foreach (var locale in _supportedLocales)
            {
                data.Add(new LocalInfo {LocaleId = locale});
            }

            return data;
        }

        public void OnSelected(string selectedLocale)
        {
            LocalizationManager.I.SetLocale(selectedLocale);
        }
    }
}