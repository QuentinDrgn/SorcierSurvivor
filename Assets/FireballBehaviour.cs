using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class FireballBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject fireballPrefab;
    [SerializeField] private GameObject Player;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float timer = 2f;

    [SerializeField] private List<GameObject> fireballs = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (fireballPrefab == null)
        {
            Debug.LogWarning("Fireball prefab is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the left mouse button is clicked
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            ShootFireball();
        }
    }

    public void ShootFireball()
    {
        if (fireballPrefab == null)
        {
            Debug.LogWarning("Fireball prefab is not assigned.");
            return;
        }

        // Spawn the fireball at the player's position
        GameObject fireball = Instantiate(fireballPrefab, Player.transform.position, Quaternion.identity);
        fireballs.Add(fireball);

        // Get the mouse position in world space
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldMousePosition = mainCamera.ScreenToWorldPoint(mousePosition);

        // Calculate the direction from the fireball to the mouse position
        Vector2 direction = (worldMousePosition - (Vector2)fireball.transform.position).normalized;

        // Get the Rigidbody2D component of the fireball
        Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            // Set the velocity to move the fireball towards the mouse position
            rb.velocity = direction * speed;
        }

        // Destroy the fireball after 2 seconds
        Destroy(fireball, 2f);
    }
}
