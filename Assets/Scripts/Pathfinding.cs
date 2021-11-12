using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Pathfinding : MonoBehaviour
{
    public Graph<int> graph = new Graph<int>();
    
    private CameraManager _cameraManager;

    public Vertex<Transform>[] vertices = new Vertex<Transform>[]{};
    

        // Start is called before the first frame update
    void Start()
    {

        _cameraManager = FindObjectOfType<CameraManager>();
        
        
        
        for (int i = 0; i < _cameraManager.BoardWidth; i++)
        {
            
            for (int j = 0; j < _cameraManager.BoardHeight; j++)
            {
                vertices = new Vertex<Transform>[]
                {
                    
                    

                };
                

            }
            
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Graph<T>
{
    private List<IVertex<T>> _vertices;    //Using list isntead of hashset since list is ordered and easier to print
    private HashSet<Edge<T>> _edges; //Used to inspect the graph
    
    
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

    public void AddEdge(IVertex<T> v1, IVertex<T> v2, float weight = 1f)
    {
            
        //This would be enough but..?
        //One position represent two relations, depends on how we want to think about it when we calculate the size.
        _edges.Add(v1.AddEdge(v2, weight));
        _edges.Add(v2.AddEdge(v1, weight));

    }
        
        
    
}

public class Vertex<T> : IVertex<T>
{
    private T _value;//Vertex Data, it is refered to IVertex it doesent know what type is it
    private Type _type;
    private HashSet<Edge<T>> outgoing; //List of edges, these are the edges going out from this vertex
    private int indexInGraph;
    
    public Type Type => _type;
    public T Value => _value;
    public int IndexInGraph => indexInGraph;
    public HashSet<Edge<T>> EdgesHashSet { get; }
    public Edge<T> AddEdge(IVertex<T> target, float weight = 1)
    {
        Edge<T> edge = new Edge<T>(this, target, weight);
        outgoing.Add(edge);
        return edge;
    }


    public HashSet<Edge<T>> Edges => outgoing;
    


    
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
    private float cost;//The cost of this conneection, i.e. a geographical desintation would be the distance, its what the graph represents.
    //This of vertices are places in a map and weight the is cost to get there.

    public IVertex<T> Source => source;

    public IVertex<T> Destination => destination;

    public float Cost => cost;

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
    T Value { get; }
    int IndexInGraph { get; }
    HashSet<Edge<T>> EdgesHashSet { get; }
    Edge<T> AddEdge( IVertex<T> target, float weight = 1 );
}
