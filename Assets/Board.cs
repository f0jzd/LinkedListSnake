using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    
    [SerializeField] private GameObject normalTile;
    [SerializeField] private GameObject rockTile;
    [Header("Spawn Chance in %")]
    [SerializeField] private int rockSpawnChance;
    
    public Tile[,] _grid;
    private CameraManager _cameraManager;


    private void Start()
    {

        
        _cameraManager = FindObjectOfType<CameraManager>();
        _grid = new Tile[_cameraManager.BoardWidth, _cameraManager.BoardHeight];
        
        
        
        for (int i = 0; i < _cameraManager.BoardWidth; i++)
        {
            for (int j = 0; j < _cameraManager.BoardHeight; j++)
            {

                
                
                if (Random.Range(0,100) < rockSpawnChance)
                {
                    var placeTile = Instantiate(rockTile, new Vector2(i, j), quaternion.identity);
                    //placeTile.GetComponent<Rocktile>();

                    _grid[i, j] = placeTile.GetComponent<RockTile>();
                    

                }
                else
                {
                    var placeTile = Instantiate(normalTile, new Vector2(i, j), quaternion.identity);
                    //placeTile.GetComponent<NormalTile>();
                    
                    _grid[i, j] = placeTile.GetComponent<NormalTile>();


                }
                
            }
        }
        
    }
}
