using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAudioManager : MonoBehaviour
{
    public AudioClip click;
    public AudioClip score;
    public AudioClip gameOver;
    public AudioClip damage;
    public AudioClip result;
    public AudioClip open;

    AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayClick()
    {
        audioSource.clip = click;
        audioSource.Play();
    }

    public void PlayScore()
    {
        audioSource.clip = score;
        audioSource.Play();
    }

    public void PlayGameOver()
    {
        audioSource.clip = gameOver;
        audioSource.Play();
    }

    public void PlayDamage()
    {
        audioSource.clip = damage;
        audioSource.Play();
    }

    public void PlayResult()
    {
        audioSource.clip = result;
        audioSource.Play();
    }

    public void PlayOpen()
    {
        audioSource.clip = open;
        audioSource.Play();
    }
}
