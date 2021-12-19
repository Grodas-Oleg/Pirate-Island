using UnityEngine;

namespace PixelCrew.Utils
{
    public static class AudioUtils
    {
        public const string SfxSourceTag = "SFXAudioSource";
        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}