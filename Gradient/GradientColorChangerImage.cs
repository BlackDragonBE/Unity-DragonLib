using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GradientColorChangerImage : MonoBehaviour
{
    public Gradient CustomGradient;
    public float FullCycleTime = 1f;
    public float StartTime = 0.0f;

    private Image _image;
    private float _time;

    private void Start()
    {
        _image = GetComponent<Image>();
        _time = StartTime;
    }

    private void Update()
    {
        _image.color = CustomGradient.Evaluate(_time);
        _time += Time.deltaTime / FullCycleTime;

        if (_time > 1.0f)
        {
            _time = 0f;
        }
    }
}