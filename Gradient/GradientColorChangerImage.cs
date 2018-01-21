using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GradientColorChangerImage : MonoBehaviour
{
    //References

    //Public
    public Gradient CustomGradient;
    public float FullCycleTime = 1f;
    public float StartTime = 0.0f;

    //Private
    private Image _image;
    private float _time;

    void Start()
    {
        _image = GetComponent<Image>();
        _time = StartTime;
    }

    void Update()
    {
        _image.color = CustomGradient.Evaluate(_time);
        _time += Time.deltaTime/FullCycleTime;

        if (_time > 1.0f)
        {
            _time = 0f;
        }
    }
}
