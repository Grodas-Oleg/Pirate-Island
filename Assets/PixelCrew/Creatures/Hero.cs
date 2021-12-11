<<<<<<< Updated upstream:Assets/PixelCrew/Creatures/Hero.cs
﻿using UnityEngine;
using PixelCrew.Components;
=======
﻿using PixelCrew.Components;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.Health;
>>>>>>> Stashed changes:Assets/PixelCrew/Creatures/Hero/Hero.cs
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

<<<<<<< Updated upstream:Assets/PixelCrew/Creatures/Hero.cs
        private float _maxThrowSwords = 5;
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        [Space] [Header("Particles")] [SerializeField]
        private ParticleSystem _hitParticles;
=======
        [SerializeField] private ProbabilityDropComponent _hitDrop;

        private bool _isOnWall;
        private static readonly int ThrowKey = Animator.StringToHash("throw");
        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");
        private static readonly int IsHeal = Animator.StringToHash("hit");
>>>>>>> Stashed changes:Assets/PixelCrew/Creatures/Hero/Hero.cs

        private bool _allowDoubleJump;


        private int CoinsCount => _session.Data.Inventory.Count("Coin");
        private int SwordsCount => _session.Data.Inventory.Count("Sword");
        private int PotionsCount => _session.Data.Inventory.Count("Potion");

        private GameSession _session;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            var health = GetComponent<HealthComponent>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;

            health.SetHealth(_session.Data.HP);
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

<<<<<<< Updated upstream:Assets/PixelCrew/Creatures/Hero.cs
            var burst = _hitParticles.emission.GetBurst(0);
            burst.count = numCoinsToDispose;
            _hitParticles.emission.SetBurst(0, burst);

            _hitParticles.gameObject.SetActive(true);
            _hitParticles.Play();
=======
            // _hitDrop.SetCount(numCoinsToDispose);
            // _hitDrop.CalculateDrop();
>>>>>>> Stashed changes:Assets/PixelCrew/Creatures/Hero/Hero.cs
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
            _particles.Spawn("Throw");
        }

        public void Throw()
        {
            if (_throwCooldown.IsReady && SwordsCount > 1)
            {
                Animator.SetTrigger(ThrowKey);
                _throwCooldown.Reset();
                _session.Data.ThrowSwordQuantity -= 1;
            }
        }

        public void Use()
        {
            if (PotionsCount > 0)
            {
                Animator.SetTrigger(IsHeal);
                var health = GetComponent<HealthComponent>();
                health.ApplyDamage(5);
                _session.Data.Inventory.Remove("Potion", 1);
            }
        }
    }
}