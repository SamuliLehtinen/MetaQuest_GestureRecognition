using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to change the color of a game object when the user has the left hand in a fist state
/// and the palm is facing up or down, but when nothing is detected, the color is set to normal
/// </summary>
public class FistRotationLeft_Color : MonoBehaviour
{
    [SerializeField]
    public Material colorNormal;
    [SerializeField]
    public Material colorPalmUp;
    [SerializeField]
    public Material colorPalmDown;

    RotationDetectorSingle rotationDetector;

    GameObject leftHand;
    
    private Renderer renderer;

    private void Awake()
    {
        // Get the renderer component
        renderer = GetComponent<Renderer>();
    }

    void Start()
    {
        leftHand = GameObject.FindGameObjectWithTag("LeftHand_Prefab");

        // Find the PoseDetector script and subscribe to its events
        rotationDetector = leftHand.GetComponent<RotationDetectorSingle>();
        if (rotationDetector != null)
        {
            // Subscribe to the FistClosed event
            rotationDetector.palmUp_Fist += OnPalmUp_Fist;
            rotationDetector.palmDown_Fist += OnPalmDown_Fist;
            rotationDetector.palmNormal_Fist += OnpalmNormal_Fist;
        }
    }

    private void OnPalmUp_Fist()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorPalmUp;
        }
    }

    private void OnPalmDown_Fist()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorPalmDown;
        }
    }

    private void OnpalmNormal_Fist()
    {
        // Check if there's a renderer attached to the object
        if (renderer != null)
        {
            // Change the material
            renderer.material = colorNormal;
        }
    }
}
