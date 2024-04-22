using UnityEngine;
using System.Collections;
using TestTask.Interfaces;
using TestTask.Abstractions;
using TestTask.Spawn;
using TestTask.Settings;
using TestTask.Handlers;

namespace TestTask.Views
{
    public class EnemyView : BaseUnit, IDamageDealer, IPoolable
    {
        private const string RunAnimation = "Run";
        private const string DieAnimation = "Die";
        private const float DisableTime = 5f;

        public PoolContainer Pool { get; set; }
        public int Damage { get; set; }

        [SerializeField] private SkinnedMeshRenderer _mesh;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _collider;

        private CarView _target;

        private float _agroDistance;
        private float _rotateSpeed;

        public void Initialize(CarView car, EnemySettings enemySettings, PoolContainer pool, ExecuteHandler executeHandler, Transform cameraTransform)
        {
            Health = enemySettings.Health;
            MaxHealth = Health;
            Speed = enemySettings.Speed;
            Damage = enemySettings.Damage;
            _agroDistance = enemySettings.AgroDistance;
            _rotateSpeed = enemySettings.RotateSpeed;

            OnSpawn += RestoreHealth;
            OnSpawn += OnRespawn;
            OnDespawn += ReturnToPool;
            OnDie += Die;

            HealthSlider.Initialize(this, cameraTransform);

            _target = car;
            Pool = pool;
            ExecuteHandler = executeHandler;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CarView>(out var car))
            {
                car.TakeDamage(Damage);
                OnDie?.Invoke();
            }
        }

        public override void Execute()
        {
            HealthSlider.LookAtCamera();

            if (Vector3.Distance(_target.transform.position, transform.position) > _agroDistance) return;

            Move(_target.transform.position - transform.position);
        }

        public override void Move(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, _rotateSpeed * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, Speed * Time.deltaTime);

            _animator.SetBool(RunAnimation, true);
        }

        public void ReturnToPool()
        {
            Pool.ReturnEnemy(this);
            gameObject.SetActive(false);
        }

        public void Stop()
        {
            _animator.SetBool(RunAnimation, false);
        }

        private void OnRespawn()
        {
            gameObject.SetActive(true);
            ExecuteHandler.Add(this);
            _animator.Rebind();
            _mesh.enabled = true;
            _collider.enabled = true;
        }

        private void Die()
        {
            ExecuteHandler.Remove(this);
            _animator.SetBool(RunAnimation, false);
            _animator.SetTrigger(DieAnimation);
            _collider.enabled = false;
            _mesh.enabled = false;
            DieParticles.Play();
            StartCoroutine(CDisable());
        }

        private IEnumerator CDisable()
        {
            yield return new WaitForSeconds(DisableTime);
            OnDespawn?.Invoke();
        }
    }
}