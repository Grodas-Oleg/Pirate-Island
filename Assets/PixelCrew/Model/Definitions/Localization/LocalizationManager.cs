using System;
using System.Collections.Generic;
using PixelCrew.Model.Data.Properties;
using UnityEngine;

namespace PixelCrew.Model.Definitions.Localization
{
    public class LocalizationManager
    {
        public static readonly LocalizationManager I;
        private readonly StringPersistantProperty _keyLocale = new StringPersistantProperty("en", "localization/current");
        private Dictionary<string, string> _localization;
        public string LocaleKey => _keyLocale.Value;
        
        public event Action OnLocalChanged;

        static LocalizationManager()
        {
            I = new LocalizationManager();
        }

        private LocalizationManager()
        {
            LoadLocale(_keyLocale.Value);
        }

        private void LoadLocale(string localeToLoad)
        {
            var def = Resources.Load<LocaleDef>($"Locales/{localeToLoad}");
            _keyLocale.Value = localeToLoad;
            _localization = def.GetData();
            OnLocalChanged?.Invoke();
        }

        public string Localize(string key)
        {
            return _localization.TryGetValue(key, out var value) ? value : $"%%%{key}%%%";
        }

        public void SetLocale(string localKey)
        {
            LoadLocale(localKey);
        }
    }
}