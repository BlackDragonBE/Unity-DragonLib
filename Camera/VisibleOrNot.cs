using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// How to use:
/// Attach to any GameObject and it will report if it's actually visible to a camera or not using a combination of frustrum checking and linecasts.
/// Be sure to hide the Scene view when using this script as the editor camera also triggers OnBecameVisible & OnBecameInvisible (Unity bug)
/// </summary>
public class VisibleOrNot : MonoBehaviour
{
    public event Action<bool, Vector3> OnVisibilityChanged;

    [Serializable]
    public class VisibilityChangedEvent : UnityEvent<bool, Vector3> { }

    public VisibilityChangedEvent OnVisibilityChangedEvent;

    [HideInInspector]
    public bool IsVisible;

    [Tooltip("WARNING: Will only work once. Be sure to hide the editor view after to avoid editor camera interference.")]
    public bool DrawDebugLines;

    [Tooltip("Camera to use for checking visibility. Defaults to main camera if left empty.")]
    public Camera _rayCastCamera;

    private bool _potentialToBeVisible = false;
    private Collider _collider;

    private void Awake()
    {
        if (!_rayCastCamera)
        {
            _rayCastCamera = Camera.main;
        }

        _collider = GetComponent<Collider>();
    }

    private void Update()
    {
        if (_potentialToBeVisible)
        {
            if (IsVisible || !IsCameraVisible())
            {
                // Already triggered visible event or collider blocking view
                return;
            }

            IsVisible = true;
            TriggerVisibilityChangedEvent();
        }
    }

    /// <summary>
    /// Check for collisions between this object & the camera from the center + all sides.
    /// Return true if any linecasts can "see" the camera.
    /// </summary>
    /// <returns>Return true if any linecasts can "see" the camera.</returns>
    private bool IsCameraVisible()
    {
        bool camVisible = !Physics.Linecast(transform.position, _rayCastCamera.transform.position) ||
            !Physics.Linecast(transform.position + new Vector3(_collider.bounds.extents.x, 0, 0), _rayCastCamera.transform.position) ||
            !Physics.Linecast(transform.position - new Vector3(_collider.bounds.extents.x, 0, 0), _rayCastCamera.transform.position) ||
            !Physics.Linecast(transform.position + new Vector3(0, _collider.bounds.extents.y, 0), _rayCastCamera.transform.position) ||
            !Physics.Linecast(transform.position - new Vector3(0, _collider.bounds.extents.y, 0), _rayCastCamera.transform.position) ||
            !Physics.Linecast(transform.position + new Vector3(0, 0, _collider.bounds.extents.z), _rayCastCamera.transform.position) ||
            !Physics.Linecast(transform.position - new Vector3(0, 0, _collider.bounds.extents.z), _rayCastCamera.transform.position);

        if (DrawDebugLines)
        {
            Debug.DrawLine(transform.position, _rayCastCamera.transform.position, Color.red);
            Debug.DrawLine(transform.position + new Vector3(_collider.bounds.extents.x, 0, 0), _rayCastCamera.transform.position, Color.red);
            Debug.DrawLine(transform.position - new Vector3(_collider.bounds.extents.x, 0, 0), _rayCastCamera.transform.position, Color.red);
            Debug.DrawLine(transform.position + new Vector3(0, _collider.bounds.extents.y, 0), _rayCastCamera.transform.position, Color.red);
            Debug.DrawLine(transform.position - new Vector3(0, _collider.bounds.extents.y, 0), _rayCastCamera.transform.position, Color.red);
            Debug.DrawLine(transform.position + new Vector3(0, 0, _collider.bounds.extents.z), _rayCastCamera.transform.position, Color.red);
            Debug.DrawLine(transform.position - new Vector3(0, 0, _collider.bounds.extents.z), _rayCastCamera.transform.position, Color.red);
        }

        return camVisible;
    }

    private void OnBecameInvisible()
    {
        _potentialToBeVisible = false;

        if (!IsVisible)
        {
            // Don't call event if already invisible (be it out of frustrum or due to a collider blocking the view)
            return;
        }

        IsVisible = false;
        TriggerVisibilityChangedEvent();
    }

    private void OnBecameVisible()
    {
        _potentialToBeVisible = true;
    }

    private void TriggerVisibilityChangedEvent()
    {
        OnVisibilityChanged.InvokeSafely(IsVisible, transform.position);
        OnVisibilityChangedEvent.Invoke(IsVisible, transform.position);
    }
}