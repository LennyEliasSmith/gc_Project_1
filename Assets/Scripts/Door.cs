using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public float doorSpeed;
    public float doorTimer;
    public float doorWait;
    public float maxDoorOpen;

    public bool doorOpening;
    public bool doorClosing;
    public bool doorOpen;
    public bool doorClosed;

    public BoxCollider trigger;

    private Vector3 closedPos;
    public Vector3 openPos;


    // Start is called before the first frame update
    void Start()
    {
        doorOpening = false;
        doorClosing = false;
        doorOpen = false;
        doorClosed = true;

        closedPos = transform.position;
        openPos = new Vector3(transform.position.x, (transform.position.y + 5), transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {

        if (doorOpening)
            OpenDoor();

        if (doorOpen)
            doorTimer = doorTimer + Time.deltaTime;

        if (doorTimer >= doorWait)
            doorClosing = true;

        if (doorClosing)
            CloseDoor();

    }

    void OpenDoor()
    {
        // Debug.Log("Door opening");
        if(transform.position.y < openPos.y)
        {
            transform.position = new Vector3(transform.position.x, (transform.position.y + (Time.deltaTime * doorSpeed)), transform.position.z);
            doorClosed = false;
        }
        else if (transform.position.y >= openPos.y)
        {
            doorOpening = false;
            doorOpen = true;
        }
    }

    void CloseDoor()
    {
        // Debug.Log("Door closing");
        if (transform.position.y > closedPos.y)
        {
            transform.position = new Vector3(transform.position.x, (transform.position.y - (Time.deltaTime * doorSpeed)), transform.position.z);
            doorClosing = true;
            doorOpen = false;
        }
        else if (transform.position.y <= closedPos.y)
        {
            doorTimer = 0;
            doorClosing = false;
            doorClosed = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            doorTimer = 0;
            doorOpening = true;
            doorClosing = false;
        }
    }

}
