using UnityEngine;
using System.Collections;

public class FollowMouse : MonoBehaviour
{
    //References

    //Public

    //Private

    void LateUpdate()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
    }
}
