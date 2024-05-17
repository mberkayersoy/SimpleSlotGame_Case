using MyExtensions.ObjectPool;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PaymentController : MonoBehaviour
{
    [SerializeField] private PoolableCoinParticle _coinPrefab;
    [Inject] private UIReelsController _reelsController;
    [Inject] private PoolManager _poolManager;

    private void Awake()
    {
        _reelsController.SpinDone += RewardPlayer;
    }

    private void RewardPlayer(bool spinDone)
    {
        if (_reelsController.CurrentResult.IsPaying)
        {
            var coin = _poolManager.Spawn(_coinPrefab, transform.position, transform.rotation);
            coin.SetFrequency(_reelsController.CurrentResult.ChancePer);
        }
    }
    private void OnDestroy()
    {
        _reelsController.SpinDone -= RewardPlayer;
    }
}
