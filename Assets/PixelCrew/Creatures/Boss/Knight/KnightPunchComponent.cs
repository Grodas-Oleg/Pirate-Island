using UnityEngine;

namespace PixelCrew.Creatures.Boss.Knight
{
    public class KnightPunchComponent : MonoBehaviour
    {
        [SerializeField] private float punchPower;

        public void Punch(GameObject target)
        {
            var rigidbody = target.GetComponent<Rigidbody2D>();

            rigidbody.AddForce(new Vector2(0, punchPower), ForceMode2D.Impulse);
        }
    }
}