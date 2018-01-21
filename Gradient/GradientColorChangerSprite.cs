using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class GradientColorChangerSprite : MonoBehaviour
{
    public Gradient CustomGradient;
    public float FullCycleTime = 1f;
    public float StartTime = 0.0f;

    private SpriteRenderer _spriteRenderer;
    private float _time;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _time = StartTime;
    }

    private void Update()
    {
        _spriteRenderer.color = CustomGradient.Evaluate(_time);
        _time += Time.deltaTime / FullCycleTime;

        if (_time > 1.0f)
        {
            _time = 0f;
        }
    }
}