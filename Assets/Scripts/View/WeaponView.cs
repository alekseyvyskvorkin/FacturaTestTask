using UnityEngine;

namespace TestTask.Views
{
    [System.Serializable]
    public class WeaponView
    {
        [field: SerializeField] public Transform SpawnBulletPosition { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; set; }

        [SerializeField] private Transform _pivot;
        [SerializeField] private float _maxAngle = 60;

        private Vector3 _baseWeaponRotation;
        private float _currentRotationY;

        public void Initialize()
        {
            _baseWeaponRotation = _pivot.localEulerAngles;
        }

        public void ResetAngle()
        {
            _pivot.localEulerAngles = _baseWeaponRotation;
            _currentRotationY = _baseWeaponRotation.y;
        }

        public void Rotate(Vector2 direction)
        {
            _currentRotationY += direction.x * RotateSpeed;
            _currentRotationY = Mathf.Clamp(_currentRotationY, -_maxAngle, _maxAngle);
            _pivot.localEulerAngles = new Vector3(_baseWeaponRotation.x, _currentRotationY, _baseWeaponRotation.z);
        }
    }
}