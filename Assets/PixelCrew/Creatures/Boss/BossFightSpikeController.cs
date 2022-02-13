using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossFightSpikeController : MonoBehaviour
    {
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ActiveSpike()
        {
            _animator.Play("Active");
        }

        public void StopSpikeAnimation()
        {
            _animator.Play("Idle");
        }
    }
}