using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    private Camera mainCamera;

    private TileManager tileManager;
    

    [HideInInspector]public LinkList<Transform> tails = new LinkList<Transform>();
    private Vector2 _moveDirection;
    private LineRenderer _lineRenderer;

    [SerializeField] private GameObject hook;


    [SerializeField] private GameObject tailPrefab;
    public bool _hasEaten;

    [SerializeField] private float powerUpDuration;

    [SerializeField] private float defaultSpeed;
    [SerializeField] private float powerUpSpeed;

    private float _currentSpeed;
    private bool hookShot;
    private bool isDead;

    private Vector3 hookBase;


    private Vector3 snakePosition;


    private Vector3 mouseWorldPosition;

    private Tile _tile;

    private CameraManager _cameraManager;
    public GameObject dieScreen;
    public Button restartButton;


    void Start()
    {
        _tile = FindObjectOfType<Tile>();
        _cameraManager = FindObjectOfType<CameraManager>();
        tileManager = FindObjectOfType<TileManager>();
        transform.position = new Vector2(_cameraManager.width / 2, _cameraManager.height / 2);
        snakePosition = transform.position;
        hook = Instantiate(hook, hookBase, quaternion.identity);
        _lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        _currentSpeed = defaultSpeed;
        StartCoroutine(moveCall());
        restartButton.onClick.AddListener(RestartGame);
        
        
        
    }

    private void Update()
    {
        //Debug.Log($"Screen Height: {Screen.height}, Screen Width{Screen.width}");
        
        hookBase = new Vector3(transform.position.x, transform.position.y, -0.5f);
        _lineRenderer.SetPosition(0,transform.position);
        _lineRenderer.SetPosition(1,hook.transform.position);
        

        Vector3 mousePos = Input.mousePosition;
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(mousePos);
        mouseWorldPosition.z = 0;
        
        Vector3 targetDir = mouseWorldPosition - transform.position;
 
        float angle = Mathf.Atan2(targetDir.x, targetDir.y) *- Mathf.Rad2Deg;
        hook.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


        
        
        if (Input.GetMouseButton(0) && !isDead)
        {
            hook.transform.position = Vector3.Lerp(hook.transform.position, mouseWorldPosition, 0.01f);
            //_lineRenderer.SetPosition(1, Vector3.Lerp(_lineRenderer.GetPosition(1), mouseWorldPosition, 0.01f));
        }

        if (!Input.GetMouseButton(0)&& !isDead)
        {
            hook.transform.position = hookBase;
            //_lineRenderer.SetPosition(1, Vector3.Lerp(_lineRenderer.GetPosition(1), _lineRenderer.GetPosition(0), 0.01f));
        }
        
        
        if (Input.GetMouseButton(0))
        {
            hookShot = true;
        }
        
        if (!Input.GetMouseButton(0))
        {
            hookShot = false;
        }
        
        

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            _moveDirection = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            _moveDirection = -Vector2.up; // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            _moveDirection = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            _moveDirection = Vector2.up;
        
        if (transform.position.x >= _cameraManager.width)
        {
            transform.position = new Vector2(0, transform.position.y);
        }
        if (transform.position.x < 0)
        {
            transform.position = new Vector2(_cameraManager.width-1, transform.position.y);
        }
        if (transform.position.y > _cameraManager.height)
        {
            transform.position = new Vector2(transform.position.x, 0);
        }
        if (transform.position.y < 0)
        {
            transform.position = new Vector2(transform.position.x, _cameraManager.height);
        }
    }


    void Move()
    {//CLEAN THIS UP PLEASE ITS CAN BE MUCH NICOER
        
        Vector2 gapToFill = transform.position;
        transform.Translate(_moveDirection);
        if (_hasEaten)
        {
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

        if (other.transform.CompareTag("Tail") || other.transform.CompareTag("Wall"))
        {
            isDead = true;
            dieScreen.SetActive(true);
            Time.timeScale = 0;
        }

        if (other.transform.CompareTag("PowerUp"))
        {
            _currentSpeed = powerUpSpeed;
            Invoke("PowerUpSpeedChanger", powerUpDuration);
            Debug.Log("Hit Speed");
        }
    }

    private void PowerUpSpeedChanger()
    {
        _currentSpeed = defaultSpeed;
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
            yield return new WaitForSeconds(_currentSpeed);
            Move();
        }
    }
    
    
}


public class LinkList<T>
{
    public ListNode<T> head;
    public ListNode<T> LLtail;
    public int count;
    private T[] _items;


    public LinkList()
    {
        //_items = new T[capacity];
        LLtail = null;
        head = null;
        count = 0;
    }

    public class ListNode<T>
    {
        public T data;
        public ListNode<T> nextNode;
    }

    public int Count => count;

    public void Add(T item)
    {
        var newNode = new ListNode<T>();
        if (head == null && LLtail == null)
        {
            newNode.data = item;
            head = newNode;
            LLtail = newNode;
            count++;
        }
        else
        {
            newNode.data = item;
            LLtail.nextNode = newNode;
            LLtail = newNode;
            count++;
        }
    }

    public void Insert(int index, T item)
    {
        int indexChecker = 0;
        var temphead = head;
        var newNode = new ListNode<T>();
        newNode.data = item;

        while (temphead != null)
        {
            if (index == 0)
            {
                count++;
                newNode.nextNode = head;
                head = newNode;

                if (newNode.nextNode == null)
                {
                    LLtail = newNode;
                }

                return;
            }

            indexChecker++;
            temphead = temphead.nextNode;

            if (index == indexChecker)
            {
                count++;
                newNode.nextNode = temphead.nextNode;
                temphead.nextNode = newNode;

                if (newNode.nextNode == null)
                {
                    LLtail = newNode;
                }

                return;
            }

            indexChecker++;
            temphead = temphead.nextNode;
        }
    }

    public void RemoveAt(int index)
    {
        int iterations = 0;
        var temphead = head;
        while (temphead != null)
        {
            if (index == 0)
            {
                head = head.nextNode;
                count--;
                return;
            }

            iterations++;
            if (iterations == index)
            {
                if (temphead.nextNode.nextNode == null)
                {
                    LLtail = temphead;
                    temphead.nextNode = null;
                    count--;
                    return;
                }
                else
                {
                    temphead.nextNode = temphead.nextNode.nextNode;
                }
            }

            temphead = temphead.nextNode;
        }
    }
}