using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class FloodState : StateMachineBehaviour
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var flood = animator.GetComponent<FloodController>();
            flood.StartFlooding();
        }
    }
}