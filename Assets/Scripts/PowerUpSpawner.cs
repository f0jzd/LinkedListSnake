using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject powerUp;

    private CameraManager _cameraManager;

    private int _randomY;
    private int _randomX;
    
    void Start()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
        FindObjectOfType<TileManager>();
        
        _randomX = Random.Range(0, _cameraManager.BoardWidth);
        _randomY = Random.Range(0, _cameraManager.BoardHeight);

        Instantiate(powerUp, new Vector3(_randomX, _randomY), Quaternion.identity);
    }

    public void Spawner()
    {
        _randomX = Random.Range(0, _cameraManager.BoardWidth);
        _randomY = Random.Range(0, _cameraManager.BoardHeight);
        
        Instantiate(powerUp, new Vector3(_randomX,_randomY),Quaternion.identity);
        
    }
}
