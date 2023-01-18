using System;
using UnityEngine;

public class AnimateSingleAxis : MonoBehaviour
{
    private Vector2 _deltaPos;

    public float deltaY;

    private Vector2 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void Update()
    {
        var positionChanged = false;
        if (_deltaPos.y != deltaY)
        {
            _deltaPos.y = deltaY;
            positionChanged = true;
        }
        
        if (positionChanged)
        {
            transform.position = _startPos + _deltaPos;
        }
    }
}