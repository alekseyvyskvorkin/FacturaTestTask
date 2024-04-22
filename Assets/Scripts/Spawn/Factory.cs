using UnityEngine;
using Zenject;

namespace TestTask.Spawn
{
    public class Factory
    {
        private DiContainer _diContainer;

        public Factory(DiContainer diContainer)
        {
            _diContainer = diContainer;
        }

        public T Create<T>(MonoBehaviour monoBehaviour) where T : MonoBehaviour
        {
            return _diContainer.InstantiatePrefabForComponent<T>(monoBehaviour);
        }
    }
}