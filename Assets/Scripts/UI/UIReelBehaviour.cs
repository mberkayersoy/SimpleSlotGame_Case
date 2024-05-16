using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIReelBehaviour : MonoBehaviour
{
    private UISlotSymbolBehaviour[] _symbols;

    private void Awake()
    {
        _symbols = GetComponentsInChildren<UISlotSymbolBehaviour>();
    }
    public void Initialize(SymbolTestSO[] symbolDatas)
    {

    }

}