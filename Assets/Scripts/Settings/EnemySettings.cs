using TestTask.Views;
using UnityEngine;

namespace TestTask.Settings
{
    [System.Serializable]
    public class EnemySettings
    {
        [field: SerializeField] public EnemyView EnemyPrefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 20f;
        [field: SerializeField] public float RotateSpeed { get; private set; } = 15f;
        [field: SerializeField] public float AgroDistance { get; private set; } = 30f;
        [field: SerializeField] public int Damage { get; private set; } = 1;
        [field: SerializeField] public int Health { get; private set; } = 2;


        [field: SerializeField] public int StartSpawnCount { get; private set; } = 10;
        [field: SerializeField] public float SpawnDelay { get; private set; } = 1f;
        [field: SerializeField] public float MinSpawnDistance { get; private set; } = 20f;
        [field: SerializeField] public float MaxSpawnDistance { get; private set; } = 50f;
    }
}