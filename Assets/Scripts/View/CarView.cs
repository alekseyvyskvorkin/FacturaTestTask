using System;
using System.Collections;
using TestTask.Abstractions;
using TestTask.Handlers;
using TestTask.Settings;
using UnityEngine;

namespace TestTask.Views
{
    public class CarView : BaseUnit
    {
        public Action OnFinish { get; set; }

        [field: SerializeField] public WeaponView Weapon { get; private set; }

        [SerializeField] private TrailRenderer[] _wheelsTrails;

        [SerializeField] private MeshRenderer[] _carRenderers;
        [SerializeField] private Material _destroyedCarMaterial;
        [SerializeField] private Material _baseCarMaterial;

        private float _maxSpeed;
        private float _stopForce;

        public void Initialize(CarSettings settings, ExecuteHandler executeHandler, Transform cameraTransform)
        {
            Health = settings.Health;
            MaxHealth = settings.Health;
            Speed = settings.Speed;
            _maxSpeed = Speed;
            _stopForce = settings.StopForceOnFinish;

            Weapon.Initialize();

            OnSpawn += Weapon.ResetAngle;
            OnSpawn += RestoreHealth;
            OnSpawn += OnRespawn;
            OnDie += Die;
            OnFinish += OnFinishLine;

            HealthSlider.Initialize(this, cameraTransform);
            ExecuteHandler = executeHandler;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            OnFinish = null;
        }

        public override void Execute()
        {
            HealthSlider.LookAtCamera();
            Move(transform.forward);
        }

        public override void Move(Vector3 direction)
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.deltaTime);
        }

        private void OnRespawn()
        {
            StopAllCoroutines();
            ChangeTrailActivity(true);
            SwitchMaterial(_baseCarMaterial);
            transform.position = Vector3.zero;
            Speed = _maxSpeed;
            ExecuteHandler.Add(this);
        }

        private void Die()
        {
            ChangeTrailActivity(false);
            SwitchMaterial(_destroyedCarMaterial);
            ExecuteHandler.Remove(this);
            DieParticles.Play();
        }

        private void OnFinishLine()
        {
            ChangeTrailActivity(false);
            StartCoroutine(CMoveOnFinish());
        }

        private IEnumerator CMoveOnFinish()
        {
            while (Speed > 0)
            {
                Move(transform.forward);
                Speed -= Time.deltaTime * _stopForce;
                yield return null;
            }
        }

        private void ChangeTrailActivity(bool value)
        {
            foreach (var trail in _wheelsTrails)
            {
                trail.Clear();
                trail.enabled = value;
            }
        }

        private void SwitchMaterial(Material material)
        {
            foreach (var renderer in _carRenderers)
            {
                renderer.material = material;
            }
        }
    }
}