using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private int iPlayerPosition = 1;
    private int iMoveDistance = 5;

    private Vector3 v3TargetPos;

    private float fXIncrement = 5f;
    private float fMoveSpeed = 50f;
    private float fMaxWidth = 5f;
    private float fMinWidth = -5f;

    /// <summary>
    /// Jump variables
    /// </summary>
    private bool bIsGrounded;
    private float fJumpHeight = 10f;
    private Rigidbody playerRigidbody;
    private BoxCollider playerCollider;
    private Vector3 v3JumpVector;
    public LayerMask groundLayers;

    private CharacterController characterController;
    private float fVerticalVelocity;
    private float fGravity = 14f;


    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerCollider = GetComponent<BoxCollider>();
        v3JumpVector = new Vector3(0, 7.0f, 0);
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {       
   
        if (Input.GetKeyDown(KeyCode.D) && transform.position.x < fMaxWidth)
        {
            v3TargetPos = new Vector3(transform.position.x + fXIncrement, transform.position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, fMoveSpeed * Time.deltaTime);
            characterController.enabled = false;
            characterController.transform.position = v3TargetPos;
            characterController.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.A) && transform.position.x > fMinWidth)
        {
            v3TargetPos = new Vector3(transform.position.x - fXIncrement, transform.position.y, transform.position.z);
            //transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, fMoveSpeed * Time.deltaTime);
            characterController.enabled = false;
            characterController.transform.position = v3TargetPos;
            characterController.enabled = true;
        }

        
        if (characterController.isGrounded)
        {
            fVerticalVelocity = -fGravity * Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                fVerticalVelocity = fJumpHeight;
            }
        }
        else
        {
            fVerticalVelocity -= fGravity * Time.deltaTime;
        }

        Vector3 v3MoveVector = new Vector3(0, fVerticalVelocity, 0);
        characterController.Move(v3MoveVector * Time.deltaTime);
        
    }
}
