#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using Zenject;

public class ResultGeneratorWindow : EditorWindow
{
    private Dictionary<string, ResultData> _createdResultsDic;
    private Dictionary<int, SlotSymbolData> _symbolDataDic;
    private Vector2 _scrollPosition;
    private string _newResultID = "";
    private int _newResultChancePer = 0;
    private const int VALID_RESULT_LENGTH = 3;
    private IDataService _dataService = new JsonDataService();

    [MenuItem("Slot Tools/Create Result")]
    public static void Init()
    {
        ResultGeneratorWindow window = GetWindow<ResultGeneratorWindow>();
        window.Show();
    }
    private void OnEnable()
    {
        _symbolDataDic = _dataService.LoadData<Dictionary<int, SlotSymbolData>>(GameConstantData.SYMBOL_DATA_PATH);
        _createdResultsDic = _dataService.LoadData<Dictionary<string, ResultData>>(GameConstantData.CREATED_RESULTS_FILE_PATH);
    }

    private void OnGUI()
    {
        GUILayout.Label("Created Symbols", EditorStyles.boldLabel);
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        if (_symbolDataDic != null)
        {
            foreach (var pair in _symbolDataDic)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("ID: " + pair.Key.ToString(), GUILayout.Width(50));
                EditorGUILayout.LabelField("Name: " + pair.Value.Name);
                EditorGUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();

        GUILayout.Space(10);

        if (_createdResultsDic != null)
        {
            foreach (var resultData in _createdResultsDic)
            {
                EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
                EditorGUILayout.LabelField("Result Combo: " + resultData.Value.ResultName);
                EditorGUILayout.LabelField("Chance Percentage: " + resultData.Value.ChancePer);

                // Add remove button
                if (GUILayout.Button("-", GUILayout.Width(25)))
                {
                    RemoveData(resultData.Key);
                }
                EditorGUILayout.EndHorizontal();
            }
        }

        GUILayout.Space(10);
        GUILayout.Label("To add a new result data, just write the IDs of 3 of the Created Symbols above and click the add button. \n" +
                        "Example usage: Result Combo = 014, Chance Percentage = 5");
        GUILayout.Label("Add New Result Data:", EditorStyles.boldLabel);
        _newResultID = EditorGUILayout.TextField("Result Combo:", _newResultID);
        _newResultChancePer = EditorGUILayout.IntSlider("Chance Percentage:", _newResultChancePer, 0, 100);

        if (GUILayout.Button("Add Result"))
        {
            TryAddResultData();
        }
        if (GUILayout.Button("Save"))
        {
            TrySave();
        }
    }
    private void RemoveData(string key)
    {
        Dictionary<string, ResultData> tmp = new(_createdResultsDic);
        tmp.Remove(key);
        _createdResultsDic = tmp;
    }

    private void TrySave()
    {
        if (CheckAllResultPercentages())
        {
            _dataService.SaveData(GameConstantData.CREATED_RESULTS_FILE_PATH, _createdResultsDic);
            EditorUtility.DisplayDialog("Results Saved",
                                        "All results have been saved successfully.", "OK");
            AssetDatabase.Refresh();
        }
    }
    private bool CheckAllResultPercentages()
    {
        int targetPer = 100;
        int currentPer = 0;
        foreach (var item in _createdResultsDic)
        {
            currentPer += item.Value.ChancePer;
        }

        if (currentPer != targetPer)
        {
            EditorUtility.DisplayDialog("Insufficient Percentage", 
                                        "The sum of the percentage probabilities of all results must be 100. \n" +
                                        $"Current total percentage: {currentPer}", "OK");
            return false;
        }
        return true;
    }
    private void TryAddResultData()
    {
        if (_createdResultsDic == null)
        {
            _createdResultsDic = new Dictionary<string, ResultData>();
        }
        if (_newResultID.Length != VALID_RESULT_LENGTH)
        {
            EditorUtility.DisplayDialog("Invalid Result Length",
                                        $"The length of the combo must be {VALID_RESULT_LENGTH}.\n" +
                                        $"The length of the written combo is {_newResultID.Length}.", "OK");
            return;
        }
        foreach (char symbolChar in _newResultID)
        {
            if (!int.TryParse(symbolChar.ToString(), out int symbolID) || !_symbolDataDic.ContainsKey(symbolID))
            {
                EditorUtility.DisplayDialog("Invalid Symbol",
                                            "The result can only contain IDs of created symbols.", "OK");
                return;
            }
        }

        if (_createdResultsDic.ContainsKey(_newResultID))
        {
            EditorUtility.DisplayDialog("Exitsing Result",
                                        $"{_newResultID} result is already exist.", "OK");
            return;
        }
        SlotSymbolData[] resultOfSymbols = GetResultOfSymbols();
        ResultData newResult = new ResultData(_newResultChancePer, 
                                              resultOfSymbols, 
                                              SetName(resultOfSymbols), 
                                              CheckAllSymbolsSameID(resultOfSymbols),
                                              CheckIsDelayNeeded(resultOfSymbols));
        _createdResultsDic.Add(_newResultID, newResult);

        ClearInputFields();
    }
    private SlotSymbolData[] GetResultOfSymbols()
    {
        SlotSymbolData[] resultSymbolsData = new SlotSymbolData[VALID_RESULT_LENGTH];
        for (int i = 0; i < VALID_RESULT_LENGTH; i++)
        {
            int symbolID = int.Parse(_newResultID[i].ToString());
            resultSymbolsData[i] = _symbolDataDic[symbolID];
        }
        return resultSymbolsData;
    }
    private bool CheckAllSymbolsSameID(SlotSymbolData[] resultSymbols)
    {
        if (resultSymbols == null || resultSymbols.Length <= 1)
        {
            return false;
        }

        int[] symbolIDs = resultSymbols.Select(symbol => symbol.ID).ToArray();

        return symbolIDs.All(id => id == symbolIDs[0]);
    }
    public bool CheckIsDelayNeeded(SlotSymbolData[] resultSymbols)
    {
        if (resultSymbols != null && resultSymbols.Length >= 2)
        {
            return resultSymbols[0].ID == resultSymbols[1].ID;
        }

        return false;
    }
    private string SetName(SlotSymbolData[] resultSymbols)
    {
        StringBuilder name = new();
        for (int i = 0; i < resultSymbols.Length; i++)
        {
            name.Append(resultSymbols[i].Name);
            if (i < resultSymbols.Length - 1)
            {
                name.Append("-");
            }
        }
        return name.ToString();
    }
    private void ClearInputFields()
    {
        _newResultID = "";
        _newResultChancePer = 0;
    }
}
#endif