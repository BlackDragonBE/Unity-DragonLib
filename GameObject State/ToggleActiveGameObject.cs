using UnityEngine;
using System.Collections;

public class ToggleActiveGameObject : MonoBehaviour
{
    public void ToggleActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}