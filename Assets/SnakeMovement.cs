using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    private LL<Transform> tails = new LL<Transform>();
    List<Transform> tail = new List<Transform>();
    private Vector2 _dir;
    public GameObject tailPrefab;
    bool ate = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Move),0.1f,0.1f);
        
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            _dir = Vector2.right;
        else if (Input.GetKey(KeyCode.DownArrow))
            _dir = -Vector2.up;    // '-up' means 'down'
        else if (Input.GetKey(KeyCode.LeftArrow))
            _dir = -Vector2.right; // '-right' means 'left'
        else if (Input.GetKey(KeyCode.UpArrow))
            _dir = Vector2.up;
    }

    void Move() {
        // Save current position (gap will be here)
        Vector2 v = transform.position;

        // Move head into new direction (now there is a gap)
        transform.Translate(_dir);

        // Ate something? Then insert new Element into gap
        if (ate) {
            // Load Prefab into the world
            GameObject g =(GameObject)Instantiate(tailPrefab,
                v,
                Quaternion.identity);

            // Keep track of it in our tail list
            //tail.Insert(0, g.transform);
            tails.Add(g.transform);
            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tails.Count > 0)
        {
            //tail.Last().position = v;
            tails.head.data.position = v;
            
            
            
            
            //tail.Insert(0, tail.Last());
            //tails.Insert(0,tails.LLtail.data);
             
            
            //tail.RemoveAt(tail.Count-1);
            //tails.RemoveAt(tails.count);





        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        // Did the snake eat something?
        Debug.Log("Ate");
        // Food?
        if (other.CompareTag("Food"))
        {
            // Get longer in next Move call
            
            
            
            ate = true;
       
            // Remove the Food
            Destroy(other.gameObject);
        }
        // Collided with Tail or Border
        
    }
}





public class LL<T>
{
    
    private const int INITIAL = 4;
    private const int MAXIMUM = 64;
        
        
    public ListNode<T> head;
    public ListNode<T> LLtail;
        
        

        
        
    public int count;
    private T[] _items;
        
        
    public LL(int capacity = INITIAL)
    {
        //_items = new T[capacity];
        LLtail = null;
        head = null;
        count = 0;
    }
    
    public class ListNode<T>
    {
        public T data;//Data of this node
        public ListNode<T> nextNode;//Link to the next node in the list
            
            
        public ListNode()
        {
            //Set the head and the tail to the same value if count is 0
            //else?
 
                
        }
    }
    
    public int Count => count;
    public void Add(T item)
    {
            
        ListNode<T> newNode = new ListNode<T>();
            
            
        if (head == null && LLtail == null)
        {
            newNode.data = item;
            //newNode.nextNode = null;
            //_items[count] = item;

            head = newNode;
            LLtail = newNode;
            count++;
                
                
        }
        else
        {
            newNode.data = item;
            LLtail.nextNode = newNode;
            LLtail = newNode;
            //_items[count] = item;
            count++;
        }
            
    }
    public void Insert(int index, T item)
    {
        int indexChecker = 0;
        ListNode<T> temphead = head;
        ListNode<T> newNode = new ListNode<T>();
        newNode.data = item;
            
        while (temphead != null)
        {
            if (index == indexChecker)
            {
                count++;
                newNode.nextNode = temphead.nextNode;
                temphead.nextNode = newNode;

                if (newNode.nextNode == null)
                {
                    LLtail = newNode;
                }
            }
                
            indexChecker++;
            temphead = temphead.nextNode;
        }
    }
    public void RemoveAt(int index)
    {
        int iterations =0;
        ListNode<T> temphead = head;
        ListNode<T> delete;

            
            
            
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
                    return;
                    count--;
                }
                else
                {
                    temphead.nextNode = temphead.nextNode.nextNode;
                }
                    
            }
            temphead = temphead.nextNode;
        }
            
    }

    public void Move(T tailPrefabPos)
    {
        


    }
    
    
}










