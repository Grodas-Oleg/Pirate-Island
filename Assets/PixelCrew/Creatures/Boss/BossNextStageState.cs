using PixelCrew.Components.GoBased;
using UnityEngine;

namespace PixelCrew.Creatures.Boss
{
    public class BossNextStageState : StateMachineBehaviour
    {
        [ColorUsage(true,true)]
        [SerializeField] private Color _stageColor;
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            var spawner = animator.GetComponent<CircularProjectileSpawner>();
            spawner.Stage++;

            var changeLight = animator.GetComponent<ChangeLightsComponent>();
            changeLight.SetColor();
        }
    }
}