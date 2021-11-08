using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public GameObject rocktile;
    public GameObject normalTile;
    
    
    
    public bool walkable;
    private SpriteRenderer _spriteRenderer;
    public Vector2Int[,] tilePositions;


    public Tile()
    {
        
    }

    
    public Vector2Int[,] TilePositions
    {



        get
        {
            return tilePositions;
        }

        set
        {
            
            tilePositions = value;
        }
    }
    

    public GameObject Rocktile
    {
        get => rocktile;
    }
    public GameObject NormalTile
    {
        get => normalTile;
    }

    
}




