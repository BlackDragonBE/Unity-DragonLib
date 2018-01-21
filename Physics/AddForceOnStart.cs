using UnityEngine;
using System.Collections;

public class AddForceOnStart : MonoBehaviour
{
    //References

    //Public
    public Vector3 Force;
    public ForceMode ForceMode;

    //Private

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Force, ForceMode);
    }
}
