using UnityEngine;
using System.Collections;

public class RotateAroundAxis : MonoBehaviour
{
    public float RotationSpeed = 10.0f; //Speed to rotate with
    public bool TurnOnX;
    public bool TurnOnY;
    public bool TurnOnZ;
    public bool TurnOnLocalX;
    public bool TurnOnLocalY;
    public bool TurnOnLocalZ;

    void Update()
    {
        if (TurnOnX)
        {
            gameObject.transform.Rotate(transform.right, RotationSpeed * Time.deltaTime);
        }

        if (TurnOnY)
        {
            gameObject.transform.Rotate(transform.up, RotationSpeed * Time.deltaTime);
        }

        if (TurnOnZ)
        {
            gameObject.transform.Rotate(transform.forward, RotationSpeed * Time.deltaTime);
        }

        if (TurnOnLocalX)
        {
            gameObject.transform.Rotate(Vector3.right, RotationSpeed * Time.deltaTime);
        }

        if (TurnOnLocalY)
        {
            gameObject.transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);
        }

        if (TurnOnLocalZ)
        {
            gameObject.transform.Rotate(Vector3.forward, RotationSpeed * Time.deltaTime);
        }
    }
}
