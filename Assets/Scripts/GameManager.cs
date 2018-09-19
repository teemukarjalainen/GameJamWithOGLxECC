using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    Camera mainCamera;
    GameObject clickedObject;
    public GameObject spawnerArea;
    GameObject[] doors;

    private float startCountDown = 3;
    private bool gameStarted = false;
    private int passesBetweenLevels = 5;

    public int hp;
    int currentLevel = 0;
    public int doorsPassed { get; set; }
    public int combo { get; set; }


    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
        mainCamera.enabled = true;
    }
	
	// Update is called once per frame
	void Update () {

        startCountDown -= Time.deltaTime;

        if (startCountDown <= 0 && gameStarted == false)
        {
            spawnerArea.SetActive(true);
            gameStarted = true;
        }

        doors = GameObject.FindGameObjectsWithTag("SlidingDoor");

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
                    if(clickedObject.name != door.name)
                    {
                        door.GetComponent<SlidingDoor>().closeDoor();
                    }
                }
            } 
        }
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
        // TODO: Implement game over feature
        spawnerArea.SetActive(false);
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
