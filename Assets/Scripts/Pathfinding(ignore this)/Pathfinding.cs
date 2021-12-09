using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pathfinding : MonoBehaviour
{
    public Graph<Transform> graph = new Graph<Transform>();
    public Vertex<Transform>[,] verticesArray;
    
    private CameraManager _cameraManager;


    private void Awake()
    {
        _cameraManager = FindObjectOfType<CameraManager>();
        verticesArray = new Vertex<Transform>[_cameraManager.BoardWidth,_cameraManager.BoardHeight];
    }
    
    /*public void findPath(Vertex<Transform> start, Vertex<Transform> target)
    {
        List<Vertex<Transform>> open = new List<Vertex<Transform>>();
        List<Vertex<Transform>> closed = new List<Vertex<Transform>>();
        
        float fCost = 0;
        float hValue = GetDistance(start, target);
        
        open.Add(start);
        
        
        foreach (var edge in start.Outgoing)
        {
            
            if (fCost == 0)
            {
                fCost = edge.Cost + hValue;
            }

            if (fCost > edge.Cost + hValue)
            {
                fCost = edge.Cost + hValue;
                var current = edge;
            }

            open.Remove(start);
            closed.Add(start);

            if (edge.Source == target)
            {
                return;
            }

            foreach (var edge2 in edge.Destination.Outgoing)
            {
                if (fCost > edge2.Cost + hValue || open.Contains(edge2))
                {
                    edge2.Source.F = edge2.Cost + hValue;
                    edge2.Destination = edge.Source;

                    if (!open.Contains(edge2))
                    {
                        open.Add(edge2);
                    }
                }
            }


        }
    }*/
    

    private float GetDistance(Vertex<Transform> current, Vertex<Transform> Destination)
    {
        
        float x = Mathf.Abs(Destination.Value.transform.position.x - current.Value.transform.position.x);
        float y = Mathf.Abs(Destination.Value.transform.position.y - current.Value.transform.position.y);
        
        return x + y;
    }
    
    
    

    
}



public class Graph<T>
{
    private List<Vertex<T>> _vertices;    //Using list instead of hashset since list is ordered and easier to print
    private HashSet<Edge<T>> _edges;       //Used to inspect the graph
    
    public int Order => _vertices.Count;
    public int Size => _edges.Count;
    
    

    public Vertex<T>[] Vertices => _vertices.ToArray();

    public Edge<T>[] Edges
    {
        get
        {
            Edge<T>[] returnValue = new Edge<T>[_edges.Count];
            _edges.CopyTo(returnValue);
            return returnValue;
        }
    }
    public Graph()
    {
        _vertices = new List<Vertex<T>>();
        _edges = new HashSet<Edge<T>>();
    }


    public void AddVertex(Vertex<T> vertex)
    {
        _vertices.Add(vertex);
    }

    public void AddEdge(Vertex<T> v1, Vertex<T> v2, float weight)
    {
        _edges.Add(v1.AddEdge(v2, weight));
    }
}

public class Vertex<T>
{
    private T _value;
    private Type _type;
    private HashSet<Edge<T>> outgoing; 
    private int indexInGraph;


    public float G { get; set; } = 0;

    public float H { get; set; } = 0;

    public float F { get; set; } = 0;

    public Type Type => _type;
    public T Value
    {
        get => _value;
        set => _value = value;
    }

    public int IndexInGraph => indexInGraph;
    public HashSet<Edge<T>> Outgoing => outgoing;
    public Edge<T> AddEdge(Vertex<T> target, float weight = 1)
    {
        Edge<T> edge = new Edge<T>(this, target, weight);
        outgoing.Add(edge);
        return edge;
    }

    public Vertex(T _value, Graph<T> G)
    {
        this._value = _value;
        outgoing = new HashSet<Edge<T>>();
        indexInGraph = G.Order;
        G.AddVertex(this);
        
    }
    
}

public class Edge<T>
{
    Vertex<T> source;
    private Vertex<T> destination;
    private float cost;
    
    public float HCost { get; set; }

    //The cost of this conneection, i.e. a geographical desintation would be the distance, its what the graph represents.
    //This of vertices are places in a map and weight the is cost to get there.

    public Vertex<T> Source => source;

    public Vertex<T> Destination
    {
        get => destination;
        set => destination = value;
    }

    public float Cost
    {
        get => cost;
        set => cost = value;
    }

    public Edge(Vertex<T> source,Vertex<T> destination, float cost = 1)
    {
        this.source = source;
        this.destination = destination;
        this.cost = cost;
    }
    
}


