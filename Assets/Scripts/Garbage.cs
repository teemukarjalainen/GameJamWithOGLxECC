using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    //Vector3 angle = new Vector3(0, 0, 0);
    float angle = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        pos.x -= 0.1f;
        transform.position = pos;
        angle += 0.01f;
        transform.Rotate(new Vector3(0, 0, 1), angle);

        if (pos.x < -30)
        {
            Destroy(gameObject);
        }
    }
}
