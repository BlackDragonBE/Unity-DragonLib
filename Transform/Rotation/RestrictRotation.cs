using UnityEngine;

public class RestrictRotation : MonoBehaviour
{
    private Transform _transform;
    private Quaternion _startRotation;

    public void Awake()
    {
        _transform = transform;
        _startRotation = transform.rotation;
    }

    public void LateUpdate()
    {
        _transform.rotation = _startRotation;
    }
}