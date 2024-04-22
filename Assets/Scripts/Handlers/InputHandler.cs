using System;
using UnityEngine;

namespace TestTask.Handlers
{
    public class InputHandler : MonoBehaviour
    {
        public Action OnClick;
        public Action<Vector2> OnTouchMove { get; set; }

        private Vector2 _previousPosition;

        private void OnDestroy()
        {
            OnTouchMove = null;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _previousPosition = (Vector2)Input.mousePosition;
                OnClick?.Invoke();
            }
            else if (Input.GetMouseButton(0))
            {
                if (_previousPosition != (Vector2)Input.mousePosition)
                {
                    OnTouchMove?.Invoke(((Vector2)Input.mousePosition - _previousPosition).normalized);
                    _previousPosition = (Vector2)Input.mousePosition;
                }
            }
        }
    }
}