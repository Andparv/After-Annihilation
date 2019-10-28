using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    Vector3 lastDragPosition;

    float minSize = 1f;
    float maxSize = 5f;
    float sensitivity = 2f;

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

        float size = Camera.main.orthographicSize;
        size += Input.GetAxis("Mouse ScrollWheel") * sensitivity;
        size = Mathf.Clamp(size, minSize, maxSize);
        Camera.main.orthographicSize = size;
    }
}
