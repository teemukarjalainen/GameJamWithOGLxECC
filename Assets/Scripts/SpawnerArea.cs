using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerArea : MonoBehaviour {

    public GameObject slidingDoor;
    GameObject spawnerArea;

    public float intervalTimer;
    float staticSpawningInterval = 1; // Spawning interval should be given in seconds
    float intervalChangeBetweenLevels = 0.3f;

    float maxInterval { get; set; }
    float minInterval { get; set; }

    public bool canSpawn;

    // Use this for initialization
    void Start () {
        spawnerArea = GameObject.Find("SpawnerArea");

        maxInterval = staticSpawningInterval + 2;
        minInterval = staticSpawningInterval;

        resetIntervalTimer();
	}
	
	// Update is called once per frame
	void Update () {
        if (canSpawn)
        {
            //createDoorsWithInterval();
            createDoorsWithRandomInterval();
        }
	}

    void createDoorsWithInterval()
    {
        intervalTimer -= Time.deltaTime;

        if (intervalTimer <= 0)
        {
            // Timer ran out, create new door
            //Debug.Log("Create door");
            Instantiate(slidingDoor, spawnerArea.transform, false);
            resetIntervalTimer();
        }
    }

    void createDoorsWithRandomInterval()
    {
        intervalTimer -= Time.deltaTime;

        if (intervalTimer <= 0)
        {
            //Debug.Log("Create door");
            Instantiate(slidingDoor, spawnerArea.transform, false);
            resetIntervalTimerRandom();
        }
    }

    void resetIntervalTimer()
    {
        intervalTimer = staticSpawningInterval;
    }

    void resetIntervalTimerRandom()
    {
        intervalTimer = Random.Range(minInterval, maxInterval);
        // Debug.Log("Randomized interval: " + intervalTimer);
    }

    public void changeMinInterval()
    {
        if (minInterval > 0.1f)
        {
            minInterval = minInterval - intervalChangeBetweenLevels;

            if (maxInterval < 0.3f)
            {
                maxInterval = 0.3f;
            }
        }
    }

    public void changeMaxInterval()
    {
        if (maxInterval > 0.3f)
        {
            maxInterval = maxInterval - intervalChangeBetweenLevels;

            if (maxInterval < 0.3f)
            {
                    maxInterval = 0.3f;
            }
        }
    }
}
