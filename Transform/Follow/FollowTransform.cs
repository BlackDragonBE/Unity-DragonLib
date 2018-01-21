// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using UnityEngine;
using System.Collections;

public class FollowTransform : MonoBehaviour
{
    //////////////////////////////////////////////////////////////
    // FollowTransform.js
    // Penelope iPhone Tutorial
    //
    // FollowTransform will follow any assigned Transform and 
    // optionally face the forward vector to match for the Transform
    // where this script is attached.
    //////////////////////////////////////////////////////////////



    public Transform targetTransform;		// Transform to follow
    public bool faceForward = false;		// Match forward vector?
    private Transform thisTransform;

    void Start()
    {
        // Cache component lookup at startup instead of doing this every frame
        thisTransform = transform;
    }

    void Update()
    {
        thisTransform.position = targetTransform.position;

        if (faceForward)
            thisTransform.forward = targetTransform.forward;
    }
}