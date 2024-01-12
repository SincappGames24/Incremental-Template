using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEditor;
using UnityEngine;

public class LevelDataCollector : EditorWindow
{
    private Vector2 _scrollPosition;

    [MenuItem("Sincapp/Prefab to ScriptableObject")]
    public static void ShowWindow()
    {
        GetWindow<LevelDataCollector>("Prefab to ScriptableObject");
    }

    private List<IInteractable> selectedInteractables = new List<IInteractable>();

    private void OnGUI()
    {
        GUILayout.Label("Select interactables and press the button to create Scriptable Objects for their data.");

        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        DisplayInteractableSelection();

        EditorGUILayout.EndScrollView();
    }

    private void DisplayInteractableSelection()
    {
        EditorGUILayout.BeginVertical();

        foreach (GameObject obj in Selection.gameObjects)
        {
            string prefabName = obj.name;

            IInteractable[] interactableList = obj.GetComponentsInChildren<IInteractable>();

            foreach (IInteractable interactableObject in interactableList)
            {
                if (interactableObject != null)
                {
                    EditorGUILayout.ObjectField(interactableObject.GetType().ToString(), obj, typeof(GameObject), true);

                    if (!selectedInteractables.Contains(interactableObject))
                    {
                        selectedInteractables.Add(interactableObject);
                    }
                }
            }

            if (GUILayout.Button("Save Interactable Variables as ScriptableObject") && selectedInteractables.Count > 0)
            {
                CreateScriptableObjectsForInteractables(selectedInteractables, prefabName);
            }

            selectedInteractables.Clear();
        }

        EditorGUILayout.EndVertical();
    }

    private void CreateScriptableObjectsForInteractables(List<IInteractable> selectedInteractables,string prefabName)
    {
        InteractableDataContainer dataContainer = ScriptableObject.CreateInstance<InteractableDataContainer>();

        foreach (var interactable in selectedInteractables)
        {
            string prefabPath = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(interactable.GetGameObjectReference());
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            if (prefab != null)
            {
                InteractableData data = interactable.GetInteractableData();
                data.InteractableReference = prefab;
                dataContainer.interactableDataList.Add(data);
            }
            else
            {
                Debug.LogError("Obje Prefab Degil! " + interactable.ToString());
            }
        }

        string path = "Assets/_Game/Prefabs/Levels/Resources/LevelDatas/" + prefabName + "_Data.asset";
        AssetDatabase.CreateAsset(dataContainer, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("Scriptable Object for interactables created!");
    }
}

