using System.Collections;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class CrabbyAI : MobAI
    {
        // [SerializeField] private ColliderCheck backAttack;
        [SerializeField] private ColliderCheck rangeAttack;

        [SerializeField] private SpawnComponent _rangeParticle;
        // [SerializeField] private float _rangeAttackDuration;

        private static readonly int IsRange = Animator.StringToHash("throw");


        [SerializeField] private float rangeAttackCooldown = 3f;


        protected override IEnumerator GoToHero()
        {
            while (vision.IsTouchingLayer)
            {
                if (rangeAttack.IsTouchingLayer)
                {
                    StartState(RangeAttack());
                }
                // else if (canAttack.IsTouchingLayer || backAttack.IsTouchingLayer)
                else if (canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    var horizontalDelta = Mathf.Abs(_target.transform.position.x - transform.position.x);
                    if (horizontalDelta <= _horizontalTrashold)
                        Creature.SetDirection(Vector2.zero);
                    else
                        SetDirectionToTarget();
                }

                yield return null;
            }

            yield return base.GoToHero();
            StartCoroutine(Patrol.DoPatrol());
        }

        private IEnumerator RangeAttack()
        {
            Animator.SetBool(IsRange, true);
            Creature.SetDirection(Vector2.zero);
            yield return new WaitForSeconds(rangeAttackCooldown);
        }

        private void DoRangeAttack()
        {
            _rangeParticle.Spawn();
        }
    }
}