using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is an attempt to move a game object with a tray
/// </summary>
public class Move_with_Tray : MonoBehaviour
{
    private bool startMovement = false;
    private bool movableObject = false;

    private bool touchesTable = false;

    private Vector3 trayStartPosition;


    private float xdif, ydif, zdif;

    GameObject theTray;


    // Start is called before the first frame update
    void Start()
    {
        theTray = GameObject.FindGameObjectWithTag("Tray");
    }

    // Update is called once per frame
    void Update()
    {
        if (startMovement)
        {
            trayStartPosition = theTray.transform.position;
            startMovement = false;
            movableObject = true;
        }


        if (movableObject)
        {
            //retrieve the change of the hand position 
            Vector3 trayPosition = theTray.transform.position;
            //translate the change of the position of the hand to the position of the cube
            xdif = trayPosition.x - trayStartPosition.x;
            ydif = trayPosition.y - trayStartPosition.y;
            zdif = trayPosition.z - trayStartPosition.z;

            Vector3 objOriginalPosition = transform.position;

            //update the position of the cube only on the x and z axis
            transform.position = new Vector3(objOriginalPosition.x + xdif, objOriginalPosition.y + 0.001f, objOriginalPosition.z + zdif);
        }


    }

    // This method is called when the collider of this object collides with another collider
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves the other object's collider
        if (collision.collider.CompareTag("Tray"))
        {   
            // TO do : understand how to corrfectly use this method
            GetComponent<Rigidbody>().useGravity = false;
            startMovement = true;
        }
    }


    // This method is called when the collider of this object collides with another collider
    void OnCollisionExit(Collision collision)
    {
        // Check if the collision involves the other object's collider
        if (collision.collider.CompareTag("Tray"))
        {
            startMovement = false;
            movableObject = false;
            GetComponent<Rigidbody>().useGravity = true;
        }
       
    }
}
