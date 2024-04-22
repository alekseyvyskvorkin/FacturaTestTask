using UnityEngine;
using Zenject;

namespace TestTask.Views
{
    [RequireComponent(typeof(BoxCollider))]
    public class FinishLineView : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<CarView>(out var car))
            {
                car.OnFinish?.Invoke();
            }
        }
    }
}

