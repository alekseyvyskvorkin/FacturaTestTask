using System.Collections.Generic;
using TestTask.Interfaces;
using UnityEngine;
using System;

namespace TestTask.Handlers
{
    public class ExecuteHandler : MonoBehaviour
    {
        private Dictionary<Type, List<IExecutable>> _executables = new Dictionary<Type, List<IExecutable>>();

        private void Update()
        {
            foreach (var executables in _executables.Values)
            {
                foreach (var executable in executables)
                {
                    executable.Execute();
                }
            }
        }

        public void Add(IExecutable executable)
        {
            if (_executables.TryGetValue(executable.GetType(), out var executables))
            {
                if (!executables.Contains(executable))
                    executables.Add(executable);
            }
            else if (!_executables.ContainsKey(executable.GetType()))
            {
                var list = new List<IExecutable>() { executable };
                _executables.Add(executable.GetType(), list);
            }
        }

        public void Remove(IExecutable executable)
        {
            _executables[executable.GetType()].Remove(executable);
        }
    }
}