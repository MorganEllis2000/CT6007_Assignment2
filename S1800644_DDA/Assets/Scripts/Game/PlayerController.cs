////////////////////////////////////////////////////////////
// File: <PlayerController.cs>
// Author: <Morgan Ellis>
// Date Created: <04/01/21>
// Brief: <This file controls the movement of the player and tracks statistics for the Non-DDA scene>
////////////////////////////////////////////////////////////

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    private Vector3 v3TargetPos; // Vector that stores the intended position that the player wants to move to

    private float fXIncrement = 5f; // the player can move left to right in increments of 5
    private float fMaxWidth = 10f; // player cannot go past 10 or -10
    private float fMinWidth = -10f;

    /// <summary>
    /// Jump variables
    /// </summary>
    private float fJumpHeight = 5f; // defines how high the player can jump

    private CharacterController characterController;
    private float fVerticalVelocity; // float used to store the vertical velocity of the player when jumping
    private float fGravity = 14f; // float that defines how fast the player will fall back to the ground

    private float fDistToGround; // used to track how far off the ground the player is and determine if they are grounded

    private int iPlayerHealth; // int that tracks the health of the player
    public float fMoveSpeed; // float the tracks the move speed of the obsticles
    public int iPlayerScore; // int that tracks the score

    /// <summary>
    /// UI components that track in game stats for the Non-DDA scene
    /// </summary>
    private Text PlayerLives;
    private Text PlayerMoveSpeed;
    private Text PlayerScore;
    public Canvas StatCanvas;
    public Canvas GameOverCanvas;

    [SerializeField]
    private bool bInvincible; // bool that makes the player invincible if true
    [SerializeField]
    private float fInvincibilityTimer = 8f; // timer to track how long the player is invincible for

    [SerializeField]
    public bool bHasCollided; // bool used to check if the player has collided with an object and stops the error of a single collision counting as 2 collisions

    StatisticTracker statisticTracker;

    private void Awake() {
        if(Time.timeScale == 0) {
            Time.timeScale = 1;
        }
    }
    void Start() {
        characterController = GetComponent<CharacterController>();
        fDistToGround = GetComponent<Collider>().bounds.extents.y;
        statisticTracker = GameObject.Find("StatisticTracker").GetComponent<StatisticTracker>();

        if (SceneManager.GetActiveScene().buildIndex == 2) {
            PlayerLives = GameObject.Find("PlayerLives").GetComponent<Text>();
            PlayerMoveSpeed = GameObject.Find("PlayerMoveSpeed").GetComponent<Text>();
            PlayerScore = GameObject.Find("PlayerScore").GetComponent<Text>();
        }

        StatCanvas = GameObject.Find("StatCanvas").GetComponent<Canvas>();
        GameOverCanvas = GameObject.Find("GameOverCanvas").GetComponent<Canvas>();

        iPlayerHealth = 5;

    }

    void Update() {
        if (fMoveSpeed == 0) {
            fMoveSpeed = 45f; // sets the initial move speed of the obsticles to 45
        }

        if (iPlayerHealth == 0 || statisticTracker.iPlayerHealth == 0) // if the players health is 0 than the game is paused
        {
            Time.timeScale = 0;
            StatCanvas.enabled = false;
            GameOverCanvas.enabled = true;
        }

        if (SceneManager.GetActiveScene().buildIndex == 2) {
            // display ui stats to the player in the non-dda scene
            PlayerLives.text = "" + statisticTracker.iPlayerHealth;
            PlayerMoveSpeed.text = "Move Speed: " + fMoveSpeed;
            PlayerScore.text = "Score: " + iPlayerScore;
        }

        // moves the player left or right based on the input, D moves right, A moves left
        if (Input.GetKeyDown(KeyCode.D) && transform.position.x < fMaxWidth && IsGrounded() == true) {
            v3TargetPos = new Vector3(transform.position.x + fXIncrement, transform.position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, fMoveSpeed * Time.deltaTime);
            characterController.enabled = false;
            characterController.transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, 5f);
            characterController.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > fMinWidth && IsGrounded() == true) {
            v3TargetPos = new Vector3(transform.position.x - fXIncrement, transform.position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, fMoveSpeed * Time.deltaTime);
            characterController.enabled = false;
            characterController.transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, 5f);
            characterController.enabled = true;
        }

        // Ground check that allows the player to up aslong as the distance to the ground is < 0.1f
        if (IsGrounded()) {
            fVerticalVelocity = -fGravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space)) {
                fVerticalVelocity = fJumpHeight;

            }
        } else {
            fVerticalVelocity -= fGravity * Time.deltaTime;

        }

        Vector3 v3MoveVector = new Vector3(0, fVerticalVelocity, 0);
        characterController.Move(v3MoveVector * Time.deltaTime);

        if (bInvincible == true) {
            fInvincibilityTimer -= Time.deltaTime;

            if (fInvincibilityTimer < 0f) {
                bInvincible = false;
                fInvincibilityTimer += 8f;
                Debug.Log("On");
                gameObject.GetComponent<BoxCollider>().enabled = true;
                gameObject.GetComponent<Renderer>().material.color = Color.white;
            } else {
                Debug.Log("OFF");
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponent<Renderer>().material.color = Color.yellow;
            }
        }
    }

    // our ground check that sees if the player is more than 0.1f off the floor
    private bool IsGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, fDistToGround + 0.1f);
    }

    private void OnTriggerEnter(Collider other) {
        // if the player collides with a coin than the appriopriate stats are updated
        if (other.CompareTag("Coin")) {
            if (bHasCollided == false) {
                bHasCollided = true;
                iPlayerScore += 1;
                statisticTracker.iNoOfCoinsCollected = statisticTracker.iNoOfCoinsCollected + 1;
                statisticTracker.fObsticleMoveSpeed = statisticTracker.fObsticleMoveSpeed + 3;
                Destroy(other.gameObject);
            }
        }
        // if the player collides with a obsticle than the appriopriate stats are updated
        else if (other.CompareTag("Object")) {
            if (bHasCollided == false && bInvincible == false) {
                bHasCollided = true;
                statisticTracker.iNoOfObsticlesHit = statisticTracker.iNoOfObsticlesHit + 1;
                statisticTracker.iPlayerHealth = statisticTracker.iPlayerHealth - 1;
                iPlayerHealth -= 1;
                fMoveSpeed -= 10f;
                Destroy(other.gameObject);
            }
        }
        // if the player collides with a heart than the appriopriate stats are updated
        else if (other.CompareTag("Heart")) {
            if (bHasCollided == false && statisticTracker.iPlayerHealth < 5 ) {
                bHasCollided = true;
                statisticTracker.iPlayerHealth = statisticTracker.iPlayerHealth + 1;
                iPlayerHealth++;
                Destroy(other.gameObject);
            }
        }
        // if the player collides with a star than the appriopriate stats are updated
        else if (other.CompareTag("Star")) {
            if (bHasCollided == false) {
                bHasCollided = true;
                Destroy(other.gameObject);
                bInvincible = true;
            }
        }

    }

    private void LateUpdate() {
        bHasCollided = false;
    }
}