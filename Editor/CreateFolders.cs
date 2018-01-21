using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class CreateFolders {
  [MenuItem("Assets/Create Default Folders")]
  private static void CreateAllFolders() {
    List<string> folders = new List<string>();
    folders.Add("Scripts");
    folders.Add("Materials");
    folders.Add("Textures");
    folders.Add("Sprites");
    folders.Add("Scenes");
    folders.Add("Models");
    folders.Add("Prefabs");
    folders.Add("Sounds");
    folders.Add("Music");

    foreach (string folder in folders) {
      if (!Directory.Exists("Assets/" + folder)) {
        Directory.CreateDirectory("Assets/" + folder);
      }
    }

    AssetDatabase.Refresh();
  }
}
