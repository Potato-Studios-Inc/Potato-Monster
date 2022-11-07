using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField] RectTransform[] charBtn;
    [SerializeField] RectTransform indicator;
    [SerializeField] float moveDelay;
    int indicatorPos;
    float moveTimer;


    void Update()
    {
        if(moveTimer < moveDelay)
        {
            moveTimer += Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (moveTimer >= moveDelay)
            {
                if (indicatorPos < charBtn.Length - 1)
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
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (moveTimer >= moveDelay) {
                if (indicatorPos > 0) 
                {
                    indicatorPos--;
                }
                else
                {
                    indicatorPos = charBtn.Length - 1;
                }

                moveTimer = 0;
            }
        }
        indicator.localPosition = charBtn[indicatorPos].localPosition;
    }
}
