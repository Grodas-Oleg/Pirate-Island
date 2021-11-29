using UnityEngine;
using PixelCrew.Components;
using PixelCrew.Model;
using PixelCrew.Utils;
using UnityEditor.Animations;

namespace PixelCrew.Creatures
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;

        [SerializeField] private float _interactionRadius;

        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private AnimatorController _armed;
        [SerializeField] private AnimatorController _disarmed;

        private float _maxThrowSwords = 5;
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        [Space] [Header("Particles")] [SerializeField]
        private ParticleSystem _hitParticles;

        private bool _allowDoubleJump;


        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();

            health.SetHealth(_session.Data.HP);
            UpdateHeroWeapon();
        }

        public void OnHealthChanged(int currentHealth)
        {
            _session.Data.HP = currentHealth;
        }

        protected override void Update()
        {
            base.Update();
        }

        protected override float CalculateYVelocity()
        {
            if (IsGrounded) _allowDoubleJump = true;
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVelosity)
        {
            if (!IsGrounded && _allowDoubleJump)
            {
                _particles.Spawn("Jump");
                _allowDoubleJump = false;
                return _jumpSpeed;
            }

            return base.CalculateJumpVelocity(yVelosity);
        }

        public void AddCoins(int coins)
        {
            _session.Data.Coin += coins;
            CoinCounter.totalCoins += coins;
        }

        public override void TakeDamage()
        {
            base.TakeDamage();
            if (_session.Data.Coin > 0)
            {
                SpawnCoins();
            }
        }

        private void SpawnCoins()
        {
            var numCoinsToDispose = Mathf.Min(_session.Data.Coin, 5);
            _session.Data.Coin -= numCoinsToDispose;
            CoinCounter.totalCoins -= numCoinsToDispose;

            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
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
            if (!_session.Data.IsArmed) return;
            base.Attack();
        }

        public void ArmHero()
        {
            _session.Data.IsArmed = true;
            UpdateHeroWeapon();
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = _session.Data.IsArmed ? _armed : _disarmed;
        }

        public void OnDoThrow()
        {
            _particles.Spawn("Throw");
        }

        public void Throw()
        {
            if (_session.Data.ThrowSwordQuantity > 1 && _throwCooldown.IsReady)
            {
                Animator.SetTrigger(ThrowKey);
                _throwCooldown.Reset();
                _session.Data.ThrowSwordQuantity -= 1;
            }
        }

        public void ProjectilePickUp()
        {
            if (_session.Data.ThrowSwordQuantity >= _maxThrowSwords)
            {
                _session.Data.ThrowSwordQuantity = _maxThrowSwords;
            }
            else
            {
                _session.Data.ThrowSwordQuantity += 1;
            }
        }
    }
}