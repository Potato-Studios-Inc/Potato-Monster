using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BreakableWindow : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public int hitsReceived;
    public Sprite[] frames;
    private bool _hotSinglePlayerInYourArea;
    public BoxCollider2D endZone;
    public AudioSource audioSource;
    public AudioClip glasSound;
    public AudioClip attackSound;



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        attackSound = Resources.Load("Sounds/attackSound") as AudioClip;
        glasSound = Resources.Load("Sounds/glassBreak") as AudioClip;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        endZone.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period) && _hotSinglePlayerInYourArea)
        {
            OnHit();
        }

        _spriteRenderer.sprite = frames[hitsReceived];
    }

    private bool IsDestroyed()
    {
        return hitsReceived >= frames.Length - 1;
    }

    private void OnHit()
    {
        if (!IsDestroyed())
        {
            audioSource.PlayOneShot(attackSound, 0.7f);
            hitsReceived++;
        }
        
        if (IsDestroyed())
        {
            audioSource.PlayOneShot(glasSound, 0.7f);
            OnDestroyed();
        }
    }

    private void OnDestroyed()
    {
        endZone.enabled = true;
    }

    public void Reset()
    {
        hitsReceived = 0;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        _hotSinglePlayerInYourArea = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        _hotSinglePlayerInYourArea = false;
    }
    
}