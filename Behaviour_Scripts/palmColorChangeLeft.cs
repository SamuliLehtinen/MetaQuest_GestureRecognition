using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class palmColorChangeLeft : MonoBehaviour
{
    // This script is used to change the color of a game object when the user opens the left palm

    [SerializeField]
    public Material colorPalmOpened;
    [SerializeField]
    public Material colorPalmClosed;

    PoseDetectorSingle poseDetector;

    GameObject leftHand;

    private Renderer colorChanger;

    private void Awake()
    {
        // Get the renderer component
        colorChanger = GetComponent<Renderer>();
    }

    void Start()
    {
        // Get the renderer component
        colorChanger.material = colorPalmClosed;
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");

        // Find the PoseDetector script and subscribe to its events
        poseDetector = leftHand.GetComponent<PoseDetectorSingle>();

        if (poseDetector != null)
        {
            // Subscribe to the FistClosed event
            poseDetector.PalmOpenedLeft += OnPalmOpenedLeft;
            poseDetector.PalmNotOpenedLeft += OnPalmNotOpenedLeft;
        }
    }

    private void OnPalmOpenedLeft()
    {
        // Check if there's a renderer attached to the object
        if (colorChanger != null)
        {
            // Change the material
            colorChanger.material = colorPalmOpened;
        }
    }

    private void OnPalmNotOpenedLeft()
    {
        // Check if there's a renderer attached to the object
        if (colorChanger != null)
        {
            // Change the material
            colorChanger.material = colorPalmClosed;
        }
    }
}
