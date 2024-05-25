using Cysharp.Threading.Tasks;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
using System.Linq;

public class ReelBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _bottomTarget;
    [SerializeField] private Transform _aboveTarget;
    private SlotSymbolBehaviour[] _symbols;
    private SlotSymbolBehaviour _targetSymbol;
    private const float REEL_CENTER = 0f;
    
    private const float SLOW_DOWN_FACTOR = 0.1f;
    private void Awake()
    {
        _symbols = GetComponentsInChildren<SlotSymbolBehaviour>();
    }
    public void Initialize(Dictionary<int, SlotSymbolData> slotSymbols)
    {
        Random random = new Random();
        foreach (var symbol in _symbols)
        {
            int randomKey = slotSymbols.Keys.ElementAt(random.Next(slotSymbols.Count));
            symbol.Initialize(slotSymbols[randomKey], _bottomTarget, _aboveTarget);
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
            symbol.SetSlowDown(SLOW_DOWN_FACTOR, slowDownDuration).Forget();
        }
        await UniTask.WaitForSeconds(slowDownDuration);
    }
}
