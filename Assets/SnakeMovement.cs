using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeMovement : MonoBehaviour
{
    List<Transform> tail = new List<Transform>();
    public LinkedList<Transform> tails = new LinkedList<Transform>();
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
            tail.Insert(0, g.transform);

            // Reset the flag
            ate = false;
        }
        // Do we have a Tail?
        else if (tail.Count > 0)
        {
            // Move last Tail Element to where the Head was
            tail.Last().position = v;

            // Add to front of list, remove from the back
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    /*void OnTriggerEnter(Collision other)
    {
        
        
        
        
    }*/
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





/*public class LinkedList<T>
{
    
    private const int INITIAL_CAPACITY = 4;
    private const int MAXIMUM_CAPACITY = 64;
    public int count;
    private int capacity;
    private T[] items;
    
    
    public Node Next;//Link to the next node in the list
    public Node Head;
    public Node Tail;
    
    public class Node<T>
    {
        public T Next;//Link to the next node in the list
        public T Head;
        public T Data;//Data of this node
        


        public Node(T item)
        {
            Data = item;
   
        }
    }
    public LinkedList()
    {

        Tail = default;
        Head = default;
        
        items = new T[capacity];
        count = 0;

    }
    
    public void Add(T item)
    {
        Node newNode = new Node(item);
        if (count == 0)
        {
                
            newNode. = item;
               
            Tail = newNode;
            Head = newNode;
            count++;
        }
        if (count != 0)
        {

            Tail. = newNode;
            Tail = newNode;
            //Set the tail to the next node
            //Set the tail data to the new value
            count++;
        }
        count++;
    }
}*/










