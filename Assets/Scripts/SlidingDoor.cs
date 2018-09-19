using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoor : MonoBehaviour {

    public bool isOpen = false;
    Renderer rend;

    // Use this for initialization
    void Start () {
        rend = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
		if (!isOpen)
        {
            // Set the main Color of the Material to red
            rend.material.SetColor("_Color", Color.red);
        } else
        {
            // Set the main Color of the Material to green
            rend.material.SetColor("_Color", Color.green);
        }
	}

    public void operateDoor()
    {
        if (isOpen)
        {
            // set the door as closed and enable collider so player can't pass
            isOpen = false;
        } else
        {
            // set the door open and disable colliders so player passes through
            isOpen = true;
        }
    }

    public void closeDoor()
    {
        isOpen = false;
    }
}
