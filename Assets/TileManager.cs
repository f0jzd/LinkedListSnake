using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TileManager : MonoBehaviour
{

    public GameObject tile;
    public float gridSize;

    
    
    
    // Start is called before the first frame update
    void Start()
    {
        for (float i = -gridSize; i <= gridSize; i++)
        {
            for (float j = (-gridSize/2); j <= (gridSize/2); j++)
            {
                Instantiate(tile, new Vector2(i, j), quaternion.identity);
            }
        }

        
        //Camera.main.orthographicSize = gridSize * Screen.height / Screen.width * 0.5f;
        //Camera.main.orthographicSize = gridSize*2 * Screen.height / Screen.width * 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
