using UnityEngine;

public class AddForceOnStart : MonoBehaviour
{
    public Vector3 Force;
    public ForceMode ForceMode;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(Force, ForceMode);
    }
}