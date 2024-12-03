using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.CompareTag("Player"))  // Check if the collision is on object with "player" tag
        {
            
            collision.GetComponent<PlayerMovement>().Die(); // call the "Die" method
        }
    }
}
