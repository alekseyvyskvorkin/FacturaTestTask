using System;

namespace TestTask.Interfaces
{
    public interface IHitable
    {
        public Action OnDie { get; set; }
        public Action<int> OnTakeDamage { get; set; }

        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public bool IsDead { get; }

        public void TakeDamage(int damage);
    }
}