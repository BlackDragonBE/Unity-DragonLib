using UnityEditor;
using System.Diagnostics;
using System.IO;

/*
This script will automatically make a Windows, Mac and Linux build in any folder you choose.

    Usage:
    - Change paramaters depending on your game / preferences
    - In Unity Editor: Build/Multi Build
    - Builds will be made and zipped by 7zip if chosen to do so and 7zip is installed on the system
*/

public class MultiBuild
{
    [MenuItem("Build/Multi Build")]
    public static void DoMultiBuild()
    {
        // Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Builds Folder", "", "");

        //PARAMETERS START
        string gameName = "Dyna Bros"; //Name of your game
        string[] levels = new string[] { "Assets/Scenes/Title.unity", "Assets/Scenes/Game.unity" }; //Scenes in order of loading
        bool zipFolders = true; //Use 7zip to compress the created folders
        //PARAMETERS END

        // Build Win
        BuildPipeline.BuildPlayer(levels, path + "/Win/" + gameName + ".exe", BuildTarget.StandaloneWindows, BuildOptions.None);

        // Build Mac
        BuildPipeline.BuildPlayer(levels, path + "/Mac/" + gameName + ".app", BuildTarget.StandaloneOSXUniversal, BuildOptions.None);

        // Build Linux
        BuildPipeline.BuildPlayer(levels, path + "/Linux/" + gameName + ".x86", BuildTarget.StandaloneLinuxUniversal, BuildOptions.None);

        // 7zip
        if (zipFolders && File.Exists(@"C:\Program Files\7-Zip\7z.exe"))
        {
            ZipFolder(path + "/Win/", gameName + " Win.zip");
            ZipFolder(path + "/Mac/", gameName + " Mac.zip");
            ZipFolder(path + "/Linux/", gameName + " Linux.zip");
        }


    }

    static void ZipFolder(string folderPath, string zipName)
    {
        Process proc = new Process();
        proc.StartInfo.FileName = @"C:\Program Files\7-Zip\7z.exe";
        proc.StartInfo.Arguments = "a -tzip " + '"' + zipName + '"' + " " + '"' + folderPath + '"';
        proc.Start();
    }
}
