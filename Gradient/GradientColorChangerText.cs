using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GradientColorChangerText : MonoBehaviour
{
    //References

    //Public
    public Gradient CustomGradient;
    public float FullCycleTime = 1f;
    public float StartTime = 0.0f;

    //Private
    private Text _text;
    private float _time;

    void Start()
    {
        _text = GetComponent<Text>();
        _time = StartTime;
    }

    void Update()
    {
        _text.color = CustomGradient.Evaluate(_time);
        _time += Time.deltaTime/FullCycleTime;

        if (_time > 1.0f)
        {
            _time = 0f;
        }
    }
}
