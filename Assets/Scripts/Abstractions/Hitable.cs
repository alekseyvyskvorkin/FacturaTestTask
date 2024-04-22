using System;
using TestTask.Interfaces;
using TestTask.UI;
using UnityEngine;

namespace TestTask.Abstractions
{
    public abstract class Hitable : MonoBehaviour, IHitable
    {
        [field: SerializeField] public HealthSlider HealthSlider { get; private set; }

        public Action OnDie { get; set; }
        public Action<int> OnTakeDamage { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }

        public bool IsDead => Health <= 0;

        public void RestoreHealth() => Health = MaxHealth;

        public void TakeDamage(int damage)
        {
            if (IsDead) return;

            Health -= damage;

            OnTakeDamage?.Invoke(damage);

            if (Health <= 0)
            {
                OnDie?.Invoke();
            }
        }
    }
}
