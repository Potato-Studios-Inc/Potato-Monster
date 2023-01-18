using System;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Player
{
    public class playerHealth : MonoBehaviour
    {
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        public int health = 100;
        private Animator _animator;
        private Rigidbody2D _rb;
        private UnityEngine.Vector2 _velocity;
        public float fallThreshold = 6;
        public float fallMultiplier = 20;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            _velocity = _rb.velocity;
            var isDead = _animator.GetBool(IsDead);
            if (!isDead && health <= 0)
            {
                Die();
            }
            else if (isDead && health > 0)
            {
                Revive();
            }
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
        }

        public void Recover(int heal)
        {
            health += heal;
            if (health > 100)
            {
                health = 100;
            }
        }

        void Die()
        {
            _animator.SetBool(IsDead, true);
        }

        void Revive()
        {
            _animator.SetBool(IsDead, false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            var isGround = other.gameObject.CompareTag("Ground");
            if (isGround)
            {
                var v = -_velocity.y;
                if (v >= fallThreshold)
                {
                    var damage = (int)((v - fallThreshold) * fallMultiplier);
                    TakeDamage(damage);
                }
            }
        }
    }
}