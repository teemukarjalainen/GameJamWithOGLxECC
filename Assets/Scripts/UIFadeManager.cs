using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFadeManager : MonoBehaviour
{
    const int fadeSpeed = 10;

    GameObject leftFadeObject1;
    GameObject leftFadeObject2;
    GameObject rightFadeObject1;
    GameObject rightFadeObject2;
    public bool fadeIn = true;
    public bool fadeOut = false;

    // Use this for initialization
    void Start()
    {
        leftFadeObject1 = GameObject.Find("ImageFadeLeft1");
        leftFadeObject2 = GameObject.Find("ImageFadeLeft2");
        rightFadeObject1 = GameObject.Find("ImageFadeRight1");
        rightFadeObject2 = GameObject.Find("ImageFadeRight2");
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeIn)
        {
            fadeOut = false;
            {// left fade
                Vector3 pos = leftFadeObject2.transform.localPosition;
                pos.x -= fadeSpeed;
                leftFadeObject2.transform.localPosition = pos;
                if (pos.x <= -480)
                {
                    Vector3 pos2 = leftFadeObject1.transform.localPosition;
                    pos2.x -= fadeSpeed;
                    leftFadeObject1.transform.localPosition = pos2;
                }
                if (pos.x <= -800)
                {
                    pos.x = -800;
                    leftFadeObject1.transform.localPosition = pos;
                    leftFadeObject2.transform.localPosition = pos;
                }
            }
            {// right fade
                Vector3 pos = rightFadeObject2.transform.localPosition;
                pos.x += fadeSpeed;
                rightFadeObject2.transform.localPosition = pos;
                if (pos.x >= 480)
                {
                    Vector3 pos2 = rightFadeObject1.transform.localPosition;
                    pos2.x += fadeSpeed;
                    rightFadeObject1.transform.localPosition = pos2;
                }
                if (pos.x >= 800)
                {
                    pos.x = 800;
                    rightFadeObject1.transform.localPosition = pos;
                    rightFadeObject2.transform.localPosition = pos;
                    fadeIn = false;
                }
            }
        }
        if (fadeOut)
        {
            {// left fade
                Vector3 pos = leftFadeObject2.transform.localPosition;
                pos.x += fadeSpeed;
                leftFadeObject2.transform.localPosition = pos;
                if (pos.x >= -480)
                {
                    Vector3 pos2 = leftFadeObject1.transform.localPosition;
                    pos2.x = -480;
                    leftFadeObject1.transform.localPosition = pos2;
                }
                if (pos.x >= -160)
                {
                    pos.x = -160;
                    leftFadeObject2.transform.localPosition = pos;
                }
            }
            {// right fade
                Vector3 pos = rightFadeObject2.transform.localPosition;
                pos.x -= fadeSpeed;
                rightFadeObject2.transform.localPosition = pos;
                if (pos.x <= 480)
                {
                    Vector3 pos2 = rightFadeObject1.transform.localPosition;
                    pos2.x = 480;
                    rightFadeObject1.transform.localPosition = pos2;
                }
                if (pos.x <= 160)
                {
                    pos.x = 160;
                    rightFadeObject2.transform.localPosition = pos;
                    fadeOut = false;
                }
            }
        }
    }
}
