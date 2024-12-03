using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZMovement : MonoBehaviour
{
    public float PatrolSpeed = 2.0f;         
    public float ChaseSpeed = 4.0f;         
    public float DetectionRange = 5.0f;     
    public float ChangeDirectionTime = 3.0f;
    

    private Rigidbody2D Rigidbody2D;
    private Vector2 PatrolDirection = Vector2.right;
    private float ChangeDirectionTimer;
    private Transform PlayerTransform;
    private bool IsChasing = false;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        ChangeDirectionTimer = ChangeDirectionTime;

        

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerTransform = player.transform;
        }
    }

    void Update()
    {
       
        if (PlayerTransform != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, PlayerTransform.position);
            IsChasing = distanceToPlayer <= DetectionRange;
        }
    }

    void FixedUpdate()
    {
        if (IsChasing)
            {
                Vector2 directionToPlayer = (PlayerTransform.position - transform.position).normalized;
                Rigidbody2D.velocity = new Vector2(directionToPlayer.x * ChaseSpeed, Rigidbody2D.velocity.y);
            }
        else
            {
               Patrol();
            }
       
    }
    
    private void Patrol()
    {
        ChangeDirectionTimer -= Time.deltaTime;

        if (ChangeDirectionTimer <= 0)
        {
            
            PatrolDirection = -PatrolDirection;
            ChangeDirectionTimer = ChangeDirectionTime;

            
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }

        Rigidbody2D.velocity = PatrolDirection * PatrolSpeed;
    }

    // From here on, the function of receiving hits from the enemy is written.

    public int MaxHits = 5; // hits needed for die      

    private int CurrentHits = 0; // count of hits received
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet")) 
        {
            CurrentHits++;
            Destroy(collision.gameObject);

            

            if (CurrentHits >= MaxHits)
            {
                Destroy(gameObject); 
            }
        }
    }

    // From here on, the function of hitting the player is written.

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {           
            PlayerMovement player = collision.gameObject.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.Die();
            }
        }
    }

}

