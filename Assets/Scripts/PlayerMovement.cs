using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float SpeedForce;
    public float JumpForce;
    public float RateofFire;
    
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

        Debug.DrawRay(transform.position, Vector3.down * 1.5f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 1.5f))
        {
            OnGround = true;
        }
        else OnGround = false;

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
        Vector3 direction;
        if (transform.localScale.x == 4.0f) direction = Vector3.right;
        else direction = Vector3.left;
        
        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 0.1f, Quaternion.identity);
        bullet.GetComponent<BulletScript>().SetDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * SpeedForce, Rigidbody2D.velocity.y); 
    }

    //Method for player death and reset scene

    public void Die()
    {
        Debug.Log("Jugador destruido");        
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
