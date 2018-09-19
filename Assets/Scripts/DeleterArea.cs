using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleterArea : MonoBehaviour {

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "SlidingDoor")
        {
            Destroy(col.gameObject);
        }
    }
}
