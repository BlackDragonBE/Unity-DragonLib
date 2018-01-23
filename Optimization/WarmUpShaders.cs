using UnityEngine;

/// <summary>
/// Warm up all shaders at the start so there's no stutter when using a previously unused shader.
/// </summary>
public class WarmUpShaders : MonoBehaviour
{
    private void Start()
    {
        Shader.WarmupAllShaders();
    }
}