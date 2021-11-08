using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fruit;

    private TileManager _tileManager;
    private CameraManager _cameraManager;
    
    void Start()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
        _tileManager = FindObjectOfType<TileManager>();
        InvokeRepeating(nameof(Spawner),0.6f,0.6f);
    }

    void Spawner()
    {
        int randomX = Random.Range(0, _cameraManager.width);
        int randomY = Random.Range(0, _cameraManager.height);
        
        GameObject fruitInstance = Instantiate(fruit,
            new Vector3(randomX,randomY, 0),
            quaternion.identity);
        
    }
}
