using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fruit;
    
    void Start()
    {
        InvokeRepeating(nameof(Spawner),0.6f,0.6f);
    }

    void Spawner()
    {
        int randomX = Random.Range(-20, 20);
        int randomY = Random.Range(-10, 10);
        
        GameObject fruitInstance = Instantiate(fruit,
            new Vector3(randomX,randomY, 0),
            quaternion.identity);
        
    }
}
