using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class waterDrop : MonoBehaviour
{
    private bool _playerHit;
    private playerHealth _playerHealth;
    
    private void Start()
    {
        _playerHealth = GameObject.Find("Player").GetComponent<playerHealth>();
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        _playerHit = true;
        _playerHealth.TakeDamage(25);
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        _playerHit = false;
    }
}
