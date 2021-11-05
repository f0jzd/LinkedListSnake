using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookManager : MonoBehaviour
{
    private SnakeMovement player;
    
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<SnakeMovement>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        
        if (other.CompareTag("Food"))
        {
            player._hasEaten = true;
            Destroy(other.gameObject);
        }
    }
}
