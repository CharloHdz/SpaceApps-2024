using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class SceneViewCamera : MonoBehaviour
{
    [SerializeField, Range(0.1f, 100f)]
    private float wheelSpeed = 1f;

    [SerializeField, Range(0.1f, 100f)]
    private float moveSpeed = 0.3f;

    [SerializeField, Range(0.1f, 1f)]
    private float rotateSpeed = 0.3f;

    private Vector3 preMousePos;

    private void Update()
    {
        MouseUpdate();
        return;
    }

    private void MouseUpdate()
    {
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheel != 0.0f)
            MouseWheel(scrollWheel);

        if (Input.GetMouseButtonDown(0) ||
           Input.GetMouseButtonDown(1) ||
           Input.GetMouseButtonDown(2))
            preMousePos = Input.mousePosition;

    }

    private void MouseWheel(float delta)
    {
        transform.position += transform.forward * delta * wheelSpeed;
        return;
    }
}
