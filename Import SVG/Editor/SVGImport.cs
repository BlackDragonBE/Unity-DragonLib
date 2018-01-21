using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

public class SVGImport : AssetPostprocessor
{
    //CHANGE THIS TO THE INKSCAPE EXECUTABLE PATH:
    public static string InkscapePath = @"C:\Program Files\Inkscape\inkscape.exe";

    // This is called always when importing something
    static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        //If the inkscape executable is not found:
        if (!File.Exists(InkscapePath))
        {
            //Try other paths first
            if (File.Exists(@"C:\Program Files (x86)\Inkscape\inkscape.exe")) //Windows x86
            {
                InkscapePath = @"C:\Program Files\Inkscape\inkscape.exe";
            }
            else if (File.Exists(@"/Applications/Inkscape.app/Contents/Resources/bin/inkscape")) //Mac OSX
            {
                InkscapePath = @"/Applications/Inkscape.app/Contents/Resources/bin/inkscape";
            }
            else
            {
                UnityEngine.Debug.LogError("SVG To PNG Importer: Inkscape was not found.\nPlease point the InkscapePath string in SVGImport.cs to the inkscape executable or install Inkscape.");
                return;
            }
        }

        foreach (string asset in importedAssets)
        {
            if (asset.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            {
                var process = Process.Start(InkscapePath,
                    '"' + Path.GetFullPath(asset) + '"' + " --export-png=" + '"' + Path.GetFullPath(asset).Replace("svg", "png") + '"');

                if (process != null)
                    process.WaitForExit();

                if (File.Exists(asset.Replace("svg", "png")))
                {
                    //Only if file exists
                    AssetDatabase.ImportAsset(asset.Replace("svg", "png"));
                }
                else
                {
                    UnityEngine.Debug.LogError("SVG To PNG Importer: Failed to convert " + asset);
                }
            }
        }
    }
}