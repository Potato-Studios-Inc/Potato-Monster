using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class waterDrop : MonoBehaviour
{
    private bool _playerHit;
    private playerHealth _playerHealth;
    public AudioSource audioSource;
    public Camera _camera;
    public Animator animator;
    public Rigidbody2D player;


    private void Start()
    {
        //find 'water-drop-animation' and assign it to anim
        animator = GameObject.Find("water-drop").GetComponent<Animator>();
        _camera = GameObject.Find("L4 Camera").GetComponent<Camera>();
        _playerHealth = GameObject.Find("Player").GetComponent<playerHealth>();
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        //mute audioSource if the player is not in the 'L4 camera'
        animator.enabled = _camera.isActiveAndEnabled;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        _playerHit = false;
    }
    
    public void play_sound()
    {
        audioSource.Play();
    }
}
