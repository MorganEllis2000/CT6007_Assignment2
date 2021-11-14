////////////////////////////////////////////////////////////
// File: <ObsticleNonDDA.cs>
// Author: <Morgan Ellis>
// Date Created: <27/01/21>
// Brief: <This file controls the movement and collisions of objects that are being used in the non DDA version of the scene>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObsticleNonDDA : MonoBehaviour
{
    private float fMoveSpeed; // float the tracks the speed that obsticles move at

    PlayerController playerController; // Gets the controller script 

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        fMoveSpeed = playerController.fMoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // this is responsible for roating the heart and moving it towards the player
        if (gameObject.tag == "Heart" || gameObject.tag == "Coin")
        {
            gameObject.transform.rotation = Quaternion.Euler(-90.0f, 0f, 0f);
            transform.Translate(Vector3.up * fMoveSpeed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.back * fMoveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // if the obsticle doesn't collide with the player that the speed goes up by one and so does the score
        if (other.CompareTag("Collider"))
        {
            playerController.fMoveSpeed++;
            playerController.iPlayerScore++;
            Destroy(gameObject);
        }
        else if (other.CompareTag("Object"))
        {
            playerController.fMoveSpeed -= 10f; // if the player hits an obsticle then the speed of the obsticles is decreased by 10
            Destroy(gameObject);
        }
    }
}
