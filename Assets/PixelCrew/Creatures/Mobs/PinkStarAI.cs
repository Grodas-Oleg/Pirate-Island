using System.Collections;

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