using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private float movementX;
    private float movementY;

    [Range(5f, 10f)]
    public float movementSpeed;

    public CharacterController controller;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 5f;
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 inputValue = movementValue.Get<Vector2>();

        movementX = inputValue.x;
        movementY = inputValue.y;
    }

    private void FixedUpdate()
    {
        Vector3 movement = (transform.right * movementX) + (transform.forward * movementY);

        controller.Move(movement * movementSpeed * Time.deltaTime);
    }
}
