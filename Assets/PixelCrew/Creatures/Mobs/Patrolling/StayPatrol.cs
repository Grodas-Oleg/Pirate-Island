using System.Collections;

namespace PixelCrew.Creatures.Mobs.Patrolling
{
    public class StayPatrol : Patrol
    {
        public override IEnumerator DoPatrol()
        {
            yield return null;
        }
    }
}