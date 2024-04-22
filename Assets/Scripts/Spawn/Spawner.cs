using UnityEngine;
using Zenject;
using System.Collections;
using TestTask.Handlers;
using TestTask.Views;
using TestTask.Settings;

namespace TestTask.Spawn
{
    public class Spawner : MonoBehaviour
    {
        private PoolContainer _poolContainer;
        private CameraHandler _cameraHandler;
        private FinishLineView _finishLine;
        private Config _config;

        private int _startSpawnCount;
        private float _maxSpawnDistance;
        private float _minSpawnDistance;
        private float _halfRoadSize;

        [Inject]
        private void Initialize(PoolContainer poolContainer,
            Config config,
            CameraHandler cameraHandler,
            FinishLineView finishLine)
        {
            _poolContainer = poolContainer;
            _cameraHandler = cameraHandler;
            _config = config;
            _finishLine = finishLine;

            _startSpawnCount = _config.EnemySettings.StartSpawnCount;
            _minSpawnDistance = _config.EnemySettings.MinSpawnDistance;
            _maxSpawnDistance = _config.EnemySettings.MaxSpawnDistance;
            _halfRoadSize = _config.HalfRoadSize;
        }

        private void Start()
        {
            var car = CreateCar();
            InitializeCameras(car);
            SpawnEnemyAtStartPlay();
        }

        public void StartSpawn()
        {
            StartCoroutine(SpawnEnemiesRoutine());
            StartCoroutine(SpawnBulletsRoutine());
        }

        public CarView CreateCar()
        {
            var car = _poolContainer.CreateCar();
            car.OnSpawn?.Invoke();
            car.OnDie += StopSpawn;
            car.OnFinish += StopSpawn;
            return car;
        }

        private void InitializeCameras(CarView car)
        {
            _cameraHandler.SetTarget(car.transform);
            _cameraHandler.OnPrepareToPlay();
        }

        public void SpawnEnemyAtStartPlay()
        {
            for (int i = 0; i < _startSpawnCount; i++)
            {
                var positionX = Random.Range(-_halfRoadSize, _halfRoadSize);
                Vector3 spawnPosition = new Vector3(positionX, 0, Mathf.Lerp(_minSpawnDistance, _maxSpawnDistance, (float)i / _startSpawnCount));
                var enemy = _poolContainer.GetEnemy();
                enemy.OnSpawn?.Invoke();
                enemy.transform.position = spawnPosition;
            }
        }

        private IEnumerator SpawnEnemiesRoutine()
        {
            yield return new WaitForSeconds(_config.EnemySettings.SpawnDelay);
            var positionX = Random.Range(-_halfRoadSize, _halfRoadSize);
            Vector3 spawnPosition = new Vector3(positionX, 0, _poolContainer.Car.transform.position.z + _maxSpawnDistance);
            if (Vector3.Distance(spawnPosition, _finishLine.transform.position) <= _minSpawnDistance) yield break;
            var enemy = _poolContainer.GetEnemy();
            enemy.transform.position = spawnPosition;
            enemy.OnSpawn?.Invoke();
            StartCoroutine(SpawnEnemiesRoutine());
        }

        private IEnumerator SpawnBulletsRoutine()
        {
            yield return new WaitForSeconds(_config.BulletSettings.ShootDelay);
            var bullet = _poolContainer.GetBullet();
            bullet.transform.parent = _poolContainer.Car.Weapon.SpawnBulletPosition;
            bullet.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
            bullet.Shoot(_poolContainer.Car.Weapon.SpawnBulletPosition.forward);
            bullet.OnSpawn?.Invoke();
            StartCoroutine(SpawnBulletsRoutine());
        }

        private void StopSpawn()
        {
            StopAllCoroutines();
        }
    }
}