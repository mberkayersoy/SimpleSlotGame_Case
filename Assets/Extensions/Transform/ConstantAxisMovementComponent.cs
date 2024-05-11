using DG.Tweening;
using UnityEngine;

namespace MyExtensions.TransformExtension
{
    public class ConstantAxisMovementComponent : MonoBehaviour
    {
        [SerializeField] private bool _isLocalMovement = false;
        [SerializeField] private float _movementAmount = 1f;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private DirectionAxis _axis = DirectionAxis.Y;
        [SerializeField] private Ease _easeType = Ease.Linear;
        [SerializeField] private LoopType _loopType = LoopType.Yoyo;

        private Vector3 _targetPosition;
        private Vector3 _initialPosition;

        [System.Serializable]
        private enum DirectionAxis
        {
            X,
            Y,
            Z
        }

        private void OnEnable()
        {
            CalculateTargetPosition();
            Move();
        }

        private void Move()
        {
            transform.DOMove(_targetPosition, _duration)
                .SetEase(_easeType)
                .SetLoops(-1, _loopType);
        }

        private void CalculateTargetPosition()
        {
            _initialPosition = _isLocalMovement ? transform.localPosition : transform.position;

            switch (_axis)
            {
                case DirectionAxis.X:
                    _targetPosition = _initialPosition + GetAxisVector(Vector3.right) * _movementAmount;
                    break;
                case DirectionAxis.Y:
                    _targetPosition = _initialPosition + GetAxisVector(Vector3.up) * _movementAmount;
                    break;
                case DirectionAxis.Z:
                    _targetPosition = _initialPosition + GetAxisVector(Vector3.forward) * _movementAmount;
                    break;
            }
        }

        private Vector3 GetAxisVector(Vector3 axisDirection)
        {
            return _isLocalMovement ? transform.TransformDirection(axisDirection) : axisDirection;
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }

}
