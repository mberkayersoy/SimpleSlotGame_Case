using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.Linq;

public class UIReelBehaviour : MonoBehaviour
{
    private UISlotSymbolBehaviour[] _symbols;
    private UISlotSymbolBehaviour _targetSymbol;
    private const float REEL_CENTER = 0f;

    private const float SLOW_DOWN_FACTOR = 0.25f;
    private void Awake()
    {
        _symbols = GetComponentsInChildren<UISlotSymbolBehaviour>();
    }

    public void Initialize(Dictionary<int, SlotSymbolData> slotSymbols)
    {
        Random random = new Random();
        foreach (var symbol in _symbols)
        {
            int randomKey = slotSymbols.Keys.ElementAt(random.Next(slotSymbols.Count));
            symbol.Initialize(slotSymbols[randomKey]);
            slotSymbols.Remove(randomKey);
        }
    }

    public void SetTargetSymbol(SlotSymbolData targetSymbol)
    {
        _targetSymbol = _symbols.FirstOrDefault(symbol => symbol.SymbolID == targetSymbol.ID);
    }
    public void StartSpinning(float spinDuration)
    {
        foreach (var symbol in _symbols)
        {
            symbol.ControlMovement(spinDuration, true);
        }
    }

    public async UniTask StopSpinAtTarget()
    {
        await UniTask.WaitUntil(() => _targetSymbol.TargetY == REEL_CENTER);
        foreach (var symbol in _symbols)
        {
            symbol.StopMovement();
        }
    }

    public async UniTask SlowDownSymbols(float slowDownDuration)
    {
        foreach (var symbol in _symbols)
        {
            symbol.SlowDownTween(SLOW_DOWN_FACTOR);
        }
        await UniTask.WaitForSeconds(slowDownDuration);
        StopSpinAtTarget().Forget();
    }
}
