using TestTask.Views;
using UnityEngine;

namespace TestTask.Settings
{
    [System.Serializable]
    public class BulletSettings
    {
        [field: SerializeField] public BulletView BulletPrefab { get; private set; }
        [field: SerializeField] public int Damage { get; private set; } = 1;
        [field: SerializeField] public float LifeTime { get; private set; } = 7f;
        [field: SerializeField] public float Speed { get; private set; } = 20f;
        [field: SerializeField] public float ShootDelay { get; private set; } = 0.5f;
    }
}