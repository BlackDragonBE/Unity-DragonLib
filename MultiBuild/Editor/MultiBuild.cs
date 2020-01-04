using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

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
        string gameName = Application.productName; //Name of your game, product name by default

        const bool buildWin = true;
        const bool buildMac = true;
        const bool buildLinux = true;
        const bool buildWebGL = true;

        //Scene paths
        string[] scenes = new string[EditorBuildSettings.scenes.Length];

        for (var i = 0; i < EditorBuildSettings.scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }

        bool zipFolders = true; //Use 7zip to compress the created folders
        //PARAMETERS END

        BuildPlayerOptions buildOptions = new BuildPlayerOptions();
        buildOptions.scenes = scenes;

        // Windows Build
        if (buildWin)
        {
            buildOptions.target = BuildTarget.StandaloneWindows64;
            buildOptions.locationPathName = path + "/Win/" + gameName + ".exe";
            BuildPipeline.BuildPlayer(buildOptions);
        }

        // Build Mac
        if (buildMac)
        {
            buildOptions.target = BuildTarget.StandaloneOSX;
            buildOptions.locationPathName = path + "/Mac/" + gameName + ".app";
            BuildPipeline.BuildPlayer(buildOptions);
        }

        // Build Linux
        if (buildLinux)
        {
            buildOptions.target = BuildTarget.StandaloneLinux64;
            buildOptions.locationPathName = path + "/Linux/" + gameName + ".x86";
            BuildPipeline.BuildPlayer(buildOptions);
        }

        // 7zip
        if (zipFolders && File.Exists(@"C:\Program Files\7-Zip\7z.exe"))
        {
            ZipFolder(path + "/Win/", gameName + " Win.zip");
            ZipFolder(path + "/Mac/", gameName + " Mac.zip");
            ZipFolder(path + "/Linux/", gameName + " Linux.zip");
        }

        // Build WebGL
        if (buildWebGL)
        {
            buildOptions.target = BuildTarget.WebGL;
            buildOptions.locationPathName = path + "/WebGL/" + gameName;
            BuildPipeline.BuildPlayer(buildOptions);
        }
    }

    private static void ZipFolder(string folderPath, string zipName)
    {
        Process proc = new Process();
        proc.StartInfo.FileName = @"C:\Program Files\7-Zip\7z.exe";
        proc.StartInfo.Arguments = "a -tzip " + '"' + zipName + '"' + " " + '"' + folderPath + '"';
        proc.Start();
    }
}