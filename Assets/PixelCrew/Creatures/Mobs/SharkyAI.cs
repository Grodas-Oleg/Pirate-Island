using System.Collections;
using PixelCrew.Components.ColliderBased;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class SharkyAI : MobAI
    {
        [SerializeField] private ColliderCheck _canDash;

        protected override IEnumerator GoToHero()
        {
            while (vision.IsTouchingLayer)
            {
                if (_canDash.IsTouchingLayer)
                {
                    StartState(Dash());
                }

                if (canAttack.IsTouchingLayer && !_canDash.IsTouchingLayer)
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


        private IEnumerator Dash()
        {
            while (_canDash.IsTouchingLayer)
            {
                Creature.Dash();
                yield return null;
            }

            StartCoroutine(GoToHero());
        }
    }
}