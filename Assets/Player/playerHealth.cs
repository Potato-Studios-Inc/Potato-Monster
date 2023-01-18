using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Player
{
    public class playerHealth : MonoBehaviour
    {
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        public int health = 100;
        public GameObject gameOverMenu;
        private Animator _animator;
        public static event Action OnPlayerDeath;
      
        private void Start()
        {
            _animator = GetComponent<Animator>();
            DontDestroyOnLoad(gameOverMenu);
         
        }

        

        private void Update()
        {
            
            var isDead = _animator.GetBool(IsDead);
            if (!isDead && health <= 0)
            {
                Die();
                playerHealth.OnPlayerDeath?.Invoke();
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
    }
}