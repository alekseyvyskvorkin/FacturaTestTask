using UnityEngine;

namespace TestTask.Settings
{
    [CreateAssetMenu(fileName = "Config", menuName = "Data/Config")]
    public class Config : ScriptableObject
    {
        [field: SerializeField] public float HalfRoadSize { get; private set; } = 3f;
        [field: SerializeField] public CarSettings CarSettings { get; private set; }
        [field: SerializeField] public BulletSettings BulletSettings { get; private set; }
        [field: SerializeField] public EnemySettings EnemySettings { get; private set; }
    }
}