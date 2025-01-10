using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    [SerializeField] private float mouseSens;
    [SerializeField] private Transform playerMesh;
    private float xRotation, yRotation, cameraX = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update() 
    {
        CameraMovement();
    }

    private void CameraMovement()
    {
        xRotation =  Input.GetAxisRaw("Mouse X") * mouseSens * Time.deltaTime;
        yRotation =  Input.GetAxisRaw("Mouse Y") * mouseSens * Time.deltaTime;

        cameraX -= yRotation;

        cameraX = Mathf.Clamp(cameraX, -90f, 90f);

        playerMesh.Rotate(Vector3.up * xRotation);
        transform.localRotation = Quaternion.Euler(cameraX, 0f, 0f); 
    }
}
