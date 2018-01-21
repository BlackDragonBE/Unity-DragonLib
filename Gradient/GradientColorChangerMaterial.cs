using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class GradientColorChangerMaterial : MonoBehaviour
{
    //References

    //Public
    public Gradient CustomGradient;
    public float FullCycleTime = 1f;
    public float StartTime = 0.0f;

    //Private
    private Material _material;
    private float _time;

    void Start()
    {
        _material = GetComponent<Renderer>().material;
        _time = StartTime;
    }

    void Update()
    {
        _material.color = CustomGradient.Evaluate(_time);
        _time += Time.deltaTime/FullCycleTime;

        if (_time > 1.0f)
        {
            _time = 0f;
        }
    }
}
