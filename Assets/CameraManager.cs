using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraManager : MonoBehaviour {

    public int width;
    public int height;

    public int borderSize;

    private TileManager _tileManager;


    private void Start()
    {
        _tileManager = FindObjectOfType<TileManager>();
    }

    void Update()
    {
        SetupCamera();
    }

    
    
    void SetupCamera()
    {
        Camera.main.transform.position = new Vector3((float)(width - 1)/2f, (float) (height-1) /2f, -10f);

        //transform.position = new Vector3(_tileManager.gridSize / 2, _tileManager.gridSize / 2,-10);

        float aspectRatio = (float) Screen.width / (float) Screen.height;
        float verticalSize = (float) height / 2f + (float) borderSize;
        float horizontalSize = ((float) width / 2f + (float) borderSize ) / aspectRatio;
        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize: horizontalSize;
        
        //Camera.main.orthographicSize = (float) (_tileManager.gridSize * Screen.height / Screen.width * 0.5);


    }

}