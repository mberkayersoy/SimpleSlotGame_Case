#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SlotSymbolEditorWindow : EditorWindow
{
    private Dictionary<int, SlotSymbolData> _symbolDataDictionary;
    private Vector2 _scrollPosition;
    private string _newKey = "";
    private string _newName = "";
    private string _newSharpSpritePath = "";
    private string _newBlurSpritePath = "";

    [MenuItem("Slot Tools/Create Slot Symbol")]
    public static void Init()
    {
        GetWindow<SlotSymbolEditorWindow>("Slot Symbol Editor");
    }
    private void OnEnable()
    {
        _symbolDataDictionary = JsonSaver.LoadData<Dictionary<int, SlotSymbolData>>(JsonSaver.SYMBOL_DATA_PATH);
    }
    private void OnDisable()
    {
        _symbolDataDictionary = null;
    }
    private void OnGUI()
    {
        GUILayout.Label("Manage Slot Symbol Data", EditorStyles.boldLabel);
        // Display section for adding new data
        GUILayout.Label("Add New Data:", EditorStyles.boldLabel);
        GUILayout.BeginHorizontal();
        GUILayout.Label("ID (int):", GUILayout.Width(50));
        _newKey = GUILayout.TextField(_newKey);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Name:", GUILayout.Width(50));
        _newName = GUILayout.TextField(_newName);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Sharp Sprite Path:", GUILayout.Width(100));
        _newSharpSpritePath = GUILayout.TextField(_newSharpSpritePath);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Blur Sprite Path:", GUILayout.Width(100));
        _newBlurSpritePath = GUILayout.TextField(_newBlurSpritePath);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);

        if (GUILayout.Button("Add Data"))
        {
            TryAddData();
        }

        GUILayout.Space(10);

        // Display current data
        GUILayout.Label("Current Data:", EditorStyles.boldLabel);
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        foreach (var pair in _symbolDataDictionary)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Key: " + pair.Key.ToString(), GUILayout.Width(100));
            EditorGUILayout.LabelField("Name: " + pair.Value.Name);
            EditorGUILayout.LabelField("Sharp Path: " + pair.Value.SharpSpritePath);
            EditorGUILayout.LabelField("Blur Path: " + pair.Value.BlurSpritePath);

            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                RemoveData(pair.Key);
            }

            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Save Data"))
        {
            JsonSaver.SaveData(_symbolDataDictionary, JsonSaver.SYMBOL_DATA_PATH);
            AssetDatabase.Refresh();
        }
    }

    private void RemoveData(int key)
    {
        Dictionary<int, SlotSymbolData> tmp = new(_symbolDataDictionary);
        tmp.Remove(key);
        _symbolDataDictionary = tmp;
    }

    private void TryAddData()
    {
        int key;
        if (!int.TryParse(_newKey, out key))
        {
            EditorUtility.DisplayDialog("Invalid Key", "Key must be an integer value.", "OK");
            return;
        }

        if (_symbolDataDictionary.ContainsKey(key))
        {
            EditorUtility.DisplayDialog("Key Already Exists", "Key already exists. Please enter a different key.", "OK");
            return;
        }

        SlotSymbolData newData = new(_newName, key, _newSharpSpritePath, _newBlurSpritePath);
        _symbolDataDictionary.Add(key, newData);

        ClearInputFields();
    }
    private void ClearInputFields()
    {
        _newKey = "";
        _newName = "";
        _newSharpSpritePath = "";
        _newBlurSpritePath = "";
    }
}
#endif