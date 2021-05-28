////////////////////////////////////////////////////////////
// File: <StatisticTracker.cs>
// Author: <Morgan Ellis>
// Date Created: <06/01/21>
// Brief: <This file is responsible for tracker statistics needed for adjusting the difficulty>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticTracker : MonoBehaviour
{
    public float iNoOfObsticlesSpawned; // keeps track of the number of obsticles spawned
    public float iNoOfObsticlesHit; // keeps tracks of the number of obsticles hit

    public int iNoOfCoinsCollected; // keeps track of the number of coins Collected
    public int iNoOfCoinsSpawned; // keeps track of the number of coins spawnned

    public float fPrOfHittingObsticle; // stores the value of the probability of hitting an obsticle
    public float fPrOfDodgingObsticle; // stores the value of the probability of dodging an obsticle

    public float fObsticleMoveSpeed; // stores the value that the obsticles will move at

    public float fPowerUpProb; // stores the value of the probability of a power up spawning

    public int iPlayerScore; // stores the players score

    public int iPlayerHealth = 5; // stores the player health

	private void Start()
	{
        iPlayerHealth = 5; // sets the players health to 5
    }

	private void Update()
	{
        fPrOfHittingObsticle = (iNoOfObsticlesHit / iNoOfObsticlesSpawned); // calculates the probability of hitting an obsticle
        fPrOfDodgingObsticle = ((iNoOfObsticlesSpawned - iNoOfObsticlesHit) / iNoOfObsticlesSpawned); // calculates the probability of dodging an obsticle

        if(fObsticleMoveSpeed == 0)
		{
            fObsticleMoveSpeed = 25f; // sets the initial speed of the obsticles to 25
		}
		else
		{
            fObsticleMoveSpeed = (5f / fPrOfHittingObsticle) + iNoOfCoinsCollected; // calculates the obsticle move speed based on the reciple function of the probability of hitting a obsticle + the number of coins collected
        }

    }
}
