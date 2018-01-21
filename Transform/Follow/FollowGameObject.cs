using UnityEngine;

/// <summary>
/// Follows a gameObject around in the chosen axis
/// </summary>
public class FollowGameObject : MonoBehaviour
{
    public Transform TransformToFollow;
    public float MoveSpeed = 20;
    public Vector3 Offset;

    public bool FollowX = true;
    public bool FollowY = true;
    public bool FollowZ = true;

    public void LateUpdate()
    {
        Vector3 poitionNextFrame = Vector3.MoveTowards(transform.position, TransformToFollow.position + Offset,
                                                 MoveSpeed * Time.fixedDeltaTime);

        if (!FollowX)
        {
            poitionNextFrame.x = transform.position.x;
        }

        if (!FollowY)
        {
            poitionNextFrame.y = transform.position.y;
        }

        if (!FollowZ)
        {
            poitionNextFrame.z = transform.position.z;
        }

        transform.position = poitionNextFrame;
    }
}