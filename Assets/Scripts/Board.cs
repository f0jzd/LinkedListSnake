using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using Edge = Unity.VisualScripting.Edge;
using Random = UnityEngine.Random;

public class Board : MonoBehaviour
{
    
    [SerializeField] private GameObject normalTile;
    [SerializeField] private GameObject rockTile;
    [Header("Spawn Chance in %")]
    [SerializeField] private int rockSpawnChance;

    private Pathfinding pathfinder;

    private Queue<IVertex<Transform>> path;

    private int tileCount=0;
    
    public Tile[,] _grid;
    private CameraManager _cameraManager;

    private SnakeMovement player;


    private void Start()
    {

        player = FindObjectOfType<SnakeMovement>();
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
                    pathfinder.verticesArray[i,j] = new Vertex<Transform>( placeTile.transform, pathfinder.graph );
                    tileCount++;
                }
                else
                {
                    var placeTile = Instantiate(normalTile, new Vector2(i, j), quaternion.identity);
                    _grid[i, j] = placeTile.GetComponent<NormalTile>();
                    pathfinder.verticesArray[i,j] = new Vertex<Transform>( placeTile.transform, pathfinder.graph );
                    tileCount++;
                }
            }
        }
        for (int i = 0; i < _cameraManager.BoardWidth; i++)
        {
            for (int j = 0; j < _cameraManager.BoardHeight; j++)
            {
                if (i+1 < _cameraManager.BoardWidth)
                {
                    pathfinder.graph.AddEdge(pathfinder.verticesArray[i, j], pathfinder.verticesArray[i+1, j], 1);
                }
                
                if (j+1 < _cameraManager.BoardHeight)
                {
                    pathfinder.graph.AddEdge(pathfinder.verticesArray[i, j], pathfinder.verticesArray[i, j+1], 1);
                }
                
                /*pathfinder.graph.AddEdge(pathfinder.verticesArray[i, j], pathfinder.verticesArray[i+1, j], 1);
                pathfinder.graph.AddEdge(pathfinder.verticesArray[i, j], pathfinder.verticesArray[i+1, j], 1);
                //pathfinder.graph.AddEdge(pathfinder.verticesArray[j-1,i], pathfinder.verticesArray[j,i], j);*/
                
            }

        }

        
        //pathfinder.graph.AddEdge(pathfinder.verticesArray[0, 0], pathfinder.verticesArray[0, 1], 1);
        //pathfinder.graph.AddEdge(pathfinder.verticesArray[0, 0], pathfinder.verticesArray[1, 0], 1);
        
        
        Debug.Log("Edges: " + pathfinder.graph.Edges.Length);
        Debug.Log("Vertices: " + pathfinder.graph.Vertices.Length);
        Debug.Log("Number of tiles: " + _cameraManager.BoardHeight*_cameraManager.BoardWidth);
        
        Debug.Log(pathfinder.verticesArray[1,1].Edges.Count);
        
        
        
        /*for( int i = 0; i < pathfinder.verticesArray[1,1].Edges.Count; i++ )
        {

            Vector3 pos1 = pathfinder.graph.Edges[1].Source.Value.position;
            Vector3 pos2 = pathfinder.graph.Edges[1].Destination.Value.position;
            Debug.DrawLine( pos1, pos2, Color.magenta, 500f );
        }*/

        
        player.StartCoroutine("moveCall");
        
        

    }
}
