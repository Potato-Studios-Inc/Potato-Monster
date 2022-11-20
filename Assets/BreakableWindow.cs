using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BreakableWindow : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    public int hitsReceived;
    public Sprite[] frames;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Period))
        {
            OnHit();
        }

        _spriteRenderer.sprite = frames[hitsReceived];
    }

    public void OnHit()
    {
        if (hitsReceived < frames.Length - 1)
        {
            hitsReceived++;
        }
    }

    public void Reset()
    {
        hitsReceived = 0;
    }
}