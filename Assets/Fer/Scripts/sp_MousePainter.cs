using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp_MousePainter : MonoBehaviour
{
    public Camera cam;
    [Space]
    public bool mouseSingleClick;
    [Space]
    public Color paintColor;

    public float radius = 1;
    public float strength = 1;
    public float hardness =1;

    void Update() {
        bool click;
        click = mouseSingleClick ? Input.GetMouseButtonDown(0) : Input.GetMouseButton(0);

        if (click){
            Vector3 position = Input.mousePosition;
            Ray ray = cam.ScreenPointToRay(position);
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, 100f)){
                Debug.DrawRay(ray.origin, hit.point - ray.origin, Color.red);
                transform.position = hit.point;
                sp_Paintable paintable = hit.collider.GetComponent<sp_Paintable>();
                if (paintable != null){
                    PaintManager.instance.paint(paintable,hit.point,radius,hardness,strength,paintColor);
                }
            }
        }
    }
}
