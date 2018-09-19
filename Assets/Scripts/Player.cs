using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    GameObject gameManager;

	// Use this for initialization
	void Start () {
        gameManager = GameObject.Find("GameManager");

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider col)
    {
        GameObject door = col.gameObject;

        if (door.GetComponent<SlidingDoor>().isOpen)
        {
            // DOOR PASSED
            gameManager.GetComponent<GameManager>().addPass();
            gameManager.GetComponent<GameManager>().addCombo();

            // Debug.Log("Passed : " + col.gameObject.name);
            Debug.Log("Score: " + gameManager.GetComponent<GameManager>().doorsPassed + " Combo: " + gameManager.GetComponent<GameManager>().combo);
        } else if (!door.GetComponent<SlidingDoor>().isOpen)
        {
            // DOOR HIT
            // Do we want to destroy doors if we hit them?
            // Destroy(col.gameObject);
            gameManager.GetComponent<GameManager>().clearCombo();
            Debug.Log("Crashed to: " + col.gameObject.name);
        }
    }
}
