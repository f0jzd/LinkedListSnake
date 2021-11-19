using UnityEngine;

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
        Camera.main.transform.position = new Vector3((float)(BoardWidth - 1)/2f, (float) (BoardHeight-1) /2f, -10f);
        float aspectRatio = (float) Screen.width / (float) Screen.height;
        float verticalSize = (float) BoardHeight / 2f + (float) borderSize;
        float horizontalSize = ((float) BoardWidth / 2f + (float) borderSize ) / aspectRatio;
        Camera.main.orthographicSize = (verticalSize > horizontalSize) ? verticalSize: horizontalSize;
        


    }

}