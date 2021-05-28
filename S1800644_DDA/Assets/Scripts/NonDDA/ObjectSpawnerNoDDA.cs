////////////////////////////////////////////////////////////
// File: <ObjectSpawnerNoDDA.cs>
// Author: <Morgan Ellis>
// Date Created: <27/01/21>
// Brief: <This file is responsible for spawning the objest in the non DDA scene>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerNoDDA : MonoBehaviour
{
    public GameObject[] go_aSpawnableObjects; // game object array of the different spawnable objects
    public Transform[] tf_aSpawnPoints; // transform array of the spawn points

    public float fTimeBtwSpawn = 3f; // determines the ammount between object spawns

    // Update is called once per frame
    void Update()
    {
        fTimeBtwSpawn -= Time.deltaTime; // starts the countdown of the spawn
        if (fTimeBtwSpawn < 0f)
        {
            int numSpawns = Random.Range(3, tf_aSpawnPoints.Length); // at least 3 object will be spawned each time

            List<Transform> freeSpawnPoints = new List<Transform>(tf_aSpawnPoints); // list of transform that tracks if a spawn point is already occupied

            for (int i = 0; i < numSpawns; ++i)
			{
                if (freeSpawnPoints.Count <= 0)
                {
                    return;
                }

                int iRandSpawnObject = Random.Range(0, go_aSpawnableObjects.Length); // chooses a random object to spawn
                int iRandSpawnPoint = Random.Range(0, freeSpawnPoints.Count); // chooses a random spawn point for this object

                Transform spawnLocation = freeSpawnPoints[iRandSpawnPoint]; // adds this spawn location to the transform list of freespawnpoints
                Debug.Log("Spawn");
                freeSpawnPoints.RemoveAt(iRandSpawnPoint); // removes this from the spawn location list
                Instantiate(go_aSpawnableObjects[iRandSpawnObject], spawnLocation.position, Quaternion.identity); // instantiate the object
            }
            fTimeBtwSpawn = 3f; // reset the timer
        }

    }
}
