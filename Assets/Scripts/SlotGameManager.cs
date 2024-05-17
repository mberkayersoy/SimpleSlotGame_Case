using System.IO;
using UnityEngine;
using Zenject;

public class SlotGameManager : MonoBehaviour
{
    private ResultGenerator _resultgenerator;
    [Inject] private SpinHandler _spinhandler;
    public ResultGenerator Resultgenerator { get => _resultgenerator; private set => _resultgenerator = value; }
    private void Awake()
    {
        if (!File.Exists(JsonSaver.ALL_SPIN_RESULTS_FILE_PATH))
        {
            Resultgenerator = new ResultGenerator();
            _spinhandler.RefreshSpinResults();

        }
        _spinhandler.AllSpinResultsDone += CreateNewSpinResults;
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
