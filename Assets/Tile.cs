using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public GameObject rocktile;
    public GameObject normalTile;
    [HideInInspector] public GameObject tiletype;
    
    
    
    public bool walkable;
    private SpriteRenderer _spriteRenderer;
    public Vector2Int tilePositions;


    public Tile()
    {
        
    }

    


    public GameObject Rocktile
    {
        get => rocktile;
    }
    public GameObject NormalTile
    {
        get => normalTile;
    }

    public GameObject Tiletype
    {

        get => tiletype;

        set => tiletype = value;
    }
}




