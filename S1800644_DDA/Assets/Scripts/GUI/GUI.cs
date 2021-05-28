////////////////////////////////////////////////////////////
// File: <GUI.cs>
// Author: <Morgan Ellis>
// Date Created: <25/01/21>
// Brief: <This file is responsible for displaying statistics to the player to see how the DDA is performing>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUI : MonoBehaviour
{
    StatisticTracker statisticTracker;

    private Text NoOfObsticlesSpawned;
    private Text NoOfObsticlesHit;
    private Text PrHittingAnObsticle;
    private Text PrDodgingAnObsticle;
    private Text ObsticleMoveSpeed;
    private Text NoOfCoinsSpawned;
    private Text NoOfCoinsCollected;
    private Text ProbOfPowerUp;
    private Text PlayerHealth;
    private Text PlayerScore;
    

    void Start()
    {
        statisticTracker = GameObject.Find("StatisticTracker").GetComponent<StatisticTracker>();

        NoOfObsticlesSpawned = GameObject.Find("NoOfObsticlesSpawned").GetComponent<Text>();
        NoOfObsticlesHit = GameObject.Find("NoOfObsticlesHit").GetComponent<Text>();
        PrHittingAnObsticle = GameObject.Find("PrHittingAnObsticle").GetComponent<Text>();
        PrDodgingAnObsticle = GameObject.Find("PrDodgingAnObsticle").GetComponent<Text>();
        ObsticleMoveSpeed = GameObject.Find("ObsticleMoveSpeed").GetComponent<Text>();
        NoOfCoinsSpawned = GameObject.Find("NoOfCoinsSpawned").GetComponent<Text>();
        NoOfCoinsCollected = GameObject.Find("NoOfCoinsCollected").GetComponent<Text>();
        ProbOfPowerUp = GameObject.Find("ProbOfPowerUp").GetComponent<Text>();
        PlayerHealth = GameObject.Find("PlayerHealth").GetComponent<Text>();
        PlayerScore = GameObject.Find("PlayerScore").GetComponent<Text>();
    }

    void Update()
    {
        NoOfObsticlesSpawned.text = "Total Obsticles: " + statisticTracker.iNoOfObsticlesSpawned;
        NoOfObsticlesHit.text = "Total Obsticles Hit: " + statisticTracker.iNoOfObsticlesHit;
        PrHittingAnObsticle.text = "Pr Hitting Obsticle: " + statisticTracker.fPrOfHittingObsticle;
        PrDodgingAnObsticle.text = "Pr Dodging Obsticle: " + statisticTracker.fPrOfDodgingObsticle;
        ObsticleMoveSpeed.text = "Obsticle Move Speed: " + statisticTracker.fObsticleMoveSpeed;
        NoOfCoinsSpawned.text = "Total Coins: " + statisticTracker.iNoOfCoinsSpawned;
        NoOfCoinsCollected.text = "Total Coins Collected: " + statisticTracker.iNoOfCoinsCollected;
        ProbOfPowerUp.text = "Power Up Probability: " + statisticTracker.fPowerUpProb;
        PlayerHealth.text = "" + statisticTracker.iPlayerHealth;
        PlayerScore.text = "Score: " + statisticTracker.iPlayerScore;
    }
}
