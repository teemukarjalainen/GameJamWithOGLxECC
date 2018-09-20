using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    GameObject gameManager;

    public Material playerHeadMaterial;
    public Texture playerDamagedTexture;
    public Texture playerNormalTexture;
    
    public GameObject brokenDoor;

    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager");
    }

    void OnTriggerEnter(Collider col)
    {
        GameObject door = col.gameObject;

        if (door.GetComponent<SlidingDoor>().isOpen)
        {
            // DOOR PASSED
            // We passed a door so let it move on. Increment the passed doors and combo counter.
            gameManager.GetComponent<GameManager>().addPass();
            gameManager.GetComponent<GameManager>().addCombo();
            playerHeadMaterial.mainTexture = playerNormalTexture;

        } else if (!door.GetComponent<SlidingDoor>().isOpen)
        {
            // DOOR HIT
            // "Destroy" the door that we collided, clear player combo and remove 1 health
            // Destroy(col.gameObject);
            gameManager.GetComponent<GameManager>().clearCombo();
            gameManager.GetComponent<GameManager>().removeHP();
            playerHeadMaterial.mainTexture = playerDamagedTexture;

            Vector3 newPos = door.transform.position;
            Quaternion newRot = door.transform.rotation;

            Instantiate(brokenDoor, newPos, newRot);

            door.GetComponent<SlidingDoor>().breakDoor();
        }

        // Show player score in debug, will be removed and replaced with actual score board
        Debug.Log("HP: " + gameManager.GetComponent<GameManager>().hp + " Score: " + gameManager.GetComponent<GameManager>().doorsPassed + " Combo: " + gameManager.GetComponent<GameManager>().combo);
    }
}
