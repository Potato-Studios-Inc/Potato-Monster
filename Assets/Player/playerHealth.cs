using System;
using UnityEngine;

namespace Player
{
    public class playerHealth : MonoBehaviour
    {
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        public int health = 100;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            var isDead = _animator.GetBool(IsDead);
            if (!isDead && health <= 0)
            {
                Die();
            } else if (isDead && health > 0)
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
    }
}