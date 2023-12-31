using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSentitivity = 100f;
    public Transform player;
    private float xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

 
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSentitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSentitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        player.Rotate(Vector3.up * mouseX);

    }
}
