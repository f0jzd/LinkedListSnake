using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    
    [SerializeField] private GameObject normalTile;
    [SerializeField] private GameObject rockTile;
    public Tile[,] _grid;
    private CameraManager _cameraManager;


    private void Start()
    {

        
        _cameraManager = FindObjectOfType<CameraManager>();
        _grid = new Tile[_cameraManager.width, _cameraManager.height];
        
        
        
        for (int i = 0; i < _cameraManager.width; i++)
        {
            for (int j = 0; j < _cameraManager.height; j++)
            {

                
                
                if (Random.Range(0,5) == 2)
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
