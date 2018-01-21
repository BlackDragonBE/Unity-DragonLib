using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class GradientColorChangerSprite : MonoBehaviour
{
    //References

    //Public
    public Gradient CustomGradient;
    public float FullCycleTime = 1f;
    public float StartTime = 0.0f;

    //Private
    private SpriteRenderer _spriteRenderer;
    private float _time;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _time = StartTime;
    }

    void Update()
    {
        _spriteRenderer.color = CustomGradient.Evaluate(_time);
        _time += Time.deltaTime/FullCycleTime;

        if (_time > 1.0f)
        {
            _time = 0f;
        }
    }
}
