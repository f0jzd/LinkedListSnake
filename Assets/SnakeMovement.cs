using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnakeMovement : MonoBehaviour
{
    private LinkList<Transform> tails = new LinkList<Transform>();
    private Vector2 _moveDirection;
    [SerializeField] private GameObject tailPrefab;
    private bool _hasEaten = false;
    private float _movementSpeedMultiplier  = 1;

    [SerializeField] private float powerUpDuration;

    private float defaultSpeed = 0.1f;
    private float powerUpSpeed = 0.4f;

    private float currentSpeed;
    
    
    public GameObject dieScreen;
    public Button restartButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentSpeed = defaultSpeed;
        InvokeRepeating(nameof(Move),currentSpeed,currentSpeed); //USE COROUTINE INSTEAD APPARENTLY
        restartButton.onClick.AddListener(RestartGame);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            _moveDirection = Vector2.right *_movementSpeedMultiplier;
        else if (Input.GetKey(KeyCode.DownArrow))
            _moveDirection = -Vector2.up * _movementSpeedMultiplier;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            _moveDirection = -Vector2.right * _movementSpeedMultiplier; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            _moveDirection = Vector2.up * _movementSpeedMultiplier;
    }
    void Move() {
        Vector2 gapToFill = transform.position;
        transform.Translate(_moveDirection);
        if (_hasEaten) {
            GameObject tail =Instantiate(tailPrefab,gapToFill,Quaternion.identity);
            tails.Add(tail.transform);
            _hasEaten = false;
        }
        if (tails.Count > 0)
        {
            tails.LLtail.data.position = gapToFill;
            tails.Insert(0,tails.LLtail.data);
            tails.RemoveAt(tails.count-1);
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
            dieScreen.SetActive(true);
            Time.timeScale = 0;
        }

        if (other.transform.CompareTag("PowerUp"))
        {
            Debug.Log("Hit Speed");
            StartCoroutine(SpeedBoost());
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        dieScreen.SetActive(false);
    }
    IEnumerator SpeedBoost()
    {

        _movementSpeedMultiplier = 2;
        yield return new WaitForSeconds(powerUpDuration);
        _movementSpeedMultiplier = 1;
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
        int iterations =0;
        var temphead = head;
        while (temphead != null)
        {
            if (index== 0)
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










