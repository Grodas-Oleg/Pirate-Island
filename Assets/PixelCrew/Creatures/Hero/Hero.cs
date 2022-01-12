using System.Collections;
using PixelCrew.Components;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using PixelCrew.Components.Health;
using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Model.Definitions.Repositories;
using PixelCrew.Model.Definitions.Repositories.Items;
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

        [SerializeField] private int _superThrowParticles;
        [SerializeField] private float _superThrowDelay;
        [SerializeField] private SpawnComponent _throwSpawner;
        [SerializeField] private ProbabilityDropComponent _hitDrop;
        [SerializeField] private ShieldPerk _shield;
        [SerializeField] private SpawnComponent _canonSpawner;

        private bool _isOnWall;
        private static readonly int ThrowKey = Animator.StringToHash("throw");

        private static readonly int IsOnWall = Animator.StringToHash("is-on-wall");

        private bool _allowDoubleJump;
        private float _defaultGravityScale;

        private const string SwordId = "Sword";
        private int CoinsCount => _session.Data.Inventory.Count("Coin");
        private int SwordsCount => _session.Data.Inventory.Count(SwordId);
        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

        private bool CanThrow
        {
            get
            {
                if (SelectedItemId == SwordId)
                    return SwordsCount > 1;

                var def = DefsFacade.I.Items.Get(SelectedItemId);
                return def.HasTag(ItemTag.Throwable);
            }
        }

        private GameSession _session;
        private HealthComponent _health;
        private AttackManager _attackManager;

        protected override void Awake()
        {
            base.Awake();
            _defaultGravityScale = Rigidbody.gravityScale;
        }

        private void Start()
        {
            _session = FindObjectOfType<GameSession>();
            _health = GetComponent<HealthComponent>();
            _attackManager = GetComponent<AttackManager>();
            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _session.StatsModel.OnUpgrade += OnHeroUpgraded;

            _health.SetHealth(_session.Data.HP.Value);
            UpdateHeroWeapon();
        }

        private void OnHeroUpgraded(StatId statId)
        {
            switch (statId)
            {
                case StatId.Hp:
                    var health = (int) _session.StatsModel.GetValue(statId);
                    _session.Data.HP.Value = health;
                    _health.SetHealth(health);
                    break;
            }
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
            _session.Data.HP.Value = currentHealth;
        }

        protected override void Update()
        {
            base.Update();

            var moveToSameDirection = Direction.x * transform.lossyScale.x > 0;
            if (_wallCheck.IsTouchingLayer && moveToSameDirection)
            {
                _isOnWall = true;
                Rigidbody.gravityScale = 0;
                Direction.y = 0;
            }
            else
            {
                _isOnWall = false;
                Rigidbody.gravityScale = _defaultGravityScale;
            }

            Rigidbody.gravityScale = IsDashing || _isOnWall ? 0 : _defaultGravityScale;

            Animator.SetBool(IsOnWall, _isOnWall);
            ModifyDamageByStat();
        }

        protected override float CalculateYVelocity()
        {
            var isJumpPressing = Direction.y > 0;
            if (IsGrounded || _isOnWall) _allowDoubleJump = true;
            if (_isOnWall && !isJumpPressing) return 0f;
            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVeloсity)
        {
            if (IsGrounded || !_allowDoubleJump || !_session.PerksModel.IsDoubleJumpSupported || _isOnWall)
                return base.CalculateJumpVelocity(yVeloсity);
            _allowDoubleJump = false;
            DoJumpVFX();
            return _jumpSpeed;
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

            _hitDrop.SetCount(numCoinsToDispose);
            _hitDrop.CalculateDrop();
        }

        public void Interact()
        {
            _interactionCheck.Check();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.IsInLayer(_groundLayer)) return;
            var contact = other.contacts[0];
            if (!(contact.relativeVelocity.y > _jumpSpeed)) return;
            _particles.Spawn("Fall");
        }

        public override void Attack()
        {
            if (SwordsCount <= 0) return;

            _attackManager.DoAttack();
        }

        public void AnimationEvent()
        {
            _attackManager.CheckAttackCombo();
        }

        protected override void MakeAttack()
        {
            base.MakeAttack();
            Sounds.Play("Melee");
        }

        private void ModifyDamageByStat()
        {
            var dmgModify = GameObject.FindGameObjectWithTag("MeleeAttack").GetComponent<HealthChangeComponent>();
            var modifyValue = (int) _session.StatsModel.GetValue(StatId.Damage);
            modifyValue = (int) ModifyByCrit(modifyValue);

            dmgModify.SetDelta(-modifyValue);
        }

        private float ModifyByCrit(float damage)
        {
            var perkCriticalChance = _session.StatsModel.GetValue(StatId.CriticalChance);
            var perkCriticalDamage = _session.StatsModel.GetValue(StatId.CriticalDamage);

            return Random.value * 100 <= perkCriticalChance ? damage *= perkCriticalDamage : damage;
        }

        private void UpdateHeroWeapon()
        {
            Animator.runtimeAnimatorController = SwordsCount > 0 ? _armed : _disarmed;
        }

        private void ThrowAndRemoveFromInventory()
        {
            Sounds.Play("Range");
            var throwableDef = DefsFacade.I.ThrowableItems.Get(SwordId);
            _throwSpawner.SetPrefab(throwableDef.Projectile);
            _throwSpawner.Spawn();

            _session.Data.Inventory.Remove(SwordId, 1);
        }

        public override void Dash()
        {
            if (!_session.PerksModel.IsDashSupported) return;
            base.Dash();
        }

        public void UseInventory()
        {
            if (IsSelectedItem(ItemTag.Throwable))
                PerformThrowing();
            else if (IsSelectedItem(ItemTag.Potion))
                UsePotion();
        }

        private void UsePotion()
        {
            var potion = DefsFacade.I.Potions.Get(SelectedItemId);

            switch (potion.Effect)
            {
                case Effect.AddHp:
                    var health = _session.Data.HP.Value += (int) potion.Value;
                    _health.SetHealth(health);
                    break;
                case Effect.SpeedUp:
                    _speedUpCooldown.Value = _speedUpCooldown.RemainingTime + potion.Time;
                    _additionalSpeed = Mathf.Max(potion.Value, _additionalSpeed);
                    _speedUpCooldown.Reset();
                    break;
            }

            _session.Data.Inventory.Remove(potion.Id, 1);
        }

        private readonly Cooldown _speedUpCooldown = new Cooldown();
        private float _additionalSpeed;

        protected override float CalculateSpeed()
        {
            if (_speedUpCooldown.IsReady)
                _additionalSpeed = 0f;

            var defaultSpeed = _session.StatsModel.GetValue(StatId.Speed);
            return defaultSpeed + _additionalSpeed;
        }

        private bool IsSelectedItem(ItemTag itemTag)
        {
            return _session.QuickInventory.SelectedDef.HasTag(itemTag);
        }

        public void PerformThrowing()
        {
            if (!_throwCooldown.IsReady || !CanThrow) return;
            Animator.SetTrigger(ThrowKey);
            ThrowAndRemoveFromInventory();
            _throwCooldown.Reset();
        }

        public void NextItem()
        {
            _session.QuickInventory.SetNextItem();
        }

        private readonly Cooldown _shieldCooldown = new Cooldown();

        public void UsePerk1()
        {
            var cooldown = _session.PerksModel.UsePerk("shield");
            _shieldCooldown.Value = cooldown;
            if (_session.PerksModel.IsShieldSupported && _shieldCooldown.IsReady)
            {
                _shield.Use();
                _shieldCooldown.Reset();
            }
        }

        private readonly Cooldown _superThrowCooldown = new Cooldown();

        public void UsePerk2()
        {
            var cooldown = _session.PerksModel.UsePerk("super-throw");
            _superThrowCooldown.Value = cooldown;
            if (_session.PerksModel.IsSuperThrowSupported && _superThrowCooldown.IsReady && SwordsCount > 1)
            {
                var possibleCount = SwordsCount - 1;
                var numThrows = Mathf.Min(_superThrowParticles, possibleCount);
                StartCoroutine(DoSuperThrow(numThrows));
                Animator.SetTrigger(ThrowKey);
                _superThrowCooldown.Reset();
            }
        }

        private IEnumerator DoSuperThrow(int numThrows)
        {
            for (var i = 0; i < numThrows; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }

        private readonly Cooldown _cannonCooldown = new Cooldown();

        public void UsePerk3()
        {
            var cooldown = _session.PerksModel.UsePerk("cannon");
            _cannonCooldown.Value = cooldown;
            if (_session.PerksModel.IsCannonSupported && _cannonCooldown.IsReady)
            {
                _canonSpawner.Spawn();
                _cannonCooldown.Reset();
            }
        }
    }
}