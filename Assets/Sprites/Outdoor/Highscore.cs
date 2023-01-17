using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscore : MonoBehaviour
{
    public Rigidbody2D player;
    public float groundLevel;
    public float scoreMultiplier;
    public int score;

    private TextMeshProUGUI _textMesh;

    // Start is called before the first frame update
    void Start()
    {
        _textMesh = GetComponent<TextMeshProUGUI>();
        OnScoreUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        var newScore = (int)Math.Round((player.transform.position.y - groundLevel) * scoreMultiplier);
        if (newScore > score)
        {
            score = newScore;
            OnScoreUpdate();
        }
    }

    void OnScoreUpdate()
    {
        _textMesh.text = "Score: " + score;
    }
}