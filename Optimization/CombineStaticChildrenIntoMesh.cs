using UnityEngine;

/// <summary>
/// Add this script to the parent GameObject to combine all of its children into a single mesh.
/// This drastically lowers draw calls.
/// This is especially handy if you have a bunch of static prefabs that get instantiated at runtime.
///
/// Limitations:
/// - This script is costly, use it sparingly and keep in mind to have show a loading screen or something if you're combining a lot of meshes.
/// - All children need to be static and share a single material.
/// - Once they have been batched their Transform cannot be altered (which makes sense anyway since they should be static)
/// - The maximum resulting mesh can only contain 64k vertices, if it hits the limit, additional combined meshes are made.
///
/// Alternative:
/// Enable GPU instancing in the shared material: https://docs.unity3d.com/Manual/GPUInstancing.html
/// This also drastically lowers draw calls and can be used even for non-static GameObjects. It takes a bit longer and does NOT work with any sort of GI.
/// Use it for "particle" like GameObjects.
/// </summary>
public class CombineStaticChildrenIntoMesh : MonoBehaviour
{
    public CombineTime WhenToCombine;

    public enum CombineTime
    {
        Awake, Start, Manual
    }

    private void Awake()
    {
        if (WhenToCombine == CombineTime.Awake)
        {
            Combine();
        }
    }

    private void Start()
    {
        if (WhenToCombine == CombineTime.Start)
        {
            Combine();
        }
    }

    public void Combine()
    {
        StaticBatchingUtility.Combine(gameObject);
    }
}