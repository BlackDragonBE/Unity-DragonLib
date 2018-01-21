using UnityEditor;
using UnityEngine;
using System.Collections;

public class DeleteMyPlayerPrefs : MonoBehaviour
{

    [MenuItem("Tools/DeleteMyPlayerPrefs")]
    static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}