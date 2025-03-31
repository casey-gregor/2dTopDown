using UnityEngine;

namespace ProjectFiles.Scripts.Camera
{
    public sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Vector3 _offset;
        
        private void Start()
        {
            if (target == null)
            {
                Debug.LogError("Target not assigned to CameraFollow!", this);
                return;
            }
            
            _offset = transform.position - target.position;
        }

        private void LateUpdate()
        {
            if (target == null) {return;}
            Vector3 newPosition = target.position + _offset;
            transform.position = newPosition;
        }
    }
}