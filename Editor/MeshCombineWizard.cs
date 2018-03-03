using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

public class MeshCombineWizard : ScriptableWizard
{
    [Tooltip("Parent of the meshes you wish to combine.")]
    public GameObject ParentGameObject;

    [Tooltip("Assets folder where the prefab and meshes will be saved to.")]
    public string SavePath = "CombinedMeshes";

    [Header("Options")]
    [Tooltip("Copies all colliders and adds them as children GameObjects to the final combined mesh(es).")]
    public bool CopyColliders;

    [Tooltip("Generates UV2 coordinates for the meshes to allow for lightmapping. Uncheck if not needed.")]
    public bool GenerateUV2 = true;

    [Tooltip("Makes the combined meshes static.")]
    public bool MakeStatic;

    [Tooltip("The meshes will be combined even when errors are found, unsuitable meshes will be skipped.")]
    public bool ForceCombine = false;

    [Header("Export")]
    [Tooltip("The combined meshes will be exported to an .OBJ mesh file as well.")]
    public bool ExportMeshCopyToOBJ;

    [MenuItem("BlackDragonLib Tools/Combine Meshes...")]
    private static void ShowWizard()
    {
        DisplayWizard<MeshCombineWizard>("Mesh Combine Wizard", "Create Combined Meshes");
    }

    // Called when wizard is opened & when fields get changed
    private void OnWizardUpdate()
    {
        helpString = "This wizard combines multiple meshes sharing the same material into one.\n" +
                     "All children of the selected parent will be searched for meshes, new mesh prefabs will be created and added to the scene & Assets folder and the parent will be deactivated.\n\n" +
                     "The results are stored in the Assets/" + SavePath + " folder. Feel free to move the assets afterwards.\n" +
            "Note: Scale the window up to see all fields. All fields have tooltips with additional info.";

        if (ParentGameObject == null && Selection.activeTransform != null)
        {
            ParentGameObject = Selection.activeTransform.gameObject;
        }

        if (ParentGameObject == null)
        {
            errorString = "Please assign a parent GameObject.";
            isValid = false;
        }
        else
        {
            errorString = "";

            int meshFilters = ParentGameObject.GetComponentsInChildren<MeshFilter>().Length;
            int colliders = ParentGameObject.GetComponentsInChildren<Collider>().Length;
            int vertices = CalculateTotalVertices(ParentGameObject.GetComponentsInChildren<MeshFilter>());

            isValid = meshFilters > 0;

            if (meshFilters == 0)
            {
                errorString = "No mesh filters found. Nothing to combine.\nPlease select a parent GameObject with meshes in its children.";
            }

            if (SavePath == "")
            {
                isValid = false;
                errorString = "The save path can't be empty. Please supply a folder name.";
            }

            StringBuilder sb = new StringBuilder(helpString);
            sb.Append("\n\nInfo:\n");

            sb.Append("Mesh Filters found: " + meshFilters + "\n");
            sb.Append("Total vertices: " + vertices + "\n");
            sb.Append("Colliders found: " + colliders + "\n");

            helpString = sb.ToString();
        }
    }

    private int CalculateTotalVertices(MeshFilter[] meshFilters)
    {
        int verts = 0;

        foreach (MeshFilter filter in meshFilters)
        {
            verts += filter.sharedMesh.vertexCount;
        }

        return verts;
    }

    // Called when create button is pressed
    private void OnWizardCreate()
    {
        bool cancelCombine = false;

        if (ParentGameObject == null) return;

        Vector3 originalPosition = ParentGameObject.transform.position;
        ParentGameObject.transform.position = Vector3.zero;

        MeshFilter[] meshFilters = ParentGameObject.GetComponentsInChildren<MeshFilter>();
        Dictionary<Material, List<MeshFilter>> materialAndMeshFilterListDictionary = new Dictionary<Material, List<MeshFilter>>();
        List<GameObject> combinedObjects = new List<GameObject>();
        string badMeshNames = "";

        // Group meshes by material
        for (int i = 0; i < meshFilters.Length; i++)
        {
            Material[] materials = meshFilters[i].GetComponent<MeshRenderer>().sharedMaterials;
            if (materials == null) continue;
            if (materials.Length > 1)
            {
                Debug.LogWarning("A mesh with multiple materials can't be combined. Split up the mesh by its materials if possible or move it to another parent.", meshFilters[i]);
                badMeshNames += "\n" + meshFilters[i].name;
                cancelCombine = true;
            }

            if (!cancelCombine)
            {
                // Get first material and add it to the material dictionary if it's not in there yet
                Material material = materials[0];

                if (materialAndMeshFilterListDictionary.ContainsKey(material))
                {
                    materialAndMeshFilterListDictionary[material].Add(meshFilters[i]);
                }
                else
                {
                    materialAndMeshFilterListDictionary.Add(material, new List<MeshFilter> { meshFilters[i] });
                }
            }
        }

        if (cancelCombine)
        {
            if (!ForceCombine)
            {
                Debug.LogError("Cancelled combining because of incompatible meshes:" + badMeshNames);
                ParentGameObject.transform.position = originalPosition;

                return;
            }
            else
            {
                Debug.LogWarning("Forcing enabled: Warnings are ignored and these incompatible meshes are skipped:" + badMeshNames);
            }
        }

        // Combine meshes with same material into a single mesh
        foreach (KeyValuePair<Material, List<MeshFilter>> entry in materialAndMeshFilterListDictionary)
        {
            List<MeshFilter> meshesWithSameMaterial = entry.Value;
            string materialName = entry.Key.ToString().Split(' ')[0];

            CombineInstance[] meshesToCombine = new CombineInstance[meshesWithSameMaterial.Count];
            for (int i = 0; i < meshesWithSameMaterial.Count; i++)
            {
                meshesToCombine[i].mesh = meshesWithSameMaterial[i].sharedMesh;
                meshesToCombine[i].transform = meshesWithSameMaterial[i].transform.localToWorldMatrix;
            }

            Mesh combinedMesh = new Mesh();
            combinedMesh.CombineMeshes(meshesToCombine);
            materialName = "MESH_" + materialName + combinedMesh.GetInstanceID();

            // Generate UV2 for lightmapping purposes
            if (GenerateUV2)
            {
                Unwrapping.GenerateSecondaryUVSet(combinedMesh);
            }

            // Create folder if it doesn't exist
            if (!AssetDatabase.IsValidFolder("Assets/" + SavePath))
            {
                AssetDatabase.CreateFolder("Assets", "" + SavePath);
            }

            // Create combined mesh asset
            AssetDatabase.CreateAsset(combinedMesh, "Assets/" + SavePath + "/COMBINED_" + materialName + ".asset");

            // Configure combined mesh
            string gameObjectName = materialAndMeshFilterListDictionary.Count > 1 ? "COMBINED_" + materialName : "COMBINED_" + ParentGameObject.name;
            GameObject combinedGameObject = new GameObject(gameObjectName);
            MeshFilter filter = combinedGameObject.AddComponent<MeshFilter>();
            filter.sharedMesh = combinedMesh;
            MeshRenderer renderer = combinedGameObject.AddComponent<MeshRenderer>();
            renderer.sharedMaterial = entry.Key;

            if (MakeStatic)
            {
                combinedGameObject.isStatic = true;
            }

            if (ExportMeshCopyToOBJ)
            {
                ConvertMeshToFile(filter, "Assets/" + SavePath + "/" + gameObjectName + "_FILE.OBJ");
                AssetDatabase.Refresh();
            }

            combinedObjects.Add(combinedGameObject);
        }

        GameObject finalCombinedGameObject = null;
        if (combinedObjects.Count > 1)
        {
            finalCombinedGameObject = new GameObject("COMBINED_" + ParentGameObject.name);
            foreach (GameObject combinedObject in combinedObjects)
            {
                combinedObject.transform.SetParent(finalCombinedGameObject.transform, true);
            }
        }
        else
        {
            finalCombinedGameObject = combinedObjects[0];
        }

        if (CopyColliders)
        {
            CopyGameObjectColliders(ref finalCombinedGameObject);
        }

        if (MakeStatic)
        {
            finalCombinedGameObject.isStatic = true;
        }

        Object prefab = PrefabUtility.CreateEmptyPrefab("Assets/" + SavePath + "/" + finalCombinedGameObject.name + ".prefab");
        PrefabUtility.ReplacePrefab(finalCombinedGameObject, prefab, ReplacePrefabOptions.ConnectToPrefab);

        ParentGameObject.SetActive(false);
        ParentGameObject.transform.position = originalPosition;
        finalCombinedGameObject.transform.position = originalPosition;
        Selection.activeGameObject = finalCombinedGameObject;
    }

    private void CopyGameObjectColliders(ref GameObject finalGameObject)
    {
        Collider[] colliders = ParentGameObject.GetComponentsInChildren<Collider>();

        for (int i = 0; i < colliders.Length; i++)
        {
            Collider collider = colliders[i];

            GameObject colGameObject = new GameObject(collider.name + "_COLLIDER");
            colGameObject.transform.SetParent(finalGameObject.transform);
            colGameObject.transform.position = collider.transform.position;

            // Copy the collider to the new GameObject using undocumented editor methods
            UnityEditorInternal.ComponentUtility.CopyComponent(collider);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(colGameObject);
        }
    }

    private string ConvertMeshToString(MeshFilter mf)
    {
        Mesh m = mf.sharedMesh;

        StringBuilder sb = new StringBuilder();

        sb.Append("g ").Append(mf.name).Append("\n");
        foreach (Vector3 v in m.vertices)
        {
            sb.Append(string.Format("v {0} {1} {2}\n", v.x, v.y, v.z));
        }
        sb.Append("\n");
        foreach (Vector3 v in m.normals)
        {
            sb.Append(string.Format("vn {0} {1} {2}\n", v.x, v.y, v.z));
        }
        sb.Append("\n");
        foreach (Vector3 v in m.uv)
        {
            sb.Append(string.Format("vt {0} {1}\n", v.x, v.y));
        }

        for (int material = 0; material < m.subMeshCount; material++)
        {
            sb.Append("\n");
            //sb.Append("usemtl ").Append(mats[material].name).Append("\n");
            //sb.Append("usemap ").Append(mats[material].name).Append("\n");

            int[] triangles = m.GetTriangles(material);
            for (int i = 0; i < triangles.Length; i += 3)
            {
                sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n",
                    triangles[i] + 1, triangles[i + 1] + 1, triangles[i + 2] + 1));
            }
        }
        return sb.ToString();
    }

    private void ConvertMeshToFile(MeshFilter mf, string pathAndName)
    {
        using (StreamWriter sw = new StreamWriter(pathAndName))
        {
            sw.Write(ConvertMeshToString(mf));
        }
    }
}