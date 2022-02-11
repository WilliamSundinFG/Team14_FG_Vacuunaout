using UnityEngine;
using UnityEngine.InputSystem;

namespace Team14
{
    public class CameraManager : MonoBehaviour
    {
        public bool UseControllerRotation = false;

        private CharacterController characterController;

        private void Awake()
        {
            Settings.InitSettings();
        }

        private void Start()
        {
            characterController = GetComponentInParent<CharacterController>();
        }

        private void LateUpdate()
        {
            if (UseControllerRotation)
                transform.localRotation = Quaternion.Euler(transform.eulerAngles.x + characterController.ControllerRotation.y, 0f, 0f);

            //float clampedY = Mathf.Clamp(transform.eulerAngles.x, -90f, 90f);
            //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, clampedY, 0f);
        }
    }
}