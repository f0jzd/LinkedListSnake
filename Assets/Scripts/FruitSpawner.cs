using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] private GameObject fruit;

    private TileManager _tileManager;
    private CameraManager _cameraManager;

    private int _randomY;
    private int _randomX;
    
    void Start()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
        _tileManager = FindObjectOfType<TileManager>();
        
        _randomX = Random.Range(0, _cameraManager.BoardWidth);
        _randomY = Random.Range(0, _cameraManager.BoardHeight);
        
        GameObject fruitInstance = Instantiate(fruit,
            new Vector3(_randomX,_randomY, 0),
            quaternion.identity);
    }

    public void Spawner()
    {
        _randomX = Random.Range(0, _cameraManager.BoardWidth);
        _randomY = Random.Range(0, _cameraManager.BoardHeight);
        
        GameObject fruitInstance = Instantiate(fruit,
            new Vector3(_randomX,_randomY, 0),
            quaternion.identity);
        
    }
}
