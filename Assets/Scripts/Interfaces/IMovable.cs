using UnityEngine;

namespace TestTask.Interfaces
{
    public interface IMovable
    {
        public float Speed { get; set; }

        public void Move(Vector3 direction);
    }

}