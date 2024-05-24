using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;
using System;
using Random = UnityEngine.Random;

public class ReelsController : MonoBehaviour, ISpinState
{
    [SerializeField] private ReelBehaviour[] _reels;
    [SerializeField] private float _totalSpinDuration;
    [SerializeField, Range(0f, 0.1f)] private float _stopDelayBetweenReels = 0.1f;
    [SerializeField] private float _reelSymbolsTweenDuration;

    [Inject] private ISpinHandler _spinhandler;
    private ResultData _currentResult;
    private const float MIN_START_DELAY = 0f;
    private const float MAX_START_DELAY = 0.1f;
    private const float MAX_DELAY_FOR_LAST_REAL = 2.25f;
    private const float MIN_DELAY_FOR_LAST_REAL = 1f;

    public event Action SpinStarted;
    public event Action SpinFinished;

    void Start()
    {
        _spinhandler.NextSpinResultConcluded += SetReelsTarget;
        foreach (var reel in _reels)
        {
            reel.Initialize(JsonSaver.LoadData<Dictionary<int, SlotSymbolData>>(JsonSaver.SYMBOL_DATA_PATH));
        }
    }
    private async UniTask StopReels()
    {
        await UniTask.WaitForSeconds(_totalSpinDuration);

        foreach (var reel in _reels)
        {
            await UniTask.WaitForSeconds(_stopDelayBetweenReels);
            if (reel != _reels[^1])
            {
                await reel.StopSpinAtTarget();
            }
            else // last reel
            {
                if (_currentResult.IsDelayNeeded)
                {
                    float randomDelay = Random.Range(MIN_DELAY_FOR_LAST_REAL, MAX_DELAY_FOR_LAST_REAL);
                    await reel.SlowDownSymbols(randomDelay);
                }
                else
                {
                    await reel.StopSpinAtTarget();
                }
            }
        }
        // Broadcast spin progress finish.
        SpinFinished?.Invoke();
    }

    private async UniTaskVoid StartReelsMovement()
    {
        SpinStarted?.Invoke();

        float randomDelay = Random.Range(MIN_START_DELAY, MAX_START_DELAY);
        foreach (var reel in _reels)
        {
            await UniTask.WaitForSeconds(randomDelay);
            reel.StartSpinning(_reelSymbolsTweenDuration);
        }

        await StopReels();
    }

    private void SetReelsTarget(ResultData currentResult)
    {
        _currentResult = currentResult;
        for (int i = 0; i < _reels.Length; i++)
        {
            _reels[i].SetTargetSymbol(currentResult.ResultSymbolsData[i]);
        }
        StartReelsMovement().Forget();
    }

    private void OnDestroy()
    {
        _spinhandler.NextSpinResultConcluded -= SetReelsTarget;
    }
}
