using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.CameraUI
{
    public class CameraZoom : MonoBehaviour
    {
        [SerializeField] float minFov = 15f;
        [SerializeField] float maxFov = 90f;
        [SerializeField] float sensitivity = 10f;

        void Update()
        {
            float fov = Camera.main.fieldOfView;
            fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
            fov = Mathf.Clamp(fov, minFov, maxFov);
            Camera.main.fieldOfView = fov;
        }
    }

}