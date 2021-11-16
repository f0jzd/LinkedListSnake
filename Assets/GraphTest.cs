using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphTest : MonoBehaviour
{
    [SerializeField] private GameObject vertexPrefab;
    private Pathfinding pathfinder;
    // Start is called before the first frame update
    void Start()
    {
        
        for( int i = 0; i < pathfinder.verticesArray.Length; i++ ) {
            Vector3 position = GetRandomVector3();
            GameObject vertex = Instantiate( vertexPrefab, position, Quaternion.identity );
           pathfinder.verticesArray[i] = new Vertex<Transform>( vertex.transform, pathfinder.graph );
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private Vector3 GetRandomVector3()
    {
        float x = Random.Range( 0, 10f );
        float y = Random.Range( 0, 5f );
        float z = Random.Range( 0, 5f );
        
        return new Vector3( x, y, z );
    }
}
