using UnityEngine;
using System.Collections;

public class CameraBackgroundColorChanger : MonoBehaviour
{
    public float duration = 5.0F;
    public Color color0 = Color.yellow;
    public Color color1 = Color.red;

    private void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        GetComponent<Camera>().backgroundColor = Color.Lerp(color0, color1, t);
    }
}