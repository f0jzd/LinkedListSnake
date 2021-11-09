using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    
    [SerializeField] private GameObject normalTile;
    [SerializeField] private GameObject rockTile;
    private Tile[,] _tile;
    private CameraManager _cameraManager;


    private void Start()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
        
        
        
        for (int i = 0; i < _cameraManager.width; i++)
        {
            for (int j = 0; j < _cameraManager.height; j++)
            {

                
                
                if (Random.Range(0,5) == 2)
                {
                    var placeTile = Instantiate(rockTile, new Vector2(i, j), quaternion.identity);

                    
                    

                    placeTile.GetComponent<Tile>().tilePositions = new Vector2Int(i,j);
                    placeTile.GetComponent<Tile>().tiletype = rockTile;
                    
                    Debug.Log(placeTile.GetComponent<Tile>().tilePositions);
                    
                }
                else
                {
                    var placeTile = Instantiate(normalTile, new Vector2(i, j), quaternion.identity);
                    placeTile.GetComponent<Tile>().tilePositions = new Vector2Int(i,j);
                    placeTile.GetComponent<Tile>().Tiletype = normalTile;


                }
                
            }
        }
        
    }
}
