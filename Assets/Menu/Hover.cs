using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hover : MonoBehaviour
{
    private List<RectTransform> _buttons = new();
    [SerializeField] RectTransform indicator;
    [SerializeField] float moveDelay;
    int indicatorPos;
    float moveTimer;

    private void Start()
    {
        foreach (RectTransform child in transform)
        {
            _buttons.Add(child);
        }
    }

    void Update()
    {
        if (_buttons.Count == 0)
        {
            return;
        }

        if (moveTimer < moveDelay)
        {
            moveTimer += Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (moveTimer >= moveDelay)
            {
                if (indicatorPos < _buttons.Count - 1)
                {
                    indicatorPos++;
                }
                else
                {
                    indicatorPos = 0;
                }

                moveTimer = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (moveTimer >= moveDelay)
            {
                if (indicatorPos > 0)
                {
                    indicatorPos--;
                }
                else
                {
                    indicatorPos = _buttons.Count - 1;
                }

                moveTimer = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            var btn = _buttons[indicatorPos].GetComponent<Button>();
            if (btn)
            {
                btn.onClick.Invoke();
            }
        }
        
        indicator.localPosition = _buttons[indicatorPos].localPosition;
    }
}