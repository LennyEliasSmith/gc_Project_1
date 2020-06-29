using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class targetScript : MonoBehaviour
{
    public Transform targetObj;
    public Rigidbody2D targetRb;
    public GameObject player;

    public float speed = 2f;
    public float distance = 2f;
    public int range = 10;

    public bool isTargetActive = false;
    public bool isTargetMovingHorizontal = false;
    public bool isTargetMovingVertical = false;
    public bool isTargetAttacking = false;

    
 



    void Start()
    {
        targetObj = GetComponent<Transform>();
        targetRb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
          
    }


    void Update()
    {
        //Boolean Functionality if statements
        if(isTargetActive == false) {PlayerInDistanceOfTarget();}

        if (isTargetMovingHorizontal && isTargetActive){TargetMovement();}

        if(isTargetMovingVertical && isTargetActive){VerticalTargetMovement();}

        if(isTargetActive && isTargetAttacking){TargetAttackPlayer();}

    }

    //Check if distance between player and target
    public void PlayerInDistanceOfTarget()
    {

        Vector3 targetPos = targetObj.position;
        Vector3 playerPos = player.transform.position;

        if (Vector3.Distance(targetPos, playerPos) < range)
        {
          ActivateTarget();

        }


    }

    
    //Rotates Target up and sets boolean to true
    public void ActivateTarget()
    {
        transform.Rotate(Vector3.right * 90 );

        isTargetActive = true;

        

    }

    //Target ping pongs on the x axis
    public void TargetMovement()
    {
      
            Debug.Log("target Move");

            targetObj.position = new Vector3(Mathf.PingPong(Time.time * speed, distance ), transform.position.y, transform.position.z);

    }

    //Target Ping Pongs on the Y Axis
    public void VerticalTargetMovement()
    {

       
            Debug.Log("target vertical Move");

            targetObj.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * speed, distance), transform.position.z);


    }

    //Target Move Towards Player
    public void TargetAttackPlayer()
    {
        Debug.Log("target Attack");
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

    }
}
