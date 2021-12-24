using UnityEngine;

namespace PixelCrew.Model.Data.Properties
{
    public class StringPersistantProperty : PrefsPersistentProperty<string>
    {
        public StringPersistantProperty(string defaultValue, string key) : base(defaultValue, key)
        {
            Init();
        }

        protected override void Write(string value)
        {
            PlayerPrefs.SetString(Key, value);
        }

        protected override string Read(string defaultValue)
        {
            return PlayerPrefs.GetString(Key, defaultValue);
        }
    }
}