using UnityEngine;

namespace PixelCrew.Components.GoBased
{
    public class DisableObjectComponent : MonoBehaviour
    {
        [SerializeField] private float _disableTime;

        private void OnEnable()
        {
            CancelInvoke();
            Invoke(nameof(Disable), _disableTime);
        }

        private void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}