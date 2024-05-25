using MyExtensions.ObjectPool;
using UnityEngine;
using Zenject;

public class PoolableCoinParticle : PoolableComponent
{
    [Inject] private PoolManager _poolManager;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    public void SetFrequency(float burstCount)
    {
        var emission = _particleSystem.emission;

        float invertedBurstCount = 1000f / Mathf.Clamp(burstCount, 1f, 100f);
        var burst = new ParticleSystem.Burst(0.0f, (short)invertedBurstCount);

        emission.SetBursts(new ParticleSystem.Burst[] { burst });
    }
    private void OnParticleSystemStopped()
    {
        _poolManager.Despawn(this);
    }
}
