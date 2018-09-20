using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    GameObject gameManager;
    GameObject audioObject;
    GameObject scoreObject;
    GameObject gameOverMaskObject;
    GameObject gameOverTextObject;
    GameObject gameOverScoreObject;
    GameObject gameOverRankingObject;
    GameObject gameOverNewRecord;
    GameObject gameOverButtonObject;
    GameObject gameOverButtonObject2;
    GameObject fadeObject;
    bool isGameOver = false;
    bool scoreCountStop = false;
    bool updatedRanking = false;
    bool activeNewRecord = false;
    bool alphaDown = false;
    bool fontSizeDown = false;
    int score = 0;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        audioObject = GameObject.Find("Audio");
        scoreObject = GameObject.Find("Score");
        gameOverMaskObject = GameObject.Find("ImageGameOverMask");
        gameOverTextObject = GameObject.Find("TextGameOver");
        gameOverScoreObject = GameObject.Find("TextGameOverScore");
        gameOverRankingObject = GameObject.Find("TextGameOverRanking");
        gameOverNewRecord = GameObject.Find("TextGameOverNewRecord");
        gameOverButtonObject = GameObject.Find("ButtonReStart");
        gameOverButtonObject2 = GameObject.Find("ButtonReturnToTitle");
        fadeObject = GameObject.Find("Fade");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11) && !isGameOver)
        {
            isGameOver = true;
            GameObject.Find("BGM").SetActive(false);
            GameObject.Find("BGMGameOver").GetComponent<UIAudioManager>().PlayGameOver();
        }

        if (gameManager && !isGameOver)
        {
            isGameOver = gameManager.GetComponent<GameManager>().isGameOver;
            if (isGameOver)
            {
                GameObject.Find("BGM").SetActive(false);
                GameObject.Find("BGMGameOver").GetComponent<UIAudioManager>().PlayGameOver();
            }
        }

        if (isGameOver)
        {
            // update mask size
            Vector2 size = gameOverMaskObject.GetComponent<RectTransform>().sizeDelta;
            size.y += 30;
            if (size.y >= 720)
            {
                size.y = 720;
            }
            gameOverMaskObject.GetComponent<RectTransform>().sizeDelta = size;

            if (size.y >= 360)
            {
                // update text alpha
                Color color = gameOverTextObject.GetComponent<Text>().color;
                color.a += 0.1f;
                if (color.a >= 1.0f)
                {
                    color.a = 1.0f;
                }
                gameOverTextObject.GetComponent<Text>().color = color;
                gameOverScoreObject.GetComponent<Text>().color = color;

                if (color.a >= 1.0f)
                {
                    // set score number
                    score += 16;
                    if (score >= scoreObject.GetComponent<UIScoreManager>().score)
                    {
                        score = scoreObject.GetComponent<UIScoreManager>().score;
                        if (scoreObject.GetComponent<UIScoreManager>().plusScore == 0 && !scoreCountStop)
                        {
                            scoreCountStop = true;
                            audioObject.GetComponent<UIAudioManager>().PlayResult();
                        }
                    }
                    string str0 = "";
                    for (int i = 0; i < 10 - score.ToString().Length; ++i)
                    {
                        str0 += "0";
                    }
                    string strScore = "SCORE\t" + str0 + score.ToString();
                    gameOverScoreObject.GetComponent<Text>().text = strScore;

                    // update ranking data
                    if (scoreCountStop && !updatedRanking)
                    {
                        updatedRanking = true;
                        int rank1 = PlayerPrefs.GetInt("Num1st", 0);
                        int rank2 = PlayerPrefs.GetInt("Num2nd", 0);
                        int rank3 = PlayerPrefs.GetInt("Num3rd", 0);
                        string str1st = PlayerPrefs.GetString("Str1st", "0000000000");
                        string str2nd = PlayerPrefs.GetString("Str2nd", "0000000000");
                        string str3rd = PlayerPrefs.GetString("Str3rd", "0000000000");
                        if (score > rank1)
                        {
                            PlayerPrefs.SetInt("Num1st", score);
                            PlayerPrefs.SetInt("Num2nd", rank1);
                            PlayerPrefs.SetInt("Num3rd", rank2);
                            str3rd = str2nd;
                            str2nd = str1st;
                            str1st = str0 + score.ToString();
                            PlayerPrefs.SetString("Str1st", str1st);
                            PlayerPrefs.SetString("Str2nd", str2nd);
                            PlayerPrefs.SetString("Str3rd", str3rd);
                            activeNewRecord = true;
                        }
                        else if (score > rank2)
                        {
                            PlayerPrefs.SetInt("Num2nd", score);
                            PlayerPrefs.SetInt("Num3rd", rank2);
                            str3rd = str2nd;
                            str2nd = str0 + score.ToString();
                            PlayerPrefs.SetString("Str2nd", str2nd);
                            PlayerPrefs.SetString("Str3rd", str3rd);
                        }
                        else if (score > rank3)
                        {
                            PlayerPrefs.SetInt("Num3rd", score);
                            str3rd = str0 + score.ToString();
                            PlayerPrefs.SetString("Str3rd", str3rd);
                        }
                        string strRanking = "1st\t" + str1st + "\n2nd\t" + str2nd + "\n3rd\t" + str3rd;
                        gameOverRankingObject.GetComponent<Text>().text = strRanking;
                    }
                }

                if (scoreCountStop)
                {
                    // update ranking alpha
                    color = gameOverRankingObject.GetComponent<Text>().color;
                    color.a += 0.1f;
                    if (color.a >= 1.0f)
                    {
                        color.a = 1.0f;
                    }
                    gameOverRankingObject.GetComponent<Text>().color = color;
                }

                if (activeNewRecord)
                {
                    // update newrecord alpha
                    color = gameOverNewRecord.GetComponent<Text>().color;
                    if (alphaDown)
                    {
                        color.a -= 0.01f;
                        if (color.a <= 0.5f)
                        {
                            color.a = 0.5f;
                            alphaDown = false;
                        }
                    }
                    else
                    {
                        color.a += 0.01f;
                        if (color.a >= 1.0f)
                        {
                            color.a = 1.0f;
                            alphaDown = true;
                        }
                    }
                    gameOverNewRecord.GetComponent<Text>().color = color;

                    // update newrecord size
                    int fontSize = gameOverNewRecord.GetComponent<Text>().fontSize;
                    if (fontSizeDown)
                    {
                        --fontSize;
                        if (fontSize <= 50)
                        {
                            fontSize = 50;
                            fontSizeDown = false;
                        }
                    }
                    else
                    {
                        ++fontSize;
                        if (fontSize >= 60)
                        {
                            fontSize = 60;
                            fontSizeDown = true;
                        }
                    }
                    gameOverNewRecord.GetComponent<Text>().fontSize = fontSize;
                }

                // update button size
                size = gameOverButtonObject.GetComponent<RectTransform>().sizeDelta;
                size.y += 10;
                if (size.y >= 100)
                {
                    size.y = 100;
                }
                gameOverButtonObject.GetComponent<RectTransform>().sizeDelta = size;
                gameOverButtonObject2.GetComponent<RectTransform>().sizeDelta = size;
            }

            // clear save data
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                PlayerPrefs.DeleteAll();
            }
        }
    }

    IEnumerator SceneChange(string nextScene)
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(nextScene);
    }

    public void OnClickReStartButton()
    {
        fadeObject.GetComponent<UIFadeManager>().fadeOut = true;
        StartCoroutine("SceneChange", "GameScene");
    }

    public void OnClickReturnToTitleButton()
    {
        fadeObject.GetComponent<UIFadeManager>().fadeOut = true;
        StartCoroutine("SceneChange", "TitleScene");
    }
}
