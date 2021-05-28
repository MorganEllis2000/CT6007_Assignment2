////////////////////////////////////////////////////////////
// File: <ObjectSpawner.cs>
// Author: <Morgan Ellis>
// Date Created: <05/01/21>
// Brief: <This file is responsible for spawning the objest in the non DDA scene>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] go_aSpawnableObjects; // game object array of the different spawnable objects
    public Transform[] tf_aSpawnPoints; // transform array of the spawn points
    public float fCooldownTimer = 1.75f; // determines the ammount between object spawns
    private int iNumOfSpawns; // int that tracks the number of spawns needed

    StatisticTracker statisticTracker;
    // Start is called before the first frame update
    void Start()
    {
        statisticTracker = GameObject.Find("StatisticTracker").GetComponent<StatisticTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        fCooldownTimer -= Time.deltaTime;// starts the countdown of the spawn
        if (fCooldownTimer < 0f )
		{
            
            if (statisticTracker.fPrOfHittingObsticle < 0.1f) { // if the probability of hitting an obsticle is less than 0.1 than an obsticle is spawned in each lane
                iNumOfSpawns = 5;
            } else if(statisticTracker.fPrOfHittingObsticle < 0.1f && statisticTracker.fPrOfHittingObsticle > 0.18f) { // if the probability of hitting an obsticle is between 0.1 and0.18 than an obsticle is spawned in 3 lanes
                iNumOfSpawns = 3;
            } else { 
                iNumOfSpawns = Random.Range(2, tf_aSpawnPoints.Length);
            }

            List<Transform> freeSpawnPoints = new List<Transform>(tf_aSpawnPoints); // list of transform that tracks if a spawn point is already occupied

            for (int i = 0; i < iNumOfSpawns; ++i)
            {

                statisticTracker.fPowerUpProb = Random.value; // generates a random value between 0 and 1 that is used as a probability factor for an objects spawn
                int iRandSpawnObject = Random.Range(0, 3); // chooses a random object from the non-powerup obsticles

                if (freeSpawnPoints.Count <= 0)
				{
                    return;
				}

                int iRandSpawnPoint = Random.Range(0, freeSpawnPoints.Count); // chooses a random object to spawn
                Transform spawnLocation = freeSpawnPoints[iRandSpawnPoint]; // adds this spawn location to the transform list of freespawnpoints
                freeSpawnPoints.RemoveAt(iRandSpawnPoint);

                if (statisticTracker.fPowerUpProb > 0.95 && statisticTracker.iPlayerHealth < 5) // if fPowerUpProb is greater than 0.95 and player has less than 5 lives instantiate a heart
                {
                    Instantiate(go_aSpawnableObjects[3], spawnLocation.position, Quaternion.identity);

                    //Debug.Log(go_aSpawnableObjects[3].name + " " + statisticTracker.fPowerUpProb);
                }
                else if (statisticTracker.fPowerUpProb > 0.7 && statisticTracker.iPlayerHealth == 1 && statisticTracker.fPrOfHittingObsticle > 0.15f) 
                {
                    Instantiate(go_aSpawnableObjects[3], spawnLocation.position, Quaternion.identity);
                }
                else if (statisticTracker.fPowerUpProb > 0.9 && statisticTracker.iPlayerHealth <= 5)
                {
                    Instantiate(go_aSpawnableObjects[3], spawnLocation.position, Quaternion.identity);
                    go_aSpawnableObjects[3].transform.Rotate(90, 0, 0);
                    //Debug.Log(go_aSpawnableObjects[3].name + " " + statisticTracker.fPowerUpProb);
                }
                else if (statisticTracker.fPowerUpProb > 0.6 && statisticTracker.fPrOfHittingObsticle <= 0.18f && statisticTracker.fPrOfHittingObsticle >= 0.12f)
				{
                    Instantiate(go_aSpawnableObjects[4], spawnLocation.position, Quaternion.identity);
                }
                else if (statisticTracker.fPowerUpProb > 0.97)
                {
                    Instantiate(go_aSpawnableObjects[4], spawnLocation.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(go_aSpawnableObjects[iRandSpawnObject], spawnLocation.position, Quaternion.identity);
                }

                if (go_aSpawnableObjects[iRandSpawnObject].tag == "Coin")
                {
                    statisticTracker.iNoOfCoinsSpawned += 1;
                }
            }
            fCooldownTimer = 1.75f;
            statisticTracker.iNoOfObsticlesSpawned += 1;
        }
    }
}