using System;
using UnityEngine;

namespace TestTask.Interfaces
{
    public interface ISpawnable
    {       
        public Action OnSpawn { get; set; }
        public Action OnDespawn { get; set; }
    }
}


