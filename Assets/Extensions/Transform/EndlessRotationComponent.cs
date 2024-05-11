using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace MyExtensions.TransformExtension
{
    public class EndlessRotationComponent : MonoBehaviour
    {
        [SerializeField] private float _duration = 1f;
        [SerializeField] private Vector3 _endValue = new Vector3(0f, 360f, 0f);
        [SerializeField] private Ease _easeType = Ease.Linear;
        [SerializeField] private RotateMode _rotateMode = RotateMode.LocalAxisAdd;
        private void OnDisable()
        {
            transform.DOKill();
            transform.ResetRotation();
        }

        private void OnEnable()
        {
            Rotate();
        }
        private void Rotate()
        {
            transform.DORotate(_endValue, _duration, _rotateMode).
                SetEase(_easeType).
                SetLoops(-1);
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }
    }
}

