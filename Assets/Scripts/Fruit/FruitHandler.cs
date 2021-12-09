using UnityEngine;

public class FruitHandler : MonoBehaviour
{
    private SnakeMovement player;
    
    void Start()
    {
        player = FindObjectOfType<SnakeMovement>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hook") || other.CompareTag("SnakeHead"))
        {
            player._hasEaten = true;
            Destroy(gameObject);
        }
    }
}
