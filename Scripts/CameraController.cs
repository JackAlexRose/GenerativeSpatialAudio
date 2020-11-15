using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    private float yRotation;

    public Transform charBody;
    //blag
    [Range(1f, 30f)]
    public float mouseSensitivity;

    void OnLook(InputValue mouseMovementValue)
    {
        Vector2 inputValue = mouseMovementValue.Get<Vector2>();

        mouseX = inputValue.x * mouseSensitivity * Time.deltaTime;
        mouseY = inputValue.y * mouseSensitivity * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        charBody.Rotate(Vector3.up * mouseX);

        yRotation -= mouseY;
        yRotation = Mathf.Clamp(yRotation, -90f, 46f);

        transform.localRotation = Quaternion.Euler(yRotation, 0f, 0f);
    }
}
