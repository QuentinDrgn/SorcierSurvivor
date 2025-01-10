using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private PlayerInput inputs;

    private void Awake(){
        if (instance != null) {
            Destroy(this);
        }
        instance = this ;
    }

    public static GameManager GetInstance()
    {
        return instance;
    }

    public PlayerInput GetInputs()
    {
        return inputs;
    }
}