using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class AttackManager : MonoBehaviour
    {
        private Animator _animator;
        public int CountAttack;

        private static readonly int AttackType = Animator.StringToHash("attack-type");

        private void Start()
        {
            _animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            CountAttack = 0;
        }

        public void DoAttack()
        {
            CountAttack++;
            if (CountAttack == 1)
            {
                _animator.SetInteger(AttackType, 1);
            }

            if (CountAttack >= 5)
            {
                ResetAttack();
            }
        }

        public void CheckAttackCombo()
        {
            var state = _animator.GetCurrentAnimatorStateInfo(0);
            if (state.IsName("attack 1"))
            {
                if (CountAttack > 1)
                {
                    _animator.SetInteger(AttackType, 2);
                }
                else
                {
                    ResetAttack();
                }
            }
            else if (state.IsName("attack 2"))
            {
                if (CountAttack > 2)
                {
                    _animator.SetInteger(AttackType, 3);
                }
                else
                {
                    ResetAttack();
                }
            }
            else if (state.IsName("attack 3"))
            {
                if (CountAttack >= 3)
                {
                    ResetAttack();
                }
            }
        }

        private void ResetAttack()
        {
            CountAttack = 0;
            _animator.SetInteger(AttackType, 0);
        }
    }
}