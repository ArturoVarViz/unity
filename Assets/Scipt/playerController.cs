using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

namespace YourNamespace
{
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody rb;
        private Animator animator; 
        public float idleSpeed = 0; 
        public float moveSpeed = 10; 
        private float currentSpeed; 
        private int count = 0;
        private Vector2 movementVector;
        public Camera mainCamera;
        public float jumpForce = 2.0f;
        private bool isJumping = false;
        public TextMeshProUGUI countText;
        public GameObject winTextObject; 
        public Transform playerTransform; 
        public Transform enemyTransform; 
        private bool isCloseToEnemy = false; 
        private bool isMoving = false; 

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>(); 
            mainCamera = Camera.main;
            SetCountText(); 
            winTextObject.SetActive(false); 
            currentSpeed = idleSpeed; 
        }

        void Update()
        {
            HandleJump();
            animator.SetBool("salto", isJumping);
        }

        void HandleJump()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
               
            }
           
			if(transform.position.y > 1f )
			{
 				isJumping = true;
			}else{
 			isJumping = false;}
        }

        void OnMove(InputValue movementValue)
        {
            movementVector = movementValue.Get<Vector2>();

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

            animator.SetBool("corro", isMoving);
        }

        void FixedUpdate()
        {
            if (isMoving)
            {
                Vector3 moveDirection = mainCamera.transform.forward;
                moveDirection.y = 0;
                moveDirection.Normalize();

                Vector3 movement = moveDirection * movementVector.y + mainCamera.transform.right * movementVector.x;
                rb.AddForce(movement * currentSpeed);
            }

            CheckDistanceToEnemy();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Coin"))
            {
                count++;
                other.gameObject.SetActive(false);
                SetCountText();
            }
            else if (other.gameObject.CompareTag("Poison"))
            {
                moveSpeed /= 2;
                other.gameObject.SetActive(false);
            }
            else if (other.gameObject.CompareTag("Power"))
            {
                other.gameObject.SetActive(false);
                moveSpeed *= 5; 
            }
            if (other.gameObject.CompareTag("Ground") && !isJumping)
            {
               // isJumping = false;
            }
        }

        void SetCountText()
        {
            countText.text = "Count: " + count.ToString();
            if (count >= 1)
            {
                winTextObject.SetActive(true);
            }
        }

        void CheckDistanceToEnemy()
        {
            float distance = Vector3.Distance(playerTransform.position, enemyTransform.position);

            if (distance < 10)
            {
                isCloseToEnemy = true;
            }
            else
            {
                isCloseToEnemy = false;
            }

            animator.SetBool("enemy", isCloseToEnemy);
        }
    }
}
