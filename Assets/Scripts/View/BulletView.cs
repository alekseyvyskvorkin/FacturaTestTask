using System;
using System.Collections;
using TestTask.Interfaces;
using TestTask.Settings;
using TestTask.Spawn;
using UnityEngine;

namespace TestTask.Views
{
    public class BulletView : MonoBehaviour, ISpawnable, IDamageDealer, IPoolable
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private ParticleSystem _mainParticles;

        public int Damage { get; set; }
        public float Speed { get; set; }
        public float LifeTime { get; set; }
        public Action OnSpawn { get; set; }
        public Action OnDespawn { get; set; }
        public PoolContainer Pool { get; set; }

        public void Initialize(BulletSettings settings, PoolContainer pool)
        {
            Speed = settings.Speed;
            Damage = settings.Damage;
            LifeTime = settings.LifeTime;

            Pool = pool;

            OnDespawn += ReturnToPool;
        }

        public void Shoot(Vector3 direction)
        {
            gameObject.SetActive(true);
            _rb.velocity = direction * Speed;
            transform.parent = Pool.SpawnParent;
            _mainParticles.Play();
            StartCoroutine(CDisable());
        }

        private IEnumerator CDisable()
        {
            yield return new WaitForSeconds(LifeTime);
            OnDespawn?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<EnemyView>(out var enemy))
            {
                enemy.TakeDamage(Damage);
                OnDespawn?.Invoke();
            }
        }

        public void ReturnToPool()
        {
            _mainParticles.Stop();
            gameObject.SetActive(false);
            Pool.ReturnBullet(this);
        }
    }
}