using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 v3TargetPos;

    private float fXIncrement = 5f;
    private float fMoveSpeed = 50f;
    private float fMaxWidth = 5f;
    private float fMinWidth = -5f;

    // Update is called once per frame
    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, v3TargetPos, fMoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.D) && transform.position.x < fMaxWidth)
        {
            v3TargetPos = new Vector3(transform.position.x + fXIncrement, transform.position.y, transform.position.z); ;
        }else if (Input.GetKeyDown(KeyCode.A) && transform.position.x > fMinWidth)
        {
            v3TargetPos = new Vector3(transform.position.x - fXIncrement, transform.position.y, transform.position.z);
        }
    }
}
