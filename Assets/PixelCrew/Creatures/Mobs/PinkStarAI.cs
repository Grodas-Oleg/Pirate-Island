using System.Collections;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class PinkStarAI : MobAI
    {
        protected override IEnumerator GoToHero()
        {
            while (vision.IsTouchingLayer)
            {
                if (canAttack.IsTouchingLayer)
                {
                    StartState(Attack());
                }
                else
                {
                    if (vision.IsTouchingLayer)
                    {
                        var horizontalDelta = Mathf.Abs(_target.transform.position.x - transform.position.x);
                        if (horizontalDelta <= _horizontalTrashold)
                            Creature.SetDirection(Vector2.zero);
                        else
                            SetDirectionToTarget();
                    }
                    else
                    {
                        StartCoroutine(Patrol.DoPatrol());
                    }
                }

                yield return null;
            }

            yield return base.GoToHero();
        }
    }
}