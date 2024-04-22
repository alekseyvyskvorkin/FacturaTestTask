using Cinemachine;
using UnityEngine;

namespace TestTask.Handlers
{
    public class CameraHandler : MonoBehaviour
    {
        [field: SerializeField] public CinemachineVirtualCamera StartCamera { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera FollowerCamera { get; private set; }

        public void SetTarget(Transform target)
        {
            StartCamera.LookAt = target;
            FollowerCamera.Follow = target;
        }

        public void OnPrepareToPlay()
        {
            StartCamera.gameObject.SetActive(true);
            FollowerCamera.gameObject.SetActive(false);
        }

        public void OnStartPlay()
        {
            StartCamera.gameObject.SetActive(false);
            FollowerCamera.gameObject.SetActive(true);
        }
    }
}

