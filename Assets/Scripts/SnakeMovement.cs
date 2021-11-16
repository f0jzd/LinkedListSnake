using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    private Vector3 mouseWorldPosition;
    private Board board;
    private CameraManager _cameraManager;
    private int withinBoundsX;
    private int withinBoundsY;
    private float _currentSpeed;
    private Vector3 playerPosition;
    private FruitSpawner _fruitSpawner;
    private float elapsedTime;
    private PowerUpSpawner _powerUpSpawner;
    private float powerUpDuration;
    
    
    void Start()
    {

        board = FindObjectOfType<Board>();
        _cameraManager = FindObjectOfType<CameraManager>();
        transform.position = new Vector2(_cameraManager.BoardWidth / 2, _cameraManager.BoardHeight / 2);
        _currentSpeed = snakeSpeed;
        StartCoroutine(moveCall());
        restartButton.onClick.AddListener(RestartGame);
        _fruitSpawner = FindObjectOfType<FruitSpawner>();
        _powerUpSpawner = FindObjectOfType<PowerUpSpawner>();
        
        
        
        withinBoundsX = _cameraManager.BoardWidth;
        withinBoundsY = _cameraManager.BoardHeight;
        
    }

    private void Update()
    {
        playerPosition = transform.position;
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            _moveDirection = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            _moveDirection = Vector2.down;
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            _moveDirection = Vector2.left;
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            _moveDirection = Vector2.up;
        
        
        if (transform.position.x >= _cameraManager.BoardWidth)
            transform.position = new Vector2(0, transform.position.y);
        if (transform.position.x < 0)
            transform.position = new Vector2(_cameraManager.BoardWidth-1, transform.position.y);
        if (transform.position.y > _cameraManager.BoardHeight-1)
            transform.position = new Vector2(transform.position.x, 0);
        if (transform.position.y < 0)
            transform.position= new Vector2(transform.position.x, _cameraManager.BoardHeight-1);
        
        
        
        int snakeX = (int) playerPosition.x;
        int snakeY = (int) playerPosition.y;

        
        if (snakeX < withinBoundsX && snakeY < withinBoundsY && snakeX > 0 && snakeY > 0)
        {
            Tile currentTile = board._grid[snakeX, snakeY];
        }
    }
    
    void Move()
    {
        
        Vector2 gapToFill = transform.position;
        transform.Translate(_moveDirection);
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
        //dieScreen.SetActive(false);
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