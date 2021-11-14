////////////////////////////////////////////////////////////
// File: <Obsticle.cs>
// Author: <Morgan Ellis>
// Date Created: <27/01/21>
// Brief: <This file controls the movement and collisions of objects that are being used in the scene>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticle : MonoBehaviour
{
	StatisticTracker statisticTracker;
	public float fObsticleMoveSpeed; // float the tracks the speed that obsticles move at
	PlayerController playerController;

	private void Start()
	{
		statisticTracker = GameObject.Find("StatisticTracker").GetComponent<StatisticTracker>();
		playerController = GameObject.Find("Player").GetComponent<PlayerController>();
		fObsticleMoveSpeed = statisticTracker.fObsticleMoveSpeed;

		if (fObsticleMoveSpeed == 0)
		{
			fObsticleMoveSpeed = 25f;
		} else if (fObsticleMoveSpeed < 15f)
		{
			fObsticleMoveSpeed = 15f;
		} else if (fObsticleMoveSpeed > 100f)
		{
			fObsticleMoveSpeed = 100f;
		}
	}

	void Update()
    {
		// this is responsible for roating the heart and moving it towards the player
		if (gameObject.tag == "Heart" || gameObject.tag == "Coin")
		{
			gameObject.transform.rotation = Quaternion.Euler(-90.0f, 0f, 0f);
			transform.Translate(Vector3.up * fObsticleMoveSpeed * Time.deltaTime);
		}
		else
		{
			transform.Translate(Vector3.back * fObsticleMoveSpeed * Time.deltaTime);
		}
    }

	private void OnTriggerEnter(Collider other)
	{
		// if the obsticle doesn't collide with the player that the speed goes up by one and so does the score
		if (other.CompareTag("Collider"))
		{
			statisticTracker.fPowerUpProb = 0;
			statisticTracker.iPlayerScore += 1;
			Destroy(gameObject);
		} else if (other.CompareTag("Object")) // if the player hits an obsticle then the speed of the obsticles is decreased by 10
		{
			Destroy(gameObject);
		}
	}
}
