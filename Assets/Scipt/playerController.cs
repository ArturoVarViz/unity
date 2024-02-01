using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed = 10;
    private Vector2 movementVector;
    public Camera mainCamera;  // Referencia a la cámara principal

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;  // Asegúrate de que tu cámara principal esté etiquetada como "MainCamera" en Unity
    }

    void OnMove(InputValue movementValue)
    {
        movementVector = movementValue.Get<Vector2>();
    }

    void OnFire()  // Corrected the method name to follow C# conventions
    {
        rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);  // Corrected the spelling of Impulse
    }

    void FixedUpdate()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            OnFire();
        }

        // Calcula la dirección de movimiento basada en la rotación de la cámara
        Vector3 moveDirection = mainCamera.transform.forward;
        moveDirection.y = 0;  // Esto asegura que el personaje no se mueva hacia arriba o hacia abajo
        moveDirection.Normalize();  // Esto hace que la longitud del vector sea 1, manteniendo la dirección

        // Aplica la fuerza en la dirección calculada
        Vector3 movement = moveDirection * movementVector.y + mainCamera.transform.right * movementVector.x;
        rb.AddForce(movement * speed);
    }
}