using UnityEngine;

public class PatrollingEnemy : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage the enemy deals
    public float moveSpeed = 2f; // Speed of the enemy
    public float jumpForce = 5f; // Force applied for jumping
    public Transform[] waypoints; // Points for the enemy to patrol
    private int currentWaypointIndex = 0; // Current waypoint index
    public float chaseRange = 5f; // Range within which the enemy will chase the player
    private Transform player; // Reference to the player
    private Rigidbody rb; // Reference to the Rigidbody component
    private bool isGrounded; // Is the enemy grounded

    void Awake()
    {
        rb = GetComponent<Rigidbody>(); // Get the Rigidbody component
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform; // Find the player and use null-conditional operator
        if (player == null) // Check if the player was found
        {
            Debug.LogWarning("Player not found! Make sure there is a GameObject with the 'Player' tag in the scene."); // Log a warning
        }
    }

    void Update()
    {
        if (player == null) // Check if the player reference is null
        {
            Debug.LogWarning("Player reference is null. Ensure the player is tagged correctly."); // Log a warning
            return; // Exit the method if player is not found
        }

        if (Vector3.Distance(transform.position, player.position) < chaseRange) // Check if the player is within chase range
        {
            ChasePlayer(); // Chase the player if within range
        }
        else
        {
            Patrol(); // Otherwise, patrol
        }
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return; // Exit if there are no waypoints

        Transform targetWaypoint = waypoints[currentWaypointIndex]; // Get the current waypoint
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime); // Calculate new position
        rb.MovePosition(newPosition); // Move using Rigidbody

        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.1f) // Check if the enemy has reached the waypoint
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; // Loop through waypoints
        }
    }

    void ChasePlayer()
    {
        Vector3 newPosition = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime); // Calculate new position
        rb.MovePosition(newPosition); // Move using Rigidbody
    }

    void FixedUpdate()
    {
        if (Time.frameCount % 10 == 0) // Check ground status every 10 frames
        {
            isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f); // Check if the enemy is grounded
        }
    }

    public void AttackPlayer()
    {
        PlayerController playerController = FindObjectOfType<PlayerController>(); // Find the PlayerController in the scene
        if (playerController != null)
        {
            playerController.SetHealth(playerController.CurrentHealth - damageAmount); // Decrease player health
        }
    }

    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // Apply force for jumping
        }
    }
} 