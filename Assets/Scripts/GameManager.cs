﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    Camera mainCamera;
    GameObject clickedObject;
    public GameObject spawnerArea;
    GameObject[] doors;
    GameObject player;
    GameObject playerTarget;

    public Material playerBodyMaterial;
    public Material playerHeadMaterial;
    public Texture gameOverPlayerHead;
    public Texture gameOverPlayerBody;

    public Texture playerHeadNormal;
    public Texture playerBodyNormal;

    private float startCountDown = 3;
    private bool gameStarted = false;
    private int passesBetweenLevels = 3;

    public int hp;
    int currentLevel = 0;
    public int doorsPassed { get; set; }
    public int combo { get; set; }

    public bool isGameOver = false;

    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        mainCamera.enabled = true;

        playerTarget = GameObject.Find("PlayerTargetPosition");
        player = GameObject.Find("PlayerCharacter");

        playerHeadMaterial.mainTexture = playerHeadNormal;
        playerBodyMaterial.mainTexture = playerBodyNormal;

        SceneManager.LoadScene("MainScene", LoadSceneMode.Additive);
    }
	
	// Update is called once per frame
	void Update () {

        startCountDown -= Time.deltaTime;

        if (startCountDown <= 0 && gameStarted == false)
        {
            spawnerArea.SetActive(true);
            playerTarget.SetActive(false);
            gameStarted = true;
        } else if (gameStarted == false)
        {
            // Move the player to the view while game is counting down.
            float speed = Vector3.Distance(player.transform.position, playerTarget.transform.position) / startCountDown;
            float step = speed * Time.deltaTime;
            player.transform.position = Vector3.MoveTowards(player.transform.position, playerTarget.transform.position, step);
        }

        if (Input.GetMouseButtonDown(0))
        { // if left button pressed...
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // the object identified by hit.transform was clicked
                clickedObject = hit.transform.gameObject;
                if (clickedObject.tag == "SlidingDoor")
                {
                    clickedObject.GetComponent<SlidingDoor>().operateDoor();
                }

                foreach (GameObject door in doors)
                {
                    if (door != null) {
                        if (clickedObject.name != door.name)
                        {
                            door.GetComponent<SlidingDoor>().closeDoor();
                        }
                    }
                }
            } 
        }
    }

    public void updateDoorsList()
    {
        doors = GameObject.FindGameObjectsWithTag("SlidingDoor");
    }

    public void removeHP()
    {
        hp--;

        if (hp == 0)
        {
            gameOver();
        }
    }

    public void gameOver()
    {
        spawnerArea.SetActive(false);

        playerHeadMaterial.mainTexture = gameOverPlayerHead;
        playerBodyMaterial.mainTexture = gameOverPlayerBody;

        isGameOver = true;
    }

    public void clearCombo()
    {
        combo = 0;
    }

    public void addCombo()
    {
        combo++;
    }

    public void addPass()
    {
        doorsPassed++;

        if (doorsPassed % passesBetweenLevels == 0)
        {
            updateLevel();
        }
    }

    void updateLevel()
    {
        currentLevel++;
        spawnerArea.GetComponent<SpawnerArea>().changeMaxInterval();
        spawnerArea.GetComponent<SpawnerArea>().changeMinInterval();
    }
}
