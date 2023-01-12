using System;
using Player;
using UnityEngine;

namespace HealthBar
{
    public class HealthBar : MonoBehaviour
    {
        private Heart[] _hearts;
        private playerHealth _playerHealth;
        private int _health;

        void Start()
        {
            _playerHealth = GameObject.Find("Player").GetComponent<playerHealth>();
            _hearts = GetComponentsInChildren<Heart>();
        }

        // Update is called once per frame
        void Update()
        {
            if (_health != _playerHealth.health)
            {
                _health = _playerHealth.health;
                UpdateHearts(_health);
            }
        }

        void UpdateHearts(int health)
        {
            var fullHearts = Convert.ToDecimal(health) * _hearts.Length / 100;
            for (int i = 0; i < _hearts.Length; i++)
            {
                var heartPercentage = 0;
                if (i < Math.Floor(fullHearts))
                {
                    heartPercentage = 100;
                }
                else if (i < fullHearts)
                {
                    heartPercentage = Convert.ToInt32(fullHearts * 100) % 100;
                }

                _hearts[i].SetPercentage(heartPercentage);
            }
        }
    }
}