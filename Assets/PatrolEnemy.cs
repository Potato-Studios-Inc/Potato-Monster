using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using Random = UnityEngine.Random;

public class PatrolEnemy : MonoBehaviour
{
    public float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    int currentPointIndex;
    private SpriteRenderer _renderer;
    public playerHealth player;
    public AudioSource audioSource;
    public AudioClip snailSound;
    public Camera camera;
    bool once;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        snailSound = Resources.Load<AudioClip>("Sounds/snail");
        audioSource.volume = 0.1f;
    }

    private void Update()
    {
        //Check if snail is in camera view
        if (camera.WorldToViewportPoint(transform.position).x is > 0 and < 1 && 
            camera.WorldToViewportPoint(transform.position).y is > 0 and < 1)
        {
            audioSource.mute = false;
        }
        else
        {
            audioSource.mute = true;
        }

        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, 
                patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
        }
        else
        {
            if (once == false)
            {
                once = true;
                StartCoroutine(Wait());
                
            }
        }
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var damage = 15;
        StartCoroutine(WaitAndTakeDamage(damage));
    }

    IEnumerator WaitAndTakeDamage(int damage)
    {
       
        player.TakeDamage(damage);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(waitTime);

        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
            _renderer.flipX = true;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(snailSound);
            }

        }
        else
        {
            currentPointIndex = 0;
            _renderer.flipX = false;
        }
        once = false;
    }
}
