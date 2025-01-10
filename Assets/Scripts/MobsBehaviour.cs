using UnityEngine;
using System.Collections.Generic;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] private GameObject mobPrefab;  // The mob prefab to spawn
    [SerializeField] private Camera mainCamera;     // Reference to the main camera
    [SerializeField] private GameObject player;     // Reference to the player
    [SerializeField] private float spawnDistance = 4f; // Distance from the camera's edge
    [SerializeField] private float spawnInterval = 2f; // Time between spawns
    [SerializeField] private float speed = 5f; // Speed of the mob

    [SerializeField] private List<GameObject> spawnedMobs = new List<GameObject>(); // List of spawned mobs

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main; // Automatically assign the main camera if not set
        }

        // Start spawning mobs at regular intervals
        InvokeRepeating(nameof(SpawnMob), 0f, spawnInterval);
    }

    private void Update()
    {
        MovingMobIntoPlayerPosition();
    }

    private void SpawnMob()
    {
    if (mobPrefab == null || mainCamera == null)
    {
        Debug.LogWarning("Mob prefab or camera is not assigned.");
        return;
    }

    // Choose a random edge of the camera
    Vector3 spawnPosition = GetRandomEdgePosition();
    Debug.Log("Spawn position: " + spawnPosition);

    // Spawn the mob at the chosen position
    GameObject mob = Instantiate(mobPrefab, spawnPosition, Quaternion.identity);
    //Debug.Log("Spawned mob: " + mob.name);
    spawnedMobs.Add(mob);
    Debug.Log("Number of spawned mobs: " + spawnedMobs.Count);
    }

    private Vector3 GetRandomEdgePosition()
    {
        // Get the camera bounds in world space
        float zPosition = mainCamera.transform.position.z; // Match camera's Z-plane

        float verticalExtent = mainCamera.orthographicSize; // Vertical size in world units
        float horizontalExtent = verticalExtent * mainCamera.aspect; // Horizontal size in world units

        // Randomize which edge to spawn on
        int edge = Random.Range(0, 4); // 0: Top, 1: Bottom, 2: Left, 3: Right

        Vector3 spawnPoint = Vector3.zero;

        switch (edge)
        {
            case 0: // Top
                spawnPoint = new Vector3(
                    Random.Range(-horizontalExtent, horizontalExtent), // Random X within horizontal bounds
                    verticalExtent + spawnDistance,                    // Just above the top
                    zPosition
                );
                break;

            case 1: // Bottom
                spawnPoint = new Vector3(
                    Random.Range(-horizontalExtent, horizontalExtent), // Random X within horizontal bounds
                    -verticalExtent - spawnDistance,                   // Just below the bottom
                    zPosition
                );
                break;

            case 2: // Left
                spawnPoint = new Vector3(
                    -horizontalExtent - spawnDistance,                 // Just left of the camera
                    Random.Range(-verticalExtent, verticalExtent),     // Random Y within vertical bounds
                    zPosition
                );
                break;

            case 3: // Right
                spawnPoint = new Vector3(
                    horizontalExtent + spawnDistance,                  // Just right of the camera
                    Random.Range(-verticalExtent, verticalExtent),     // Random Y within vertical bounds
                    zPosition
                );
                break;
        }

        // Convert from local camera space to world space
        spawnPoint += mainCamera.transform.position;

        return spawnPoint;
    }

    private void MovingMobIntoPlayerPosition() {
        if (player == null)
        {
            Debug.LogWarning("Player is not assigned.");
            return;
        }

        foreach (GameObject mob in spawnedMobs)
        {
            if (mob != null)
            {
                Vector3 direction = (player.transform.position - mob.transform.position).normalized;
                mob.transform.position += direction.normalized * speed * Time.deltaTime;
            }
        }
    }
}
