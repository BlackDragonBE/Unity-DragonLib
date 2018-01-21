using UnityEngine;

public class FollowObjectOnUI : MonoBehaviour
{
    public Camera GUICam;
    public Camera MainCam;
    public Vector3 Offset;
    public Transform Target;

    public void Awake()
    {
        MainCam = Camera.main;
    }

    public void LateUpdate()
    {
        Vector3 newPosition = MainCam.WorldToViewportPoint(Target.position);
        newPosition = GUICam.ViewportToWorldPoint(newPosition) + Offset;

        transform.position = new Vector3(newPosition.x, newPosition.y, 0);
    }
}