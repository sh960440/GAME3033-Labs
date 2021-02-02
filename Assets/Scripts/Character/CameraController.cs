using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject followTarget;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] private float horizontalDamping = 1f;

        private Transform followTargetTransform;

        private Vector2 PreviousMouseInput;

        // Start is called before the first frame update
        void Start()
        {
            followTargetTransform = followTarget.transform;
            PreviousMouseInput = Vector2.zero;
        }

        public void OnLook(InputValue delta)
        {
            Vector2 aimValue = delta.Get<Vector2>();
            
            // Rotate the camera
            followTargetTransform.rotation *= Quaternion.AngleAxis(Mathf.Lerp(PreviousMouseInput.x, aimValue.x, 1f / horizontalDamping) * rotationSpeed, transform.up);

            // Rotate the player
            transform.rotation = Quaternion.Euler(0, followTargetTransform.transform.rotation.eulerAngles.y, 0);

            followTargetTransform.localEulerAngles = Vector3.zero;

            PreviousMouseInput = aimValue;
        }
    }
}