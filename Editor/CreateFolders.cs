using System.Collections.Generic;
using System.IO;
using UnityEditor;

public class CreateFolders
{
    [MenuItem("Assets/Create Default Folders")]
    private static void CreateAllFolders()
    {
        // Replace this with your company or user name or leave empty to add folders to root
        string CompanyName = "BlackDragonBE";

        List<string> folders = new List<string>
        {
            "Scripts",
            "Materials",
            "Textures",
            "Sprites",
            "Scenes",
            "Models",
            "Prefabs",
            "Sounds",
            "Music"
        };

        foreach (string folder in folders)
        {
            if (!Directory.Exists("Assets/" + folder))
            {
                Directory.CreateDirectory("Assets/" + CompanyName + "/" + folder);
            }
        }

        AssetDatabase.Refresh();
    }
}