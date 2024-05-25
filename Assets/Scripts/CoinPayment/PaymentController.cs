using MyExtensions.ObjectPool;
using UnityEngine;
using Zenject;

public class PaymentController : MonoBehaviour
{
    [SerializeField] private PoolableCoinParticle _coinPrefab;
    [Inject] private ISpinState _spinState;
    [Inject] private PoolManager _poolManager;
    [Inject] private ISpinHandler _iSpinResult;
    private ResultData _currentResult;
    private void Awake()
    {
        _spinState.SpinFinished += RewardPlayer;
        _iSpinResult.NextSpinResultConcluded += SpinHandler_NextSpinResultConcluded;
    }
    private void SpinHandler_NextSpinResultConcluded(ResultData currentResult)
    {
        _currentResult = currentResult;
    }
    private void RewardPlayer()
    {
        if (_currentResult.IsPaying)
        {
            var coin = _poolManager.Spawn(_coinPrefab, transform.position, transform.rotation);
            coin.SetFrequency(_currentResult.ChancePer);
        }
    }
    private void OnDestroy()
    {
        _spinState.SpinFinished -= RewardPlayer;
        _iSpinResult.NextSpinResultConcluded -= SpinHandler_NextSpinResultConcluded;
    }
}
