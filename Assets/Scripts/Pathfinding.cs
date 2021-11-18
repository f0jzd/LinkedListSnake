using System;
using System.Collections.Generic;
using System.IO;
using System.Transactions;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Pathfinding : MonoBehaviour
{
    public Graph<Transform> graph = new Graph<Transform>();
    public Vertex<Transform>[,] verticesArray;
    
    private CameraManager _cameraManager;


    private void Awake()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
        verticesArray = new Vertex<Transform>[_cameraManager.BoardWidth,_cameraManager.BoardHeight];

        /*
        for( int i = 0; i < verticesArray.Length; i++ ) {
            Vector3 position = GetRandomVector3();
            GameObject vertex = Instantiate( vertexPrefab, position, Quaternion.identity );
            verticesArray[i] = new Vertex<Transform>( vertex.transform, graph );
        }
        
        graph.AddEdge( verticesArray[0], verticesArray[1], 1 );
        graph.AddEdge( verticesArray[0], verticesArray[2], 2 );
        graph.AddEdge( verticesArray[1], verticesArray[2], 3 );
        graph.AddEdge( verticesArray[2], verticesArray[3], 4 );
        graph.AddEdge( verticesArray[2], verticesArray[5], 5 );
        graph.AddEdge( verticesArray[2], verticesArray[4], 6 );
        graph.AddEdge( verticesArray[3], verticesArray[5], 7 );
        graph.AddEdge( verticesArray[4], verticesArray[5], 8 );
        
        
        Debug.Log( $"Edges: {graph.Size}" );
        for( int i = 0; i < graph.Edges.Length; i++ ) {
            Vector3 pos1 = graph.Edges[i].Source.Value.position;
            Vector3 pos2 = graph.Edges[i].Destination.Value.position;
            Debug.DrawLine( pos1, pos2, Color.magenta, 500f );
        }*/
    }
    
    
    public void Pathfinder(Vertex<Transform> start,Vertex<Transform> destination )
    {

        List<Edge<Transform>> openList = new List<Edge<Transform>>();
        List<Edge<Transform>> closedList = new List<Edge<Transform>>();
        Edge<Transform> current = null;


        foreach (Edge<Transform> edge in start.Outgoing) //
        {
            openList.Add(edge);
        }

        foreach (Edge<Transform> edge in start.Outgoing)//
        {
            if (current == null)
            {
                current.Cost = edge.Cost; 
            }
            
            if (edge.Cost < current.Cost )
            {
                current.Cost = edge.Cost; 
            }

            openList.Remove(edge);
            closedList.Add(edge);

            if (current.Source.Value == destination.Value)
            {
                return;
            }

            //Foreach neighbour of the current node
            
            

        }
        
        
    }

    private float GetDistance(IVertex<Transform> Start, IVertex<Transform> Destination)
    {
        float distX = Mathf.Abs(Start.Value.transform.position.x - Destination.Value.transform.position.x);
        float distY = Mathf.Abs(Start.Value.transform.position.y - Destination.Value.transform.position.y);

        if (distX > distY)
        {
            return 10*distY + 10 * (distX - distY);
        }
        return 10*distX + 10 * (distY - distX);
    }
    
    
    

    
}



public class Graph<T>
{
    private List<IVertex<T>> _vertices;    //Using list instead of hashset since list is ordered and easier to print
    private HashSet<Edge<T>> _edges;       //Used to inspect the graph
    
    
    public int Order => _vertices.Count;

    public int Size => _edges.Count;
    
    

    public IVertex<T>[] Vertices => _vertices.ToArray();

    public Edge<T>[] Edges
    {
        get
        {
            Edge<T>[] returnValue = new Edge<T>[_edges.Count];
            _edges.CopyTo(returnValue);
            return returnValue;
                
            //Gets the array of vertices and edges.
        }
    }
    public Graph()
    {
        _vertices = new List<IVertex<T>>();
        _edges = new HashSet<Edge<T>>();
    }


    public void AddVertex(IVertex<T> vertex)
    {
        _vertices.Add(vertex);
    }

    public void AddEdge(IVertex<T> v1, IVertex<T> v2, float weight)
    {
        
        _edges.Add(v1.AddEdge(v2, weight));
        _edges.Add(v2.AddEdge(v1, weight));

    }


    
}

public class Vertex<T> : IVertex<T>
{
    private T _value;//Vertex Data, it is referred to IVertex it doesnt know what type is it
    private Type _type;
    private HashSet<Edge<T>> outgoing; //List of edges, these are the edges going out from this vertex
    private int indexInGraph;

/*
    public int G { get; set; } = 0;

    public int H { get; set; } = 0;

    public int F { get; set; } = 0;
*/

    public Type Type => _type;
    public T Value
    {
        get => _value;
        set => _value = value;
    }

    public int IndexInGraph => indexInGraph;
    public HashSet<Edge<T>> Outgoing => outgoing;
    public Edge<T> AddEdge(IVertex<T> target, float weight = 1)
    {
        Edge<T> edge = new Edge<T>(this, target, weight);
        outgoing.Add(edge);
        return edge;
    }


    //public HashSet<Edge<T>> Edges => outgoing;
    


    
    public Vertex(T _value, Graph<T> G)
    {
        this._value = _value;
        this.outgoing = new HashSet<Edge<T>>();
        this.indexInGraph = G.Order;
        G.AddVertex(this);
        
    }
    
}

public class Edge<T>
{
    private IVertex<T> source;
    private IVertex<T> destination;
    private float cost;
    //The cost of this conneection, i.e. a geographical desintation would be the distance, its what the graph represents.
    //This of vertices are places in a map and weight the is cost to get there.

    public IVertex<T> Source => source;

    public IVertex<T> Destination => destination;

    public float Cost
    {
        get => cost;
        set => cost = value;
    }

    public Edge(IVertex<T> source,IVertex<T> destination, float cost = 1)
    {
            
        //an edge has source desitnation and weight, it goes from seomthing to something weith the wieght inbetween
        this.source = source;
        this.destination = destination;
        this.cost = cost;
    }
    
}

public interface IVertex<T>
{
    T Value { get; set; }
    int IndexInGraph { get; }
    HashSet<Edge<T>> Outgoing { get; }
    Edge<T> AddEdge( IVertex<T> target, float weight = 1);
}

