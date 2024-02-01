using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float rotationSpeed = 3.0f;  // Velocidad de rotación

    private Vector3 offset;
    private bool isFirstPerson = false;  // Añade esta línea
    private bool isRotating = false; 

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not set in the CameraController script.");
            return;
        }

        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Player reference is not set in the CameraController script.");
            return;
        }
         
        // Comprueba si se ha pulsado la tecla 'r'
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            // Cambia el estado de la rotación
            isRotating = !isRotating;
        }

        // Si está rotando, rota la cámara alrededor del jugador
        if (isRotating)
        {
            Quaternion rotation = Quaternion.AngleAxis(rotationSpeed, Vector3.up);
            offset = rotation * offset;
        }

        // Aplicar la nueva posición de la cámara
        transform.position = player.transform.position + offset;

        // Hacer que la cámara siempre mire al jugador
        transform.LookAt(player.transform.position);
        
        // Comprueba si se ha pulsado la tecla 'C'
        if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            // Cambia el estado de la vista en primera persona
            isFirstPerson = !isFirstPerson;
        }

        // Si está en primera persona, coloca la cámara en la posición del jugador y ajusta la rotación X a 0
        if (isFirstPerson)
        {
            transform.position = player.transform.position;
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            // Obtener la entrada horizontal para rotar la cámara usando J y L
            float horizontalInput = 0.0f;
            if (Keyboard.current.aKey.isPressed)
            {
                horizontalInput = -1.0f;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                horizontalInput = 1.0f;
            }

            // Calcular la nueva rotación de la cámara solo si hay entrada horizontal
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                Quaternion rotation = Quaternion.AngleAxis(horizontalInput * rotationSpeed, Vector3.up);
                transform.rotation = rotation * transform.rotation;
            }
        }
        else
        {
            // Obtener la entrada horizontal para rotar la cámara usando J y L
            float horizontalInput = 0.0f;
            if (Keyboard.current.aKey.isPressed)
            {
                horizontalInput = -1.0f;
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                horizontalInput = 1.0f;
            }

            // Calcular la nueva posición de la cámara alrededor del jugador solo si hay entrada horizontal
            if (Mathf.Abs(horizontalInput) > 0.1f)
            {
                Quaternion rotation = Quaternion.AngleAxis(horizontalInput * rotationSpeed, Vector3.up);
                offset = rotation * offset;
            }

            // Aplicar la nueva posición de la cámara
            transform.position = player.transform.position + offset;
        }

        // Hacer que la cámara siempre mire al jugador
        transform.LookAt(player.transform.position);
    }

    void Update()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            // Mueve el personaje hacia adelante a una velocidad determinada
            float speed = 5.0f;  // Ajusta la velocidad a tu gusto
            player.transform.position += transform.forward * speed * Time.deltaTime;
        }
    }
}
