using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class waterDrop : MonoBehaviour
{
    private bool _playerHit;
    private playerHealth _playerHealth;
    public AudioSource audioSource;
    public Rigidbody2D player;
    public Camera _camera;
    public Animator animator;

    public bool CameraEnabled;


    private void Start()
    {
        //find 'water-drop-animation' and assign it to anim
        animator = GameObject.Find("water-drop").GetComponent<Animator>();
        _camera = GameObject.Find("L4 Camera").GetComponent<Camera>();
        CameraEnabled = true;
        _playerHealth = GameObject.Find("Player").GetComponent<playerHealth>();
        audioSource = GetComponent<AudioSource>();
        //audioSource.mute = true;
    }
    void Update()
    {
        //disable animator when _camera is not active
        if (_camera.enabled == false)
        {
            CameraEnabled = false;
            audioSource.mute = true;
        }
        else
        {
            CameraEnabled = true;
            audioSource.mute = false;
        }
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
    
    public void play_sound()
    {
        audioSource.Play();
    }
}
