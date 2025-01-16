using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerInput inputs;
    private InputAction moveaction;
    private InputAction fireaction;
    private GameManager manager;
    [SerializeField] private FireballBehaviour fireball;
    [SerializeField] private GameObject earthBallPrefab;
    private Vector2 velocity = Vector2.zero;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float earthBallOffset = 1f; // Distance devant le joueur où la boule de terre sera placée
    [SerializeField] private float rotationSpeed = 1f; // Vitesse de rotation de la boule de terre
    private GameObject currentEarthBall;
    private float earthBallLifetime = 10f; // Temps de vie de la boule de terre en secondes
    private float earthBallSpawnTime;

    void Start()
    {
        manager = GameManager.GetInstance();
        inputs = manager.GetInputs();
        moveaction = inputs.actions.FindAction("Move");
        fireaction = inputs.actions.FindAction("Fire");
    }

    private void FixedUpdate()
    {
        Vector2 _moveValue = moveaction.ReadValue<Vector2>();
        velocity = _moveValue * speed;

        transform.position += new Vector3(velocity.x * Time.fixedDeltaTime, velocity.y * Time.fixedDeltaTime, 0);

        if (currentEarthBall != null)
        {
            MoveEarthBall();
        }
    }

    private void Update()
    {
        if (fireaction.triggered)
        {
            fireball.ShootFireball();
            SpawnEarthBall();
        }

        if (currentEarthBall != null && Time.time - earthBallSpawnTime >= earthBallLifetime)
        {
            Destroy(currentEarthBall);
            currentEarthBall = null;
        }
    }

    private void SpawnEarthBall()
    {
        if (currentEarthBall == null)
        {
            Vector3 spawnPosition = transform.position + transform.right * earthBallOffset;
            currentEarthBall = Instantiate(earthBallPrefab, spawnPosition, Quaternion.identity);
            earthBallSpawnTime = Time.time;
        }
    }

    private void MoveEarthBall()
    {
        // Calculer la position cible en fonction de la rotation
        float angle = rotationSpeed * Time.time;
        Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * earthBallOffset;
        Vector3 targetPosition = transform.position + offset;

        // Déplacer la boule de terre vers la position cible
        currentEarthBall.transform.position = Vector3.Lerp(currentEarthBall.transform.position, targetPosition, Time.fixedDeltaTime * speed);
    }
}
