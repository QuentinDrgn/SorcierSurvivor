using UnityEngine;

public class EarthBallBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Mob"))
        {
            Destroy(other.gameObject);
        }
    }
}