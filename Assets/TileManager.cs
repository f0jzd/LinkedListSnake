using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour
{
    
    private CameraManager _cameraManager;
    
    
    //public GameObject _tilePrefab;


    //public Vector2[,] tilePositions;

    private Tile _tile;
    


    // Start is called before the first frame update
    void Start()
    {

        _tile = FindObjectOfType<Tile>();
        
        _cameraManager = FindObjectOfType<CameraManager>();
        
        Random.Range(0, 5);
        
        
        //_tile = new Vector2[gridSize+1, gridSize+1];
        
        for (int i = 0; i < _cameraManager.width; i++)
        {
            for (int j = 0; j < _cameraManager.height; j++)
            {

                if (Random.Range(0,5) == 2)
                {
                    //var placeTile = Instantiate(_tile.Rocktile, new Vector2(i, j), quaternion.identity);
                    
                    //placeTile.GetComponent<Tile>().TilePositions = new Vector2Int[i,j];//Somethigns fucky here
                    

                }
                else
                {
                    //var placeTile = Instantiate(_tile.NormalTile, new Vector2(i, j), quaternion.identity);
                    //placeTile.GetComponent<Tile>().TilePositions = new Vector2Int[i,j];
                }
                    
                


            }
        }


        /*foreach (var VARIABLE in tilePositions)
        {
            
            Debug.Log(VARIABLE);
        }*/
        
        //Camera.main.orthographicSize = gridSize * Screen.height / Screen.width * 0.5f;
        //Camera.main.orthographicSize = gridSize*2 * Screen.height / Screen.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
