using UnityEditor;
using UnityEngine;

public class DeleteMyPlayerPrefs : MonoBehaviour
{
    [MenuItem("Tools/DeleteMyPlayerPrefs")]
    private static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}