using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SnakeMovement : MonoBehaviour
{
    
    public GameObject dieScreen;
    public Button restartButton;
    private LinkList<Transform> tails = new LinkList<Transform>();
    
    [SerializeField] private GameObject tailPrefab;
    [SerializeField] private float duration;
    [SerializeField] private float snakeSpeed;
    
    [HideInInspector] public bool _hasEaten;
    [HideInInspector] public bool isDead;
    
    private Vector2 _moveDirection;
    private Vector3 playerPosition;
    private Vector3 mouseWorldPosition;
    
    private Board board;
    private CameraManager _cameraManager;
    private PowerUpSpawner _powerUpSpawner;
    private FruitSpawner _fruitSpawner;
    
    
    private float elapsedTime;
    private float powerUpDuration;
    private float _currentSpeed;

    private bool moveRight;
    private bool moveLeft;
    private bool moveUp;
    private bool moveDown;

    private SnakeMoveDirection direction; 

    enum SnakeMoveDirection
    {
     moveRight= 0,
     moveLeft= 1,
     moveUp= 2,
     moveDown= 3,
    }
    
    
    void Awake()
    {

        board = FindObjectOfType<Board>();
        _cameraManager = FindObjectOfType<CameraManager>();
        transform.position = new Vector2(_cameraManager.BoardWidth / 2, _cameraManager.BoardHeight / 2);
        _currentSpeed = snakeSpeed;
        restartButton.onClick.AddListener(RestartGame);
        _fruitSpawner = FindObjectOfType<FruitSpawner>();
        _powerUpSpawner = FindObjectOfType<PowerUpSpawner>();
        
        direction = (SnakeMoveDirection) Random.Range(0, 3);
        
        
        
        
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            direction = SnakeMoveDirection.moveRight;
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            direction = SnakeMoveDirection.moveDown;
        else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            direction = SnakeMoveDirection.moveLeft;
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            direction = SnakeMoveDirection.moveUp;

        playerPosition = transform.position;

    }
    
    void Move()
    {
        
        int snakeX = (int) playerPosition.x;
        int snakeY = (int) playerPosition.y;
        var newPos = board._grid[snakeX, snakeY];
        
        if (direction == SnakeMoveDirection.moveDown)
            newPos = newPos.transform.position.y-1 < 0 ? board._grid[snakeX, _cameraManager.BoardHeight-1] : board._grid[snakeX, snakeY - 1];
        
        if (direction == SnakeMoveDirection.moveUp)
            newPos = newPos.transform.position.y+1 > _cameraManager.BoardHeight-1 ? board._grid[snakeX, 0] : board._grid[snakeX, snakeY + 1];
        
        if (direction == SnakeMoveDirection.moveLeft) 
            newPos = newPos.transform.position.x-1 < 0 ? board._grid[_cameraManager.BoardWidth-1, snakeY] : board._grid[snakeX-1, snakeY];
        
        if (direction == SnakeMoveDirection.moveRight) 
            newPos = newPos.transform.position.x+1 > _cameraManager.BoardWidth-1 ? board._grid[0, snakeY] : board._grid[snakeX+1, snakeY];
        
        
        Vector2 gapToFill = transform.position;
        transform.position = newPos.transform.position;
        
        
        
        
        if (_hasEaten)
        {
            _fruitSpawner.Invoke("Spawner",0);
            GameObject tail = Instantiate(tailPrefab, gapToFill, Quaternion.identity);
            tails.Add(tail.transform);
            _hasEaten = false;
        }

        if (tails.Count > 0)
        {
            tails.LLtail.data.position = gapToFill;
            tails.Insert(0, tails.LLtail.data);
            tails.RemoveAt(tails.count - 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            
            _hasEaten = true;
            Destroy(other.gameObject);
        }
        if (other.CompareTag("PowerUp"))
        {
            
            powerUpDuration += duration;
            _powerUpSpawner.Spawner();
            Destroy(other.gameObject);
        }

        if (other.transform.CompareTag("Tail") || other.transform.CompareTag("Wall"))
        {
            isDead = true;
            dieScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        dieScreen.SetActive(false);
    }


    IEnumerator moveCall()
    {
        while (true)
        {

            if (powerUpDuration < 0)
            {
                Move();
                yield return new WaitForSeconds(_currentSpeed);
            }
            if (powerUpDuration >= 0)
            {
                powerUpDuration -= 0.1f;
                Move();
                yield return new WaitForSeconds(_currentSpeed/2);
            }

            yield return null;
            
        }
    }
}