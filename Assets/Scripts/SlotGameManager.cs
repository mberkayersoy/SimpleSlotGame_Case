using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SlotGameManager : MonoBehaviour
{
    private ResultGenerator _resultgenerator;
    private SpinHandler _spinhandler;
    public ResultGenerator Resultgenerator { get => _resultgenerator; private set => _resultgenerator = value; }
    private void Awake()
    {
        if (!File.Exists(JsonSaver.ALL_SPIN_RESULTS_FILE_PATH))
        {
            Resultgenerator = new ResultGenerator();
        }
        _spinhandler = new SpinHandler();
        _spinhandler.AllSpinResultsDone += CreateNewSpinResults;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _spinhandler.GetNextSpinResult();
        }
    }

    private void CreateNewSpinResults()
    {
        Resultgenerator = new ResultGenerator();
    }

    private void OnDestroy()
    {
        _spinhandler.AllSpinResultsDone -= CreateNewSpinResults;
    }
}
