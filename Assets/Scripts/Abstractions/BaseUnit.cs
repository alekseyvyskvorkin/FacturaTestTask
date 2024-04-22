using System;
using TestTask.Handlers;
using TestTask.Interfaces;
using UnityEngine;

namespace TestTask.Abstractions
{
    public abstract class BaseUnit : Hitable, ISpawnable, IExecutable, IMovable
    {
        [field: SerializeField] public ParticleSystem DieParticles { get; private set; }

        public Action OnSpawn { get; set; }
        public Action OnDespawn { get; set; }

        public float Speed { get; set; }

        public ExecuteHandler ExecuteHandler { get; set; }

        public virtual void OnDestroy()
        {
            OnSpawn = null;
            OnDespawn = null;
            OnDie = null;
        }

        public abstract void Execute();
        public abstract void Move(Vector3 direction);
    }
}