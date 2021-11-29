using PixelCrew.Creatures;
using UnityEngine;

namespace PixelCrew.Components
{
    public class ProjectilePickUpComponent : MonoBehaviour
    {
        public void ProjectilePickUp(GameObject go)
        {
            var hero = go.GetComponent<Hero>();
            if (hero != null)
            {
                hero.ProjectilePickUp();
            }
        }
    }
}