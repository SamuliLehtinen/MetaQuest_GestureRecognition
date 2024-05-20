using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This script is used to change the color of a game object when the user closes the left hand
/// </summary>
public class fistColorChangeLeft : MonoBehaviour
{
    [SerializeField]
    public Material colorFistClosed;
    [SerializeField]
    public Material colorFistOpened;

    PoseDetectorSingle poseDetector;
    GameObject leftHand;
    private Renderer renderer;

    private void Awake()
    {
        // Get the renderer component
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        renderer.material = colorFistOpened;
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");
        // Find the PoseDetector script and subscribe to its events
        poseDetector = leftHand.GetComponent<PoseDetectorSingle>();
        if (poseDetector != null)
        {
            // Subscribe to the FistClosed event
            poseDetector.FistClosedLeft += OnFistClosedLeft;
            poseDetector.FistNotClosedLeft += OnFistNotClosedLeft;
        }
    }

    private void OnFistClosedLeft()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorFistClosed;
        }
    }

    private void OnFistNotClosedLeft()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorFistOpened;
        }
    }
}
