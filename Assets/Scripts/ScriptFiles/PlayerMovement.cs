using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float SpeedForce; // scale of speed apply in movement
    public float JumpForce; // scale of force apply in jump
    public float MaxFallSpeed = -10f; // fall speed limiter
    public float RateofFire; // how fast the gun shot
    public float shootRange = 10f; // maximun shoot range
    public LayerMask shootableLayer; // impact layers of the shot
    public Transform MuzzleGun; // reference of point of shot
    public GameObject[] MuzzleFlashPrefabs; // muzzleflashes prefabs array
   

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    private bool OnGround;
    private float LastShoot;
    

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-4.0f, 4.0f, 4.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);

        Animator.SetBool("running", Horizontal != 0.0f);

        // raycast pos
        Vector3 center = transform.position;
        Vector3 left = transform.position + Vector3.left * 0.5f;
        Vector3 right = transform.position + Vector3.right * 0.5f;

        // draw for debug
        Debug.DrawRay(center, Vector3.down * 1.5f, Color.red);
        Debug.DrawRay(left, Vector3.down * 1.5f, Color.green);
        Debug.DrawRay(right, Vector3.down * 1.5f, Color.blue);

        // check collision of rays
        OnGround = Physics2D.Raycast(center, Vector2.down, 1.5f) ||
                   Physics2D.Raycast(left, Vector2.down, 1.5f) ||
                   Physics2D.Raycast(right, Vector2.down, 1.5f);


        if (Input.GetKeyDown(KeyCode.W) && OnGround)
        {
            Jump();
        }    


        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + RateofFire)
        {
            Shoot();
            LastShoot = Time.time;
        }

    }

    
    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    private void Shoot()
    {
        // shot by player direction
        Vector2 shootDirection = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // initial position of the shot (adjusting by gun muzzle)
        Vector2 shootOrigin = (Vector2)transform.position + new Vector2(shootDirection.x * 0.5f, -0.2f);

        // raycast for collision detect
        RaycastHit2D hit = Physics2D.Raycast(shootOrigin, shootDirection, shootRange, shootableLayer);

        Debug.DrawRay(shootOrigin, shootDirection * shootRange, Color.red, 0.2f);

        

        if (hit.collider != null)
        {
            Debug.Log($"Impacto en: {hit.collider.name}");

            // script for apply dmg
            var targetHealth = hit.collider.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.TakeDamage(1); // apply 1 damage
            }
        }
        else
        {
            Debug.Log("Disparo misseado");
        }

        GenerateMuzzleFlash();
    }

    

    private void GenerateMuzzleFlash()
    {
        if (MuzzleFlashPrefabs.Length > 0)
        {
            // this make a random selection of flashes
            int randomIndex = Random.Range(0, MuzzleFlashPrefabs.Length);
            GameObject selectedFlash = MuzzleFlashPrefabs[randomIndex];

            // this copy the rotation of the character
            Quaternion muzzleRotation = transform.localScale.x > 0
                ? Quaternion.identity // right position
                : Quaternion.Euler(0, 180, 0); // left position
           
            // fixed instance with the correct quaternion
            GameObject flashInstance = Instantiate(selectedFlash, MuzzleGun.position, muzzleRotation);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * SpeedForce, Rigidbody2D.velocity.y);

        if (Rigidbody2D.velocity.y < MaxFallSpeed)
        {
            Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, MaxFallSpeed);
        }
    }

    // method for player death and reset scene

    public void Die()
    {
        Debug.Log("Jugador destruido");        
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }    

}
