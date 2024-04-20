using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("PA PalmColorChange : subscription done.");
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


    // Update is called once per frame
    void Update()
    {
        
    }
}
