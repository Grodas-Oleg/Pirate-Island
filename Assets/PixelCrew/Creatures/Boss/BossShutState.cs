using PixelCrew.Components.GoBased;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossShutState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<CircularProjectileSpawner>();
            spawner.LaunchProjectiles();
        }
    }
}
