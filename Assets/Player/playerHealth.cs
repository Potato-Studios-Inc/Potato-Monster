using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vector2 = System.Numerics.Vector2;

namespace Player
{
    public class playerHealth : MonoBehaviour
    {
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        public int health = 100;
        public GameObject gameOverMenu;
        private Animator _animator;
        public static event Action OnPlayerDeath;

        private Rigidbody2D _rb;
        private UnityEngine.Vector2 _velocity;
        public float fallThreshold = 6;
        public float fallMultiplier = 20;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody2D>();
            DontDestroyOnLoad(gameOverMenu);
        }


        private void Update()
        {
            _velocity = _rb.velocity;
            var isDead = _animator.GetBool(IsDead);
            if (!isDead && health <= 0)
            {
                Die();
                OnPlayerDeath?.Invoke();
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

        public void OnEnable()
        {
            playerHealth.OnPlayerDeath += EnableGameOverMenu;
        }

        public void OnDisable()
        {
            playerHealth.OnPlayerDeath -= EnableGameOverMenu;
        }

        public void EnableGameOverMenu()
        {
            gameOverMenu.SetActive(true);
        }

        //public void OnEnableStart()
        //{
        //    playerHealth.OnPlayerStart += DisableGameOverMenu;
        //}

        //public void DisableGameOverMenu()
        //{
        //    gameOverMenu.SetActive(false);
        //}

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("Menu");
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