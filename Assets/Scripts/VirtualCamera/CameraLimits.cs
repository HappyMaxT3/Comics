using UnityEngine;
using Cinemachine;

namespace Main
{
    public class CameraLimitsWithCinemachine : MonoBehaviour
    {
        [SerializeField] private float topLimit;
        [SerializeField] private float bottomLimit;
        [SerializeField] private float leftLimit;
        [SerializeField] private float rightLimit;

        private CinemachineVirtualCamera virtualCamera;
        private Transform cameraTransform;

        private void Awake()
        {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            if (virtualCamera != null)
            {
                cameraTransform = virtualCamera.transform;
            }
        }

        private void LateUpdate()
        {
            if (cameraTransform != null)
            {
                Vector3 position = cameraTransform.position;
                position.x = Mathf.Clamp(position.x, leftLimit, rightLimit);
                position.y = Mathf.Clamp(position.y, bottomLimit, topLimit);
                cameraTransform.position = position;
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
            Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
            Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, bottomLimit));
            Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
        }
    }
}
