using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class CameraManager : MonoBehaviour {

    public int BoardWidth;
    public int BoardHeight;

    public int borderSize;

    
    private void Start()
    {
        SetupCamera();
    }
    
    void SetupCamera()
    {
        var main = Camera.main;
        Debug.Assert(main != null, nameof(main) + " != null");
        main.transform.position = new Vector3((BoardWidth - 1)/2f, (BoardHeight-1) /2f, -10f);
        float aspectRatio = Screen.width / (float) Screen.height;
        float verticalSize = BoardHeight / 2f + borderSize;
        float horizontalSize = (BoardWidth / 2f + borderSize ) / aspectRatio;
        main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize: horizontalSize;
    }

}