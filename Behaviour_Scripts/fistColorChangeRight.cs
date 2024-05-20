using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fistColorChangeRight : MonoBehaviour
{
    // This script is used to change the color of a game object when the user closes the left hand
    [SerializeField]
    public Material colorFistClosed;
    [SerializeField]
    public Material colorFistOpened;

    PoseDetectorSingle poseDetector;
    GameObject rightHand;
    private Renderer renderer;

    private void Awake()
    {
        // Get the renderer component
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        // Get the renderer component
        renderer.material = colorFistOpened;
        rightHand = GameObject.FindGameObjectWithTag("RightHand_Prefab");

        // Find the PoseDetector script and subscribe to its events
        poseDetector = rightHand.GetComponent<PoseDetectorSingle>();
        if (poseDetector != null)
        {
            // Subscribe to the FistClosed event
            poseDetector.FistClosedRight += OnFistClosedRight;
            poseDetector.FistNotClosedRight += OnFistNotClosedRight;
        }
    }

    private void OnFistClosedRight()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorFistClosed;
        }
    }

    private void OnFistNotClosedRight()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorFistOpened;
        }
    }
}
