using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotator : MonoBehaviour
{
    [SerializeField, Range(0.1f, 1f)]
    private float rotateSpeed = 0.3f;

    [SerializeField, Range(0.1f, 100f)]
    private float zoomSpeed = 1f;

    private Vector3 preMousePos;

    void Update()
    {
        MouseUpdate();
    }

    private void MouseUpdate()
    {
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            preMousePos = Input.mousePosition;
        }

        MouseDrag(Input.mousePosition);
    }


    private void MouseDrag(Vector3 mousePos)
    {
        Vector3 diff = mousePos - preMousePos;

        if (diff.magnitude < Vector3.kEpsilon)
            return;

        if (Input.GetMouseButton(1))
        {
            RotateObject(new Vector2(-diff.y, diff.x) * rotateSpeed);
        }

        preMousePos = mousePos;
    }

    private void RotateObject(Vector2 angle)
    {
        transform.Rotate(Vector3.right, angle.x, Space.Self);
        transform.Rotate(Vector3.up, angle.y, Space.World);
    }
}
