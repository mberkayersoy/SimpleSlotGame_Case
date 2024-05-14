using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGameManager : MonoBehaviour
{
    private ResultGenerator _resultgenerator;

    public ResultGenerator Resultgenerator { get => _resultgenerator; private set => _resultgenerator = value; }

    private void Awake()
    {
        Resultgenerator = new ResultGenerator();
    }
}
