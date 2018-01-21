using UnityEngine;
using System.Collections;

public class SelfDestruct : MonoBehaviour
{
    public float TimeUntilDestruction = 1.0f;


    void Start()
    {
        Destroy(gameObject, TimeUntilDestruction);
    }

}
