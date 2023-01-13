using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animations : MonoBehaviour
{
    public Animation _animation;
    public GameObject camerasParent;
    private Camera[] _cameras;

    void Start()
    {
        _cameras = camerasParent.GetComponentsInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("L1 Camera").GetComponent<Camera>())
        {
            GameObject.Find("KITCHEN").GetComponent<Image>();
            _animation.Play();
        }
    }
}
