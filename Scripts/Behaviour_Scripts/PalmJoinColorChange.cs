using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script is used to change the color of a game object when the user mimics the reading of a book with both hands
/// </summary>
public class PalmJoinColorChange : MonoBehaviour
{
    [SerializeField]
    public Material colorPalmJoin;
    [SerializeField]
    public Material colorPalmNoJoin;

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
        colorChanger.material = colorPalmNoJoin;
        // Find the PoseDetector script and subscribe to its events
        poseDetector = FindObjectOfType<PoseDetectorDouble>();
        
        if (poseDetector != null)
        {
            // Subscribe to the FistClosed event
            poseDetector.PalmJoin += OnPalmJoin;
            poseDetector.PalmNoJoin += OnPalmNoJoin;
        }
        
    }

    private void OnPalmJoin()
    {
        // Check if there's a renderer attached to the object
        if (colorChanger != null)
        {
            // Change the material
           colorChanger.material = colorPalmJoin;
        }
    }

    private void OnPalmNoJoin()
    {
        // Check if there's a renderer attached to the object
        if (colorChanger != null)
        {
            // Change the material
            colorChanger.material = colorPalmNoJoin;
        }
    }
}
