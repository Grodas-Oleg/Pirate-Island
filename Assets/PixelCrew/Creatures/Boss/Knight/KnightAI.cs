using System;
using System.Collections;
using PixelCrew.Components.Audio;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.Effects.CameraRelated;
using PixelCrew.Components.GoBased;
using PixelCrew.Components.Health;
using PixelCrew.Creatures.Hero.Features;
using PixelCrew.Utils;
using PixelCrew.Utils.Disposables;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PixelCrew.Creatures.Boss.Knight
{
    public class KnightAI : MonoBehaviour
    {
        [SerializeField] private AngleProjectileSpawner angleProjectiles;
        [SerializeField] private LayerCheck groundCheck;
        [SerializeField] private ShieldPerk shield;
        [SerializeField] private Cooldown shieldCooldown;
        [SerializeField] private KnightPunchComponent punchComponent;
        [SerializeField] private Transform[] points;
        [SerializeField] private int stageIndex;
        [SerializeField] private StageSequence[] stages;

        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private HealthComponent _health;
        private SpawnListComponent _spawner;
        private GameObject _hero;
        private CameraShakeEffect _cameraShake;
        private PlaySoundsComponent _sfx;
        private WaitForSeconds _waitAttack;
        private WaitForSeconds _waitAttackCoroutine;
        private Coroutine _current;

        private Vector2 _direction;
        private int _destinationPointIndex;
        private int _maxHealth;
        private float nextStateWait = 2;
        private bool _isGrounded;
        public event Action<Cooldown> OnShieldUse;

        private readonly float _trashHold = 0.5f;
        private static readonly int DoAttack = Animator.StringToHash("Attack");
        private static readonly int DoAttack2 = Animator.StringToHash("Attack 2");
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int IsRunning = Animator.StringToHash("Run");
        private static readonly int OnGround = Animator.StringToHash("OnGround");

        private readonly CompositeDisposable _trash = new CompositeDisposable();

        private void Start()
        {
            _maxHealth = _health.Health;
            _trash.Retain(_health._onChange.Subscribe(OnHealthChanged));
            OnHealthChanged(_health.Health);
            _waitAttack = new WaitForSeconds(stages[stageIndex].AttackCd);
            _waitAttackCoroutine =
                new WaitForSeconds(stages[stageIndex].AttackCd * stages[stageIndex].AttacksNumber + nextStateWait);
        }

        private void Awake()
        {
            _sfx = GetComponent<PlaySoundsComponent>();
            _spawner = GetComponent<SpawnListComponent>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<HealthComponent>();
            angleProjectiles = GetComponent<AngleProjectileSpawner>();
            _cameraShake = FindObjectOfType<CameraShakeEffect>();
            _hero = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            _isGrounded = groundCheck.IsTouchingLayer;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_direction.x * stages[stageIndex].Speed, _direction.y);
            _animator.SetBool(IsRunning, _direction.x != 0);
            _animator.SetBool(OnGround, _isGrounded);
        }

        public void StartFight()
        {
            StartCoroutine(GoToNextPoint());
        }

        private void StartState(IEnumerator coroutine)
        {
            SetDirection(Vector2.zero);

            if (_current != null)
                StopCoroutine(_current);
            _current = StartCoroutine(coroutine);
        }

        private IEnumerator Attack()
        {
            for (int i = 0; i < stages[stageIndex].AttacksNumber; i++)
            {
                ChoseAttack();
                yield return _waitAttack;
            }

            yield return new WaitForSeconds(nextStateWait);

            StartState(GoToNextPoint());
        }

        private IEnumerator GoToNextPoint()
        {
            while (enabled)
            {
                if (stageIndex == 2)
                {
                    UseShield();
                }

                if (!IsOnPoint())
                {
                    punchComponent.gameObject.SetActive(true);
                    var direction = points[_destinationPointIndex].position - transform.position;
                    direction.y = 0;
                    SetDirection(direction.normalized);
                    UpdateSpriteDirection(direction.normalized);
                    yield return new WaitForEndOfFrame();
                    punchComponent.gameObject.SetActive(false);
                }
                else
                {
                    UpdateSpriteDirection(LookAtHero());
                    _rigidbody.velocity = Vector2.zero;
                    StartState(Attack());
                    _destinationPointIndex = (int) Mathf.Repeat(_destinationPointIndex + 1, points.Length);
                    yield return _waitAttackCoroutine;
                }
            }
        }

        private void OnHealthChanged(int health)
        {
            var healthToPercent = ((float) health / _maxHealth) * 100;
            if (healthToPercent >= 66) return;
            stageIndex = 1;
            if (healthToPercent >= 33) return;
            angleProjectiles.Stage = 1;
            stageIndex = 2;
        }

        private void SetDirection(Vector2 direction)
        {
            _direction = direction;
        }

        private Vector2 LookAtHero()
        {
            var direction = _hero.transform.position - transform.position;
            direction.y = 0;
            return direction.normalized;
        }

        private void UpdateSpriteDirection(Vector2 direction)
        {
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        private bool IsOnPoint()
        {
            return (points[_destinationPointIndex].position - transform.position).magnitude < _trashHold;
        }

        private void ChoseAttack()
        {
            _cameraShake?.SetShake(.5f, 3);
            if (stageIndex >= 1)
            {
                var value = Random.Range(0, 2);
                _animator.SetTrigger(value == 1 ? DoAttack : DoAttack2);
            }
            else
            {
                _animator.SetTrigger(DoAttack);
            }
        }

        public void SpawnSlash()
        {
            _spawner.Spawn(stages[stageIndex].SlashType);
        }

        private void SpawnDaggers()
        {
            angleProjectiles.LaunchProjectiles();
        }

        private void UseShield()
        {
            if (!shieldCooldown.IsReady) return;
            _sfx.Play("Shield");
            shield.Use();
            shieldCooldown.Reset();
            OnShieldUse?.Invoke(shieldCooldown);
        }

        public void OnHit()
        {
            _animator.SetTrigger(Hit);
        }

        public void OnDie()
        {
            _cameraShake?.SetShake(3, 15);
            SetDirection(Vector2.zero);
            _current = null;
            _animator.SetTrigger(Die);
        }
    }

    [Serializable]
    public struct StageSequence
    {
        [SerializeField] private float speed;
        [SerializeField] private float attackCd;
        [SerializeField] private int attacksNumber;
        [SerializeField] private string slashType;
        public float Speed => speed;
        public float AttackCd => attackCd;
        public int AttacksNumber => attacksNumber;
        public string SlashType => slashType;
    }
}