using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    
    [SerializeField] private GameObject normalTile;
    [SerializeField] private GameObject rockTile;
    [Header("Spawn Chance in %")]
    [SerializeField] private int rockSpawnChance;

    private Pathfinding pathfinder;
    
    public Tile[,] _grid;
    private CameraManager _cameraManager;


    private void Start()
    {


        pathfinder = FindObjectOfType<Pathfinding>();
        _cameraManager = FindObjectOfType<CameraManager>();
        _grid = new Tile[_cameraManager.BoardWidth, _cameraManager.BoardHeight];


        for (int i = 0; i < _cameraManager.BoardWidth; i++)
        {
            for (int j = 0; j < _cameraManager.BoardHeight; j++)
            {
                if (Random.Range(0, 100) < rockSpawnChance)
                {
                    var placeTile = Instantiate(rockTile, new Vector2(i, j), quaternion.identity);
                    _grid[i, j] = placeTile.GetComponent<RockTile>();
                    pathfinder.verticesArray[i] = new Vertex<Transform>( placeTile.transform, pathfinder.graph );
                }
                else
                {
                    var placeTile = Instantiate(normalTile, new Vector2(i, j), quaternion.identity);
                    _grid[i, j] = placeTile.GetComponent<NormalTile>();
                    pathfinder.verticesArray[i] = new Vertex<Transform>( placeTile.transform, pathfinder.graph );
                }
            }
        }
        for (int i = 0; i < _cameraManager.BoardWidth; i++)
        {
            for (int j = 0; j < _cameraManager.BoardHeight; j++)
            {
                pathfinder.graph.AddEdge( pathfinder.verticesArray[i], pathfinder.verticesArray[j], j);
            }
        }
        
    }
}
