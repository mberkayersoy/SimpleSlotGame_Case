using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class ResultGeneratorWindow : EditorWindow
{
    private ResultGenerator _resultgenerator;
    private Vector2 scrollPosition;

    [MenuItem("Window/Show Results Window")]
    public static void ShowWindow()
    {
        GetWindow<ResultGeneratorWindow>("Show Results");
    }

    private void OnEnable()
    {
        _resultgenerator = FindObjectOfType<SlotGameManager>().Resultgenerator;
    }

    private void OnGUI()
    {
        if (_resultgenerator == null)
        {
            EditorGUILayout.LabelField("Slot results not found!");
            return;
        }
        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        EditorGUILayout.LabelField("Result Indices", EditorStyles.boldLabel);
        foreach (var pair in GetKeyIndices(_resultgenerator.Results))
        {
            EditorGUILayout.LabelField($"{pair.Key}: {string.Join(",", pair.Value)}");
        }
        EditorGUILayout.Space(20);

        EditorGUILayout.LabelField("Results List", EditorStyles.boldLabel);
        for (int i = 0; i < _resultgenerator.Results.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField($"Spin {i}:", GUILayout.Width(50));
            _resultgenerator.Results[i].Key = EditorGUILayout.TextField(_resultgenerator.Results[i].Key);
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
    }

    private Dictionary<string, List<int>> GetKeyIndices(CustomTuple<string, int>[] results)
    {
        Dictionary<string, List<int>> keyIndices = new Dictionary<string, List<int>>();

        for (int i = 0; i < results.Length; i++)
        {
            string key = results[i].Key;
            if (!keyIndices.ContainsKey(key))
            {
                keyIndices[key] = new List<int>();
            }
            keyIndices[key].Add(i);
        }

        return keyIndices;
    }
}
