using System.Collections;
using PixelCrew.Components;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Utils;
using UnityEditor.Animations;
using UnityEngine;

namespace PixelCrew.Creatures.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private ColliderCheck _wallCheck;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        [Header("Super Throw")] [SerializeField]
        private Cooldown _superThrowCooldown;

        [SerializeField] private int _superThrowParticles;
        [SerializeField] private float _superThrowDelay;

        [SerializeField] private ProbabilityDropComponent _hitDrop;

        private bool _isOnWall;
        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");
        private static readonly int IsHeal = Animator.StringToHash("hit");

        private bool _allowDoubleJump;
        private float _defaultGravityScale;
        private bool _superThrow;

        private int CoinsCount => _session.Data.Inventory.Count("Coin");
        private int SwordsCount => _session.Data.Inventory.Count("Sword");
        private int PotionsCount => _session.Data.Inventory.Count("Potion");

        private GameSession _session;
        private HealthComponent _health;

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rigidbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _health = GetComponent<HealthComponent>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            _health.SetHealth(_session.Data.HP);
            UpdateHeroWeapon();
        }

        private void OnDestroy()
        {
            _session.Data.Inventory.OnChanged -= OnInventoryChanged;
        }

        private void OnInventoryChanged(string id, int value)
        {
            if (id == "Sword")
                UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.HP = currentHealth;
        }

        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;
            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }

            if (_isDashing || _isOnWall)
            {
                Rigidbody.gravityScale = 0;
            }
            else
            {
                Rigidbody.gravityScale = _defaultGravityScale;
            }

            Animator.SetBool(IsOnWall, _isOnWall);
        }

        protected override float CalculateYVelocity()
        {
            var _isJumpPressing = Direction.y > 0;
            if (IsGrounded || _isOnWall) _allowDoubleJump = true;
            if (_isOnWall && !_isJumpPressing) return 0f;
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVeloсity)
        {
            if (!IsGrounded && _allowDoubleJump)
            {
                _allowDoubleJump = false;
                DoJumpVFX();
                return _jumpSpeed;
            }

            return base.CalculateJumpVelocity(yVeloсity);
        }

        public void AddInInventory(string id, int value)
        {
            _session.Data.Inventory.Add(id, value);
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (CoinsCount > 0)
                SpawnCoins();
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(CoinsCount, 5);
            _session.Data.Inventory.Remove("Coin", numCoinsToDispose);

            // _hitDrop.SetCount(numCoinsToDispose);
            // _hitDrop.CalculateDrop();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.IsInLayer(_groundLayer))
            {
                var contact = other.contacts[0];
                if (contact.relativeVelocity.y > _jumpSpeed)
                {
                    _particles.Spawn("Fall");
                }
            }
        }


        public override void Attack()
        {
            if (SwordsCount <= 0) return;
            base.Attack();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordsCount > 0 ? _armed : _disarmed;
        }

        public void OnDoThrow()
        {
            if (_superThrow)
            {
                var numThrows = Mathf.Min(_superThrowParticles, SwordsCount - 1);
                StartCoroutine(DoSuperThrow(numThrows));
            }
            else
            {
                ThrowAndRemoveFromInventory();
            }

            _superThrow = false;
        }

        private IEnumerator DoSuperThrow(int numThrows)
        {
            for (int i = 0; i < numThrows; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }

        private void ThrowAndRemoveFromInventory()
        {
            Sounds.Play("Range");
            _particles.Spawn("Throw");
            _session.Data.Inventory.Remove("Sword", 1);
        }

        public void StartThrowing()
        {
            _superThrowCooldown.Reset();
        }

        public void PerformThrowing()
        {
            if (!_throwCooldown.IsReady || SwordsCount <= 1) return;

            if (_superThrowCooldown.IsReady) _superThrow = true;

            Animator.SetTrigger(ThrowKey);
            _throwCooldown.Reset();
        }

        public void Use()
        {
            if (PotionsCount > 0)
            {
                Sounds.Play("Use");
                Animator.SetTrigger(IsHeal);
                _health.ApplyDamage(5);
                _session.Data.Inventory.Remove("Potion", 1);
            }
        }
    }
}