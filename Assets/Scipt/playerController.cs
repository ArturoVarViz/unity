using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace YourNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rb;
        private Animator animator; // Referencia al componente Animator
        public float idleSpeed = 0; // Velocidad en estado de reposo
        public float moveSpeed = 10; // Velocidad en estado de movimiento
        private float currentSpeed; // Velocidad actual
        private int count = 0;
        private Vector2 movementVector;
        public Camera mainCamera;
        public float jumpForce = 9.8f;
        public TextMeshProUGUI countText;
        public GameObject winTextObject; // Objeto de texto para mostrar el mensaje de victoria
        public Transform playerTransform; // Transform del jugador
        public Transform enemyTransform; // Transform del enemigo
        private bool isCloseToEnemy = false; // Variable que indica si el jugador está cerca del enemigo
        private bool isMoving = false; // Variable de estado para controlar si el jugador está en movimiento
        private bool isjump = false;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>(); // Obtener la referencia al Animator
            mainCamera = Camera.main;
            SetCountText(); // Llama al método para actualizar el texto del recuento al inicio
            winTextObject.SetActive(false); // Desactiva el mensaje de victoria al inicio
            currentSpeed = idleSpeed; // Inicialmente establecemos la velocidad actual como la velocidad de reposo
        }

        void OnMove(InputValue movementValue)
        {
            movementVector = movementValue.Get<Vector2>();

            // Si hay movimiento, cambiamos la velocidad actual a la velocidad de movimiento
            if (movementVector.magnitude > 0)
            {
                isMoving = true;
                currentSpeed = moveSpeed;
            }
            else
            {
                isMoving = false;
                currentSpeed = idleSpeed;
            }

            // Actualizar el booleano "IsMoving" en el Animator
            animator.SetBool("IsMoving", isMoving);
        }

        void OnFire()
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        void FixedUpdate()
        {
            // Solo aplicamos fuerza si estamos en movimiento
            if (isMoving)
            {
                Vector3 moveDirection = mainCamera.transform.forward;
                moveDirection.y = 0;
                moveDirection.Normalize();

                Vector3 movement = moveDirection * movementVector.y + mainCamera.transform.right * movementVector.x;
                rb.AddForce(movement * currentSpeed);
            }

            // Verifica la distancia al enemigo
            CheckDistanceToEnemy();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("coins"))
            {
                count++;
                other.gameObject.SetActive(false);
                SetCountText(); // Actualiza el texto del recuento cuando se recolecta una moneda
            }
            else if (other.gameObject.CompareTag("poison"))
            {
                moveSpeed /= 2;
                // Desactiva el objeto "poison"
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.CompareTag("power"))
            {
                // Desactiva el objeto "power"
                other.gameObject.SetActive(false);
                // Aumenta la velocidad del jugador
                moveSpeed *= 5; // Aumenta la velocidad actual (puedes ajustar el multiplicador según tu preferencia)
            }
        }

        void SetCountText()
        {
            countText.text = "Count: " + count.ToString();
            if (count >= 1)
            {
                winTextObject.SetActive(true); // Activa el mensaje de victoria cuando se alcanza el recuento requerido
            }
        }

        void CheckDistanceToEnemy()
        {
            // Calcula la distancia entre el jugador y el enemigo
            float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

            // Verifica si la distancia es menor que 10 unidades
            if (distance < 10)
            {
                // Si la distancia es menor que 10 unidades, establece la variable isCloseToEnemy como true
                isCloseToEnemy = true;
            }
            else
            {
                // Si la distancia no es menor que 10 unidades, establece la variable isCloseToEnemy como false
                isCloseToEnemy = false;
            }

            // Actualizar el booleano "enemy" en el Animator
            animator.SetBool("enemy", isCloseToEnemy);
        }
        void OnJump()
        {
            isjump = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetBool("salto", isjump);
        }
    }

}
  