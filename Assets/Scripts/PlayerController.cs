using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement variables
    public float moveSpeed = 5f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;

    // Health variables
    public float maxHealth = 100f; // Maximum health
    private float currentHealth; // Current health

    // Stamina variables
    public float maxStamina = 100f; // Maximum stamina
    private float currentStamina; // Current stamina

    private CharacterController characterController;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        currentHealth = maxHealth; // Initialize current health
        currentStamina = maxStamina; // Initialize current stamina
    }

    public float CurrentHealth // Property to access current health
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); } // Ensure health does not exceed maxHealth
    }

    public float Stamina // Property to access current stamina
    {
        get { return currentStamina; }
        set { currentStamina = Mathf.Clamp(value, 0, maxStamina); } // Ensure stamina does not exceed maxStamina
    }

    // Method to set health
    public void SetHealth(float health)
    {
        CurrentHealth = health; // Use the property to set health
    }

    void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        // Check if the player is grounded
        isGrounded = characterController.isGrounded;

        // Get input for horizontal and vertical movement
        float moveHorizontal = Input.GetAxis("Horizontal"); // Left and right movement
        float moveVertical = Input.GetAxis("Vertical"); // Forward and backward movement

        // Create a movement direction vector
        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime); // Move the player

        // Apply gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Move the player with gravity
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        // Jump logic
        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) // Check if grounded and space is pressed
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Calculate jump velocity
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime; // Apply gravity to the vertical velocity
    }
}