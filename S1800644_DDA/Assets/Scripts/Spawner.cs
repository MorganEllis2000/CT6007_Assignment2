using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] goaObsticleSpawns;

    private float fSpawnTime;
    private float fStartTimeBetweenSpawns = 4f;
    private float fDecreaseTime = 0.0f;
    private float fMinTime = 0.65f;
    private int iRandomSpawn;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(fSpawnTime <= 0)
		{
            iRandomSpawn = Random.Range(0, goaObsticleSpawns.Length);
            Instantiate(goaObsticleSpawns[iRandomSpawn], transform.position, Quaternion.identity);
            fSpawnTime = fStartTimeBetweenSpawns;
            if(fStartTimeBetweenSpawns > fMinTime)
			{
                fStartTimeBetweenSpawns -= fDecreaseTime;
			}
		}
		else
		{
            fSpawnTime -= Time.deltaTime;
		}
        
    }
}
