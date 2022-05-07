using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif
using UnityEngine;

#if UNITY_EDITOR

public class SincappWindowExtentions : MonoBehaviour
{
    [MenuItem("Sincapp/Setup Child As Mesh")]
    public static void SetupChildMesh()
    {
        GameObject activeGameObject = Selection.activeGameObject;
       
        if (activeGameObject == null) return;

        activeGameObject.AddComponent(typeof(BoxCollider));
        GameObject go = new GameObject("GameObject");
        go.transform.SetParent(activeGameObject.transform);
        go.transform.localPosition = Vector3.zero;
        MeshFilter childMeshFilter = go.AddComponent(typeof(MeshFilter)) as MeshFilter;
        MeshRenderer childMeshRenderer = go.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
        EditorUtility.CopySerialized(activeGameObject.GetComponent<MeshFilter>(), childMeshFilter);
        EditorUtility.CopySerialized(activeGameObject.GetComponent<MeshRenderer>(), childMeshRenderer);
        DestroyImmediate(activeGameObject.GetComponent<MeshFilter>());
        DestroyImmediate(activeGameObject.GetComponent<MeshRenderer>());
    }

    [MenuItem("Sincapp/Clear Persist Data")]
    public static void ClearPersistData()
    {
        var persistFileName = "Persist Sincapp";
        var persistFileLocation = Application.persistentDataPath + Path.DirectorySeparatorChar + persistFileName;

        if (File.Exists(persistFileLocation))
        {
            File.Delete(persistFileLocation);
        }
    }
}

#endif