using System.Collections.Generic;
using Unity.Mathematics;
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
        
        
        
        Vertex<int>[] vertices = new Vertex<int>[]
        {
            new Vertex<int>(1, pathfinder.graph),
            new Vertex<int>(2, pathfinder.graph),
            new Vertex<int>(3, pathfinder.graph),
            new Vertex<int>(4, pathfinder.graph),
            new Vertex<int>(5, pathfinder.graph),
            new Vertex<int>(6, pathfinder.graph)
        };

        pathfinder.graph.AddEdge(vertices[0], vertices[1], 4f);
        pathfinder.graph.AddEdge(vertices[0], vertices[2], 4f);
        pathfinder.graph.AddEdge(vertices[1], vertices[2], 2f);
        pathfinder.graph.AddEdge(vertices[2], vertices[3], 3f);
        pathfinder.graph.AddEdge(vertices[2], vertices[5], 6f);
        pathfinder.graph.AddEdge(vertices[2], vertices[4], 1f);
        pathfinder.graph.AddEdge(vertices[3], vertices[5], 2f);
        pathfinder.graph.AddEdge(vertices[4], vertices[5], 3f);
        
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

        foreach (var asd in pathfinder.vertices)
        {
            Debug.Log(pathfinder.graph);
        }
        
        
    }
}
