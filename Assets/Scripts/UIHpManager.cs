using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpManager : MonoBehaviour
{
    const int initHp = 3;

    GameObject gameManager;
    GameObject redMaskObject;
    bool sizeUp = false;
    bool isDamage = false;
    bool alphaDown = false;
    int hp = initHp;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        redMaskObject = GameObject.Find("ImageDamageMask");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager)
        {
            if (hp > gameManager.GetComponent<GameManager>().hp)
            {
                isDamage = true;
            }
            hp = gameManager.GetComponent<GameManager>().hp;
        }

        for (int i = 1; i <= initHp; ++i)
        {
            // update color alpha
            GameObject obj = GameObject.Find("ImageHp" + i.ToString());
            if (i > hp)
            {
                Color color = obj.GetComponent<Image>().color;
                color.g = 0;
                color.b = 0;
                color.a -= 0.1f;
                if (color.a < 0)
                {
                    color.a = 0;
                }
                obj.GetComponent<Image>().color = color;
            }
            else
            {
                Color color = obj.GetComponent<Image>().color;
                color.a += 0.1f;
                if (color.a > 1.0f)
                {
                    color.a = 1.0f;
                }
                obj.GetComponent<Image>().color = color;
            }

            // update size
            Vector2 size = obj.GetComponent<RectTransform>().sizeDelta;
            if (sizeUp)
            {
                size += new Vector2(0.6f, 0.6f);
                if (size.x >= 70 && i == initHp)
                {
                    size = new Vector2(70, 70);
                    sizeUp = false;
                }
            }
            else
            {
                size -= new Vector2(0.6f, 0.6f);
                if (size.x <= 60 && i == initHp)
                {
                    size = new Vector2(60, 60);
                    sizeUp = true;
                }
            }
            obj.GetComponent<RectTransform>().sizeDelta = size;
        }

        // damage effect
        if (isDamage)
        {
            Color color = redMaskObject.GetComponent<Image>().color;
            if (alphaDown)
            {
                color.a -= 0.05f;
                if (color.a <= 0)
                {
                    color.a = 0;
                    alphaDown = false;
                    isDamage = false;
                }
            }
            else
            {
                color.a += 0.05f;
                if (color.a >= 0.5f)
                {
                    color.a = 0.5f;
                    alphaDown = true;
                }
            }
            redMaskObject.GetComponent<Image>().color = color;
        }
    }
}
