using System.Collections;
using System.Collections.Generic;
using CameraMovement;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

public class platforms : MonoBehaviour
{
    public GameObject prefab;
    public float xDeltaMin, xDeltaMax, yDeltaMin, yDeltaMax;
    public float xMin, xMax;
    private Vector3 _lastPosition;
    private float _platformWidth;

    public AutoMove autoMove;

    // Start is called before the first frame update
    void Start()
    {
        _lastPosition = prefab.transform.position;
        _platformWidth = prefab.GetComponent<SpriteRenderer>().size.x * prefab.transform.localScale.x;
        Debug.Log(_platformWidth);
    }

    private Vector3 GetNextPosition()
    {
        var next = _lastPosition;
        next.y += Random.Range(yDeltaMin, yDeltaMax);
        // Calculate the range of x values that will keep the platform on the tree
        var xPosMin = Mathf.Max(xMin, _lastPosition.x + xDeltaMin);
        var xPosMax = Mathf.Min(xMax, _lastPosition.x + xDeltaMax);
        var xPosDeltaMin = xPosMin - _lastPosition.x;
        var xPosDeltaMax = xPosMax - _lastPosition.x;
        if (xPosDeltaMin > -_platformWidth)
        {
            // The platform is too close to the left edge, so move it to the right
            xPosDeltaMin = _platformWidth;
        }
        else if (xPosDeltaMax < _platformWidth)
        {
            // The platform is too close to the right edge, so move it to the left
            xPosDeltaMax = -_platformWidth;
        }
        else 
        {
            // Make sure the platform doesn't overlap the previous one
            var moveRight = Random.value > 0.5f;
            if (moveRight)
            {
                xPosDeltaMin = _platformWidth;
            }
            else
            {
                xPosDeltaMax = -_platformWidth;
            }
        }

        next.x += Random.Range(xPosDeltaMin, xPosDeltaMax);
        Debug.Log("xPosMin: " + xPosMin + " xPosMax: " + xPosMax + " xPosDeltaMin: " + xPosDeltaMin +
                  " xPosDeltaMax: " + xPosDeltaMax);
        return next;
    }

    // Update is called once per frame
    void Update()
    {
        var (cameraMin, cameraMax) = autoMove.CameraViewVectors();

        if (cameraMax.y < _lastPosition.y) return;
        var pos = GetNextPosition();
        var newPlatform = Instantiate(prefab, transform);
        newPlatform.transform.position = pos;
        _lastPosition = pos;
    }
}