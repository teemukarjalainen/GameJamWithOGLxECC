﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScoreManager : MonoBehaviour
{
    public GameObject moveUpObject;
    public int score = 0;
    public int plusScore = 0;

    GameObject gameManager;
    GameObject audioObject;
    GameObject scoreObject;
    GameObject moveUpParent;
    int doorPassed = 0;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        audioObject = GameObject.Find("Audio");
        scoreObject = GameObject.Find("TextScoreNumber");
        moveUpParent = GameObject.Find("TextMoveUpParent");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager)
        {
            if (doorPassed < gameManager.GetComponent<GameManager>().doorsPassed)
            {
                doorPassed = gameManager.GetComponent<GameManager>().doorsPassed;
                InstantiateMoveUpText(100 * gameManager.GetComponent<GameManager>().combo);
            }
        }

        // cheat button
        if (Input.GetKeyDown(KeyCode.F12))
        {
            InstantiateMoveUpText(500);
        }

        if (plusScore > 0)
        {
            if(plusScore - 16 >= 0)
            {
                plusScore -= 16;
                score += 16;
            }
            else
            {
                score += plusScore;
                plusScore = 0;
            }
        }

        string str0 = "";
        for (int i = 0; i < 10 - score.ToString().Length; ++i)
        {
            str0 += "0";
        }
        scoreObject.GetComponent<Text>().text = str0 + score.ToString();
    }

    void InstantiateMoveUpText(int addScore)
    {
        audioObject.GetComponent<UIAudioManager>().PlayScore();
        int down = 0;
        if (moveUpParent.transform.childCount > 0)
        {
            Vector3 childPos = moveUpParent.transform.GetChild(moveUpParent.transform.childCount - 1).localPosition;
            down = (int)(childPos.y) / 40 - 2;
        }
        if (down > -2)
        {
            down = -2;
        }
        GameObject instObj = Instantiate<GameObject>(moveUpObject, moveUpParent.transform);
        Vector3 instPos = instObj.transform.localPosition;
        instPos.y = down * 40;
        instObj.transform.localPosition = instPos;
        instObj.GetComponent<Text>().text = "+" + addScore.ToString();
        instObj.GetComponent<UIMoveUpScore>().addScore = addScore;
    }

    public void AddPlusScore(int addScore)
    {
        plusScore += addScore;
    }
}
