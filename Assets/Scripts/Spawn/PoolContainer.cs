using System.Collections.Generic;
using TestTask.Handlers;
using TestTask.Settings;
using TestTask.Views;
using UnityEngine;
using Zenject;

namespace TestTask.Spawn
{
    public class PoolContainer : MonoBehaviour
    {
        [field: SerializeField] public Transform SpawnParent { get; private set; }

        public CarView Car { get; private set; }

        private Queue<EnemyView> _enemies = new Queue<EnemyView>();
        private Queue<BulletView> _bullets = new Queue<BulletView>();

        private List<EnemyView> _activatedEnemies = new List<EnemyView>();
        private List<BulletView> _activatedBullets = new List<BulletView>();

        private ExecuteHandler _executeHandler;
        private CameraHandler _cameraHandler;
        private Factory _factory;
        private Config _config;

        [Inject]
        public void Initialize(Config config, Factory factory, ExecuteHandler executeHandler, CameraHandler cameraHandler)
        {
            _executeHandler = executeHandler;
            _cameraHandler = cameraHandler;
            _config = config;
            _factory = factory;
        }

        public CarView CreateCar()
        {
            Car = _factory.Create<CarView>(_config.CarSettings.CarPrefab);
            Car.Initialize(_config.CarSettings, _executeHandler, _cameraHandler.FollowerCamera.transform);
            Car.OnDie += StopEnemies;
            Car.transform.parent = SpawnParent;
            return Car;
        }

        public EnemyView GetEnemy()
        {
            EnemyView enemy = null;
            if (_enemies.Count > 0)
                enemy = _enemies.Dequeue();
            else
            {
                enemy = _factory.Create<EnemyView>(_config.EnemySettings.EnemyPrefab);
                enemy.Initialize(Car, _config.EnemySettings, this, _executeHandler, _cameraHandler.FollowerCamera.transform);
                enemy.transform.parent = SpawnParent;
            }

            _activatedEnemies.Add(enemy);
            return enemy;
        }

        public BulletView GetBullet()
        {
            BulletView bullet = null;
            if (_bullets.Count > 0)
                bullet = _bullets.Dequeue();
            else
            {
                bullet = _factory.Create<BulletView>(_config.BulletSettings.BulletPrefab);
                bullet.Initialize(_config.BulletSettings, this);
                bullet.transform.parent = SpawnParent;
            }

            _activatedBullets.Add(bullet);
            return bullet;
        }

        public void ReturnEnemy(EnemyView enemy)
        {
            _activatedEnemies.Remove(enemy);
            _enemies.Enqueue(enemy);
            _executeHandler.Remove(enemy);
        }

        public void ReturnBullet(BulletView bullet)
        {
            _activatedBullets.Remove(bullet);
            _bullets.Enqueue(bullet);
        }

        public void DeactivateOldEnemies()
        {
            var enemies = new List<EnemyView>();
            enemies.AddRange(_activatedEnemies);
            foreach (var enemy in enemies)
            {
                enemy.gameObject.SetActive(false);
                ReturnEnemy(enemy);
            }
        }

        private void StopEnemies()
        {
            foreach (var enemy in _activatedEnemies)
            {
                enemy.Stop();
            }
        }
    }
}