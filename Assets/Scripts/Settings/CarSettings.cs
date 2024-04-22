using TestTask.Views;
using UnityEngine;

namespace TestTask.Settings
{
    [System.Serializable]
    public class CarSettings
    {
        [field: SerializeField] public CarView CarPrefab { get; private set; }
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float StopForceOnFinish { get; private set; } = 1f;
        [field: SerializeField] public int Health { get; private set; } = 20;
    }
}