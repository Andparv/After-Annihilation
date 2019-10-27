using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Vector3 lastDragPosition;

    float minFov = 15f;
    float maxFov = 90f;
    float sensitivity = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
            lastDragPosition = Input.mousePosition;
        if (Input.GetMouseButton(1))
        {
            var delta = lastDragPosition - Input.mousePosition;
            transform.Translate(delta * Time.deltaTime * 0.75f);
            lastDragPosition = Input.mousePosition;
        }

        float fov = Camera.main.fieldOfView;
        fov += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFov);
        Camera.main.fieldOfView = fov;
    }
}
