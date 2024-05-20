using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistBumpColorChange : MonoBehaviour
{
    //This script is used to change the color of a game object when the user performs a fist bump gesture
    // Used in the project to demonstrate the fist bump gesture detections

    [SerializeField]
    public Material colorFistBump;
    [SerializeField]
    public Material colorFistNoBump;

    PoseDetectorDouble poseDetector;

    private Renderer colorChanger;

    private void Awake()
    {
        // Get the renderer component
        colorChanger = GetComponent<Renderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get the renderer component
        colorChanger.material = colorFistNoBump;
        // Find the PoseDetector script and subscribe to its events
        poseDetector = FindObjectOfType<PoseDetectorDouble>();
        if (poseDetector != null)
        {
            // Subscribe to the FistClosed event
            poseDetector.FistBump += OnFistBump;
            poseDetector.FistNoBump += OnFistNoBump;
            //Debug.Log("PA FistColorChange : subscription done.");
        }
        
    }

    private void OnFistBump()
    {
        // Check if there's a renderer attached to the object
        if (colorChanger != null)
        {
            // Change the material
           colorChanger.material = colorFistBump;
        }
    }

    private void OnFistNoBump()
    {
        // Check if there's a renderer attached to the object
        if (colorChanger != null)
        {
            // Change the material
            colorChanger.material = colorFistNoBump;
        }
    }
}
