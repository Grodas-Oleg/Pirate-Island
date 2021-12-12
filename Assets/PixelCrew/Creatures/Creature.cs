using System.Collections;
using PixelCrew.Components.Audio;
using PixelCrew.Components.ColliderBased;
using PixelCrew.Components.GoBased;
using UnityEngine;

namespace PixelCrew.Creatures
{
    public class Creature : MonoBehaviour
    {
        [Header("Params")] [SerializeField] private bool _invertScale;
        [SerializeField] protected float _speed;
        [SerializeField] protected float _jumpSpeed;
        [SerializeField] private float _damageVelocity;
        [SerializeField] private float _dashDistance;

        [Header("Checkers")] [SerializeField] protected LayerMask _groundLayer;
        [SerializeField] private LayerCheck _groundCheck;
        [SerializeField] private CheckCircleOverlap _attackRange;
        [SerializeField] protected SpawnListComponent _particles;

        protected Rigidbody2D Rigidbody;
        protected Vector2 Direction;
        protected Animator Animator;
        protected PlaySoundsComponent Sounds;
        protected bool IsGrounded;
        private bool _isJumping;
        protected bool _isDashing = false;
        private float _dashDelay = 0.5f;
        private bool _xDirection;

        private static readonly int IsGroundKey = Animator.StringToHash("is-ground");
        private static readonly int IsRunning = Animator.StringToHash("is-running");
        private static readonly int VerticalVelocity = Animator.StringToHash("vertical-velocity");
        private static readonly int Hit = Animator.StringToHash("hit");
        private static readonly int IsAttack = Animator.StringToHash("attack");
        private static readonly int IsDashing = Animator.StringToHash("is-dashing");

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Animator = GetComponent<Animator>();
            Sounds = GetComponent<PlaySoundsComponent>();
        }

        public void SetDirection(Vector2 direction)
        {
            Direction = direction;
        }

        protected virtual void Update()
        {
            IsGrounded = _groundCheck.IsTouchingLayer;
        }

        protected virtual void FixedUpdate()
        {
            var xVelocity = Direction.x * _speed;
            var yVelocity = CalculateYVelocity();

            if (_dashDelay > 0f) _dashDelay -= Time.deltaTime;

            if (!_isDashing) Rigidbody.velocity = new Vector2(xVelocity, yVelocity);

            Animator.SetBool(IsRunning, Direction.x != 0);
            Animator.SetBool(IsGroundKey, IsGrounded);
            Animator.SetFloat(VerticalVelocity, Rigidbody.velocity.y);
            Animator.SetBool(IsDashing, _isDashing);

            UpdateSpriteDirection(Direction);
        }

        protected virtual float CalculateYVelocity()
        {
            var yVelosity = Rigidbody.velocity.y;
            var _isJumpPressing = Direction.y > 0;

            if (IsGrounded)
            {
                _isJumping = false;
            }

            if (_isJumpPressing)
            {
                _isJumping = true;

                var isFalling = Rigidbody.velocity.y <= 0.001f;
                yVelosity = isFalling ? CalculateJumpVelocity(yVelosity) : yVelosity;
            }
            else if (Rigidbody.velocity.y > 0 && _isJumping)
            {
                yVelosity *= 0.5f;
            }

            return yVelosity;
        }

        protected virtual float CalculateJumpVelocity(float yVeloсity)
        {
            if (IsGrounded)
            {
                yVeloсity = _jumpSpeed;
                DoJumpVFX();
            }

            return yVeloсity;
        }

        protected void DoJumpVFX()
        {
            _particles.Spawn("Jump");
            Sounds.Play("Jump");
        }

        public void UpdateSpriteDirection(Vector2 direction)
        {
            var multiplayer = _invertScale ? -1 : 1;
            if (direction.x > 0)
            {
                _xDirection = true;
                transform.localScale = new Vector3(multiplayer, 1, 1);
            }
            else if (direction.x < 0)
            {
                _xDirection = false;
                transform.localScale = new Vector3(-1 * multiplayer, 1, 1);
            }
        }

        public virtual void TakeDamage()
        {
            _isJumping = false;
            Animator.SetTrigger(Hit);
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, _damageVelocity);
        }

        public void Dash()
        {
            if (_dashDelay <= 0)
            {
                if (_xDirection)
                {
                    StartCoroutine(Dash(1f));
                }
                else
                {
                    StartCoroutine(Dash(-1f));
                }

                _dashDelay = 1f;
            }
        }

        IEnumerator Dash(float direction)
        {
            _isDashing = true;
            Sounds.Play("Dash");
            Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0f);
            Rigidbody.AddForce(new Vector2(_dashDistance * direction, 0f), ForceMode2D.Impulse);
            yield return new WaitForSeconds(.2f);
            _isDashing = false;
        }

        public virtual void Attack()
        {
            Animator.SetTrigger(IsAttack);
        }

        public void MakeAttack()
        {
            _particles.Spawn("Slash");
            _attackRange.Check();
            Sounds.Play("Melee");
        }
    }
}