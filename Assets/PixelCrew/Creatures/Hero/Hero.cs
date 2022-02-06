using System;
using System.Collections;
using System.Collections.Generic;
using PixelCrew.Components;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.Effects.CameraRelated;
using PixelCrew.Components.GoBased;
using PixelCrew.Components.Health;
using PixelCrew.Creatures.Hero.Features;
using PixelCrew.Model;
using PixelCrew.Model.Definitions;
using PixelCrew.Model.Definitions.Player;
using PixelCrew.Model.Definitions.Repositories;
using PixelCrew.Model.Definitions.Repositories.Items;
using PixelCrew.Utils;
using UnityEngine;
using UnityEngine.Analytics;
using Random = UnityEngine.Random;

namespace PixelCrew.Creatures.Hero
{
    public class Hero : Creature
    {
        [SerializeField] private CheckCircleOverlap _interactionCheck;
        [SerializeField] private Cooldown _throwCooldown;
        [SerializeField] private RuntimeAnimatorController _armed;
        [SerializeField] private RuntimeAnimatorController _disarmed;

        [SerializeField] private int _superThrowParticles;
        [SerializeField] private float _superThrowDelay;
        [SerializeField] private SpawnComponent _throwSpawner;
        [SerializeField] private ProbabilityDropComponent _hitDrop;
        [SerializeField] private ShieldPerk _shield;
        [SerializeField] private SpawnComponent _canonSpawner;
        [SerializeField] private HeroFlashLight _light;

        private static readonly int ThrowKey = Animator.StringToHash("throw");

        public event Action<string, Cooldown> OnPerkUsed;

        private bool _allowDoubleJump;
        private float _defaultGravityScale;
        private float _additionalSpeed;
        private const string SwordId = "Sword";

        private int CoinsCount => _session.Data.Inventory.Count("Coin");
        private int SwordsCount => _session.Data.Inventory.Count(SwordId);
        private string SelectedItemId => _session.QuickInventory.SelectedItem.Id;

        private readonly int _dashCost = 10, _attackCost = 15;

        // private int _superThrowCount;
        // private int _cannonUseCount;
        // private int _shieldUseCount;

        private readonly Cooldown _cannonCooldown = new Cooldown();
        private readonly Cooldown _superThrowCooldown = new Cooldown();
        private readonly Cooldown _shieldCooldown = new Cooldown();
        private readonly Cooldown _speedUpCooldown = new Cooldown();

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

        private HeroStaminaController _stamina;
        private CameraShakeEffect _cameraShake;
        private GameSession _session;
        private HealthComponent _health;
        private AttackManager _attackManager;

        protected override void Awake()
        {
            base.Awake();

            _cameraShake = FindObjectOfType<CameraShakeEffect>();
            _session = GameSession.Instance;
            _health = GetComponent<HealthComponent>();
            _attackManager = GetComponent<AttackManager>();
            _stamina = GetComponent<HeroStaminaController>();

            _defaultGravityScale = Rigidbody.gravityScale;
        }

        private void Start()
        {
            // Управление PlayerInput 
            // var input = GetComponent<PlayerInput>();
            // input.actions["123"].ReadValue<Vector2>();
            // input.actions["123"].triggered;

            _session.Data.Inventory.OnChanged += OnInventoryChanged;
            _session.StatsModel.OnUpgrade += OnHeroUpgraded;

            _health.SetHealth(_session.Data.HP.Value);
            _stamina.SetStamina(_session.Data.Stamina.Value);
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
                case StatId.Stamina:
                    var stamina = (int) _session.StatsModel.GetValue(statId);
                    _session.Data.Stamina.Value = stamina;
                    _stamina.SetStamina(stamina);
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
            Rigidbody.gravityScale = IsDashing ? 0 : _defaultGravityScale;

            ModifyDamageByStat();
        }

        protected override float CalculateYVelocity()
        {
            if (IsGrounded) _allowDoubleJump = true;

            return base.CalculateYVelocity();
        }

        protected override float CalculateJumpVelocity(float yVeloсity)
        {
            if (IsGrounded || !_allowDoubleJump || !_session.PerksModel.IsDoubleJumpSupported)
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

            _cameraShake?.Shake();
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
            if (GameSession.Instance.Data.Stamina.Value < _attackCost) return;
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
            _stamina.UseStamina(_attackCost);
        }

        private void ModifyDamageByStat()
        {
            var critical = false;
            var dmgModify = GameObject.FindGameObjectWithTag("MeleeAttack").GetComponent<HealthChangeComponent>();
            var modifyValue = _session.StatsModel.GetValue(StatId.Damage);

            var perkCriticalChance = _session.StatsModel.GetValue(StatId.CriticalChance);
            var perkCriticalDamage = _session.StatsModel.GetValue(StatId.CriticalDamage);

            if (Random.value * 100 <= perkCriticalChance)
            {
                modifyValue *= perkCriticalDamage;
                critical = true;
            }


            dmgModify.SetDelta((int) -modifyValue, critical);
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
            if (!_dashDelay.IsReady) return;
            if (GameSession.Instance.Data.Stamina.Value < _dashCost) return;
            StartCoroutine(XDirection ? Dash(1f) : Dash(-1f));
            _dashDelay.Reset();
        }

        IEnumerator Dash(float direction)
        {
            IsDashing = true;
            Sounds.Play("Dash");
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f);
            Rigidbody.AddForce(new Vector2(_dashDistance * direction, 0f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.2f);
            IsDashing = false;
            _stamina.UseStamina(_dashCost);
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

        public void UsePerk1()
        {
            var cooldown = _session.PerksModel.PerkCooldown("shield");
            _shieldCooldown.Value = cooldown;
            if (!_session.PerksModel.IsShieldSupported || !_shieldCooldown.IsReady) return;

            _shield.Use();
            _shieldCooldown.Reset();

            // _shieldUseCount++;
            // AnalyticsEvent.Custom("use-shield", new Dictionary<string, object>
            // {
            //     {"count", _shieldUseCount},
            //     {"level", _session.LevelIndex}
            // });

            OnPerkUsed?.Invoke("shield", _shieldCooldown);
        }

        public void UsePerk2()
        {
            var cooldown = _session.PerksModel.PerkCooldown("super-throw");
            _superThrowCooldown.Value = cooldown;

            if (!_session.PerksModel.IsSuperThrowSupported || !_superThrowCooldown.IsReady || SwordsCount <= 1) return;

            var possibleCount = SwordsCount - 1;
            var numThrows = Mathf.Min(_superThrowParticles, possibleCount);
            StartCoroutine(DoSuperThrow(numThrows));
            Animator.SetTrigger(ThrowKey);
            _superThrowCooldown.Reset();

            // _superThrowCount++;
            // AnalyticsEvent.Custom("use-super-throw", new Dictionary<string, object>
            // {
            //     {"count", _superThrowCount},
            //     {"level", _session.LevelIndex}
            // });

            OnPerkUsed?.Invoke("super-throw", _superThrowCooldown);
        }

        private IEnumerator DoSuperThrow(int numThrows)
        {
            for (var i = 0; i < numThrows; i++)
            {
                ThrowAndRemoveFromInventory();
                yield return new WaitForSeconds(_superThrowDelay);
            }
        }

        public void UsePerk3()
        {
            var cooldown = _session.PerksModel.PerkCooldown("cannon");
            _cannonCooldown.Value = cooldown;
            if (!_session.PerksModel.IsCannonSupported || !_cannonCooldown.IsReady) return;

            _canonSpawner.Spawn();
            _cannonCooldown.Reset();

            // _cannonUseCount++;
            // AnalyticsEvent.Custom("use-cannon", new Dictionary<string, object>
            // {
            //     {"count", _cannonUseCount},
            //     {"level", _session.LevelIndex}
            // });

            OnPerkUsed?.Invoke("cannon", _cannonCooldown);
        }

        public void SwitchLight()
        {
            _light.gameObject.SetActive(!_light.gameObject.activeInHierarchy);
            var fuel = GameObject.FindWithTag("FuelBar").GetComponent<CanvasGroup>();
            fuel.alpha = fuel.alpha == 1f ? .5f : 1f;
        }
    }
}