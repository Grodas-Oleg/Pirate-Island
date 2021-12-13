using UnityEngine;

namespace PixelCrew.Utils
{
    public class AudioUtils
    {
        public const string SFXSourceTag = "SFXAudioSource";
        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SFXSourceTag).GetComponent<AudioSource>();
        }
    }
}