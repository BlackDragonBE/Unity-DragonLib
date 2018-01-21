using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GradientColorChangerText : MonoBehaviour
{
    public Gradient CustomGradient;
    public float FullCycleTime = 1f;
    public float StartTime = 0.0f;

    private Text _text;
    private float _time;

    private void Start()
    {
        _text = GetComponent<Text>();
        _time = StartTime;
    }

    private void Update()
    {
        _text.color = CustomGradient.Evaluate(_time);
        _time += Time.deltaTime / FullCycleTime;

        if (_time > 1.0f)
        {
            _time = 0f;
        }
    }
}