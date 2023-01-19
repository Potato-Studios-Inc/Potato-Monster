using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PatrolEnemy : MonoBehaviour
{
    public float speed;
    public Transform[] patrolPoints;
    public float waitTime;
    int currentPointIndex;
    private SpriteRenderer _renderer;
    public playerHealth player;
    bool once;


    private void Update()
    {
        if (transform.position != patrolPoints[currentPointIndex].position)
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);
            
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
        }
        else
        {
            currentPointIndex = 0;
            _renderer.flipX = false;
        }
        once = false;
    }
}
