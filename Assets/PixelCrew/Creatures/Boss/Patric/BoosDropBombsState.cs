using PixelCrew.Components.GoBased;
using UnityEngine;

namespace PixelCrew.Creatures.Boss.Patric
{
    public class BoosDropBombsState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var bombSpawner = animator.GetComponent<BossBombsSpawnComponent>();
            bombSpawner.DropBombs();
        }
    }
}
