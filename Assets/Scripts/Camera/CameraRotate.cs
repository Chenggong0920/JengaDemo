using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CameraRotate : MonoBehaviour
{
    public Vector3 rotateCenter;
    public float rotateSpeed = 120f;

    void Update()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");

            Vector3 rotateAxis = new Vector3(0, 1, 0); // rotate around Y axis
            transform.RotateAround(rotateCenter, rotateAxis, mouseX * Time.deltaTime * rotateSpeed);
            transform.RotateAround(rotateCenter, transform.right, -mouseY * Time.deltaTime * rotateSpeed);
        }
    }

    public void OnStackSelected(int stackIndex)
    {
        rotateCenter = new Vector3(BlockManager.Instance.initialPosition_ZAxis.x + stackIndex * BlockManager.Instance.stack_offset, 0f, 0f);
    }
}