using DG.Tweening;
using UnityEngine;

namespace MyExtensions.TransformExtension
{
    public class ScaleLoopComponent : MonoBehaviour
    {
        [SerializeField] private float _targetScale = 1.5f;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private Ease _easeType = Ease.Linear;

        private float _defaultScale;

        private void Awake()
        {
            _defaultScale = transform.localScale.x;
        }
        private void StartScaleLoop()
        {
            transform.DOScale(_targetScale, _duration).SetEase(_easeType).SetLoops(-1, LoopType.Yoyo);
        }

        private void OnEnable()
        {
            StartScaleLoop();
        }

        private void OnDisable()
        {
            transform.DOKill();
            transform.localScale = Vector3.one * _defaultScale;
        }
    }

}
