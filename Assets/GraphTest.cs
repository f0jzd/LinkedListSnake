using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GraphTest : MonoBehaviour {
	/*[SerializeField]
	private GameObject vertexPrefab;

	private Graph<Transform> graph;
	private Vertex<Transform>[] verticiesArray;

	private void Awake() {
		graph = new Graph<Transform>();
		verticiesArray = new Vertex<Transform>[3];

		for( int i = 0; i < verticiesArray.Length; i++ ) {
			Vector3 position = GetRandomVector3();
			GameObject vertex = Instantiate( vertexPrefab, position, Quaternion.identity );
			verticiesArray[i] = new Vertex<Transform>( vertex.transform, graph );
		}

		Debug.Log( $"Vertices: {graph.NumberOfVertices}" );

		graph.AddEdge( verticiesArray[0], verticiesArray[1], 1 );
		graph.AddEdge( verticiesArray[0], verticiesArray[2], 2 );
		graph.AddEdge( verticiesArray[1], verticiesArray[2], 3 );
		/*graph.AddEdge( verticiesArray[2], verticiesArray[3], 4 );
		graph.AddEdge( verticiesArray[2], verticiesArray[5], 5 );
		graph.AddEdge( verticiesArray[2], verticiesArray[4], 6 );
		graph.AddEdge( verticiesArray[3], verticiesArray[5], 7 );
		graph.AddEdge( verticiesArray[4], verticiesArray[5], 8 );

		Debug.Log( $"Edges: {graph.NumberOfEdges}" );
		for( int i = 0; i < graph.Edges.Length; i++ ) {
			Vector3 pos1 = graph.Edges[i].SourceVertex.Value.position;
			Vector3 pos2 = graph.Edges[i].DestinationVertex.Value.position;
			Debug.DrawLine( pos1, pos2, Color.magenta, 500f );
		}

		//int cost = graph.CalculateDistanceToNeighbours(2);
		//Debug.Log($"Cost of neighbours: {cost}");
	}

	private Vector3 GetRandomVector3() {
		float x = Random.Range( 0, 10f );
		float y = Random.Range( 0, 5f );
		float z = Random.Range( 0, 5f );

		return new Vector3( x, y, z );
	}

	private void OnDrawGizmos() {
		Gizmos.color = Color.magenta;
		for( int i = 0; i < graph.Edges.Length; i++ ) {
			Vector3 pos1 = graph.Edges[i].SourceVertex.Value.position;
			Vector3 pos2 = graph.Edges[i].DestinationVertex.Value.position;
			Gizmos.DrawLine( pos1, pos2 );
		}
	}
}
public class Graph<T> {
		private List<IVertex<T>> verticesList;
		private HashSet<Edge<T>> edges;

		public int NumberOfVertices => verticesList.Count;
		public int NumberOfEdges => edges.Count;

		public IVertex<T>[] Vertices => verticesList.ToArray();
		public Edge<T>[] Edges {
			get {
				Edge<T>[] retval = new Edge<T>[edges.Count];
				edges.CopyTo( retval );
				return retval;
			}
		}

		public Graph() {
			verticesList = new List<IVertex<T>>();
			edges = new HashSet<Edge<T>>();
		}

		public void AddVertex( IVertex<T> vertex ) {
			verticesList.Add( vertex );
		}

		public void AddEdge( IVertex<T> v1, IVertex<T> v2, int cost ) {
			edges.Add( v1.AddEdge( v2, cost ) );
			edges.Add( v2.AddEdge( v1, cost ) );
		}

		public void DepthFirstSearch( IVertex<T> vertice = null ) {
			HashSet<IVertex<T>> visited = new HashSet<IVertex<T>>();
			Stack<IVertex<T>> vStack = new Stack<IVertex<T>>();

			if( vertice == null ) {
				vertice = verticesList[0];
			}

			vStack.Push( vertice );
			while( vStack.Count > 0 ) {
				IVertex<T> current = vStack.Pop();
				if( visited.Contains( current ) ) {
					continue;
				}

				visited.Add( current );
				foreach( Edge<T> e in current.EdgesHashSet ) {
					if( !visited.Contains( e.DestinationVertex ) ) {
						vStack.Push( e.DestinationVertex );
					}
				}
			}
		}

		public void BreadthFrirstSearch( IVertex<T> vertice = null ) {
			HashSet<IVertex<T>> visited = new HashSet<IVertex<T>>();
			Queue<IVertex<T>> vQueue = new Queue<IVertex<T>>();

			if( vertice == null ) {
				vertice = verticesList[0];
			}

			vQueue.Enqueue( vertice );
			while( vQueue.Count > 0 ) {
				IVertex<T> current = vQueue.Dequeue();
				if( visited.Contains( current ) ) {
					continue;
				}

				visited.Add( current );
				foreach( Edge<T> e in current.EdgesHashSet ) {
					if( !visited.Contains( e.DestinationVertex ) ) {
						vQueue.Enqueue( e.DestinationVertex );
					}
				}
			}
		}

		public int CalculateDistanceToNeighbours( int i ) {
			int cost = 0;
			IVertex<T> vertex = verticesList[i];
			for( int e = 0; e < Edges.Length; e++ ) {
				if( Edges[e].SourceVertex == vertex ) {
					cost += Edges[e].Cost;
				}
			}

			return cost;
		}


		//Calculate distance to each neighbour
		//Distance = edge cost 

		//Get it's neighbours: see how many edges/this the source vertex
		// Get the cost of each edge


	}
public class Vertex<T> : IVertex<T> {
	private T value;
	private HashSet<Edge<T>> edgesHashSet;
	private int indexInGraph;

	public Type Type => typeof(T); // Why with big T?

	public T Value => value;
	public int IndexInGraph => indexInGraph;
	public HashSet<Edge<T>> EdgesHashSet => edgesHashSet;

	public Edge<T> AddEdge( IVertex<T> target, int cost = 1 ) {
		Edge<T> edge = new Edge<T>( this, target, cost );
		edgesHashSet.Add( edge );
		return edge;
	}

	public Vertex( T value ) {
		this.value = value;
		this.edgesHashSet = new HashSet<Edge<T>>();
	}

	public Vertex( T value, Graph<T> graph ) {
		this.value = value;
		this.edgesHashSet = new HashSet<Edge<T>>();
		this.indexInGraph = graph.NumberOfVertices;
		graph.AddVertex( this );
	}

}
public interface IVertex<T> {
	T Value { get; }
	int IndexInGraph { get; }
	HashSet<Edge<T>> EdgesHashSet { get; }
	Edge<T> AddEdge( IVertex<T> target, int cost = 1 );
}
public class Edge<T> {
	IVertex<T> sourceVertex;
	private IVertex<T> destinationVertex;
	private int cost;

	public IVertex<T> SourceVertex => sourceVertex;
	public IVertex<T> DestinationVertex => destinationVertex;
	public int Cost => cost;

	public Edge( IVertex<T> sourceVertex, IVertex<T> destinationVertex, int cost = 1 ) {
		this.sourceVertex = sourceVertex;
		this.destinationVertex = destinationVertex;
		this.cost = cost;
	}*/

}


