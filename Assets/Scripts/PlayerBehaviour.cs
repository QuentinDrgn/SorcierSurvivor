using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour
{
    private PlayerInput inputs;
    private InputAction moveaction;
    private InputAction fireaction;
    private GameManager manager;
    [SerializeField] private FireballBehaviour fireball;
    private Vector2 velocity = Vector2.zero;
    [SerializeField] private float speed = 5f;
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

    }

    private void Update()
    {
        if (fireaction.triggered)
        {
            fireball.ShootFireball();
        }
    }
}