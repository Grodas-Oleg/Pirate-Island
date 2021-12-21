using System.Collections;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using PixelCrew.Creatures.Mobs.Patrolling;
using UnityEngine;

namespace PixelCrew.Creatures.Mobs
{
    public class MobAI : MonoBehaviour
    {
        [SerializeField] protected ColliderCheck vision;
        [SerializeField] protected ColliderCheck canAttack;

        [SerializeField] private float alarmDelay = 0.5f;
        [SerializeField] private float attackCooldown = 1f;
        [SerializeField] private float missHeroCooldown = 0.5f;

        [SerializeField] public float _horizontalTrashold = 0.2f;

        private Coroutine _current;
        public GameObject _target;

        private static readonly int IsDeadKey = Animator.StringToHash("is-dead");

        private SpawnListComponent _particles;
        protected Creature Creature;
        protected Animator Animator;
        private bool _isDead;
        protected Patrol Patrol;


        protected virtual void Awake()
        {
            _particles = GetComponent<SpawnListComponent>();
            Creature = GetComponent<Creature>();
            Animator = GetComponent<Animator>();
            Patrol = GetComponent<Patrol>();
        }

        protected virtual void Start()
        {
            StartState(Patrol.DoPatrol());
        }

        public void OnHeroInVision(GameObject go)
        {
            if (_isDead) return;

            _target = go;

            StartState(AgroToHero());
        }

        private IEnumerator AgroToHero()
        {
            LookAtHero();
            _particles.Spawn("Exclamation");
            yield return new WaitForSeconds(alarmDelay);

            StartState(GoToHero());
        }

        private void LookAtHero()
        {
            var direction = GetDirectionToTarget();
            Creature.SetDirection(Vector2.zero);
            Creature.UpdateSpriteDirection(direction);
        }

        protected virtual IEnumerator GoToHero()
        {
            _particles.Spawn("MissHero");
            Creature.SetDirection(Vector2.zero);
            yield return new WaitForSeconds(missHeroCooldown);

            StartCoroutine(Patrol.DoPatrol());
        }

        protected IEnumerator Attack()
        {
            while (canAttack.IsTouchingLayer)
            {
                Creature.Attack();
                yield return new WaitForSeconds(attackCooldown);
            }

            StartCoroutine(GoToHero());
        }

        protected void SetDirectionToTarget()
        {
            var direction = GetDirectionToTarget();
            Creature.SetDirection(direction);
        }

        private Vector2 GetDirectionToTarget()
        {
            var direction = _target.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        protected void StartState(IEnumerator coroutine)
        {
            Creature.SetDirection(Vector2.zero);

            if (_current != null)
                StopCoroutine(_current);
            _current = StartCoroutine(coroutine);
        }

        public void OnDie()
        {
            _isDead = true;
            Animator.SetBool(IsDeadKey, true);

            Creature.SetDirection(Vector2.zero);
            if (_current != null)
                StopCoroutine(_current);
        }
    }
}