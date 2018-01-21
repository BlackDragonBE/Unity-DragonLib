using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform target;

    public bool LockX = false;
    public bool LockY = false;
    public bool LockZ = false;

    public void LateUpdate()
    {
        Vector3 originalRotation = transform.rotation.eulerAngles;

        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);

        if (LockX)
        {
            originalRotation = new Vector3(originalRotation.x, rotation.eulerAngles.y, rotation.eulerAngles.z);
            rotation = Quaternion.Euler(originalRotation);
        }

        if (LockY)
        {
            originalRotation = new Vector3(rotation.eulerAngles.x, originalRotation.y, rotation.eulerAngles.z);
            rotation = Quaternion.Euler(originalRotation);
        }

        if (LockZ)
        {
            originalRotation = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, originalRotation.z);
            rotation = Quaternion.Euler(originalRotation);
        }

        transform.rotation = rotation;
    }
}