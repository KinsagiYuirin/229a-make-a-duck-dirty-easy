using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    
    [Header("Player Status")]
    [SerializeField] private int health = 10;
    public int Health { get => health; set => health = value;}
    
    [SerializeField] private int maxHealth = 10;
    
    [Header("Player Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    
    [Header("Player Shooting")]
    [SerializeField] private Rigidbody2D bullet;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject crossHair;
    
    [SerializeField] private bool canJump = false;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        Time.timeScale = 1;
        //InGameUI.instance.HideCursor();
        
        rb = GetComponent<Rigidbody2D>();
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        MoveMent();
        Jump();
        PlayerDie();
        playerShoot();
    }

    void MoveMent()
    {
        float move = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(move * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            canJump = false;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            canJump = true;
            //CameraController.instance.isPlayerGrounded = true;
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            CameraController.instance.isPlayerGrounded = false;
        }
    }
    
    private void PlayerDie()
    {
        if (health == 0)
        {
            InGameUI.instance.OnDeadPanel();
        }
    }

    void playerShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * 10f, Color.green, 10f);

            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                crossHair.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log($"hit point: ({hit.point.x}, {hit.point.y})");

                Vector2 projectile = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);
                Rigidbody2D firedBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
                firedBullet.velocity = projectile;
            }
        }
        
        Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 crossHair, float t)
        {
            Vector2 distance = crossHair - origin;

            float distX = distance.x;
            float distY = distance.y;

            float velocityX = distX / t;
            float velocityY = distY / t + 0.5f * Mathf.Abs(Physics2D.gravity.y) * t;

            Vector2 result = new Vector2(velocityX, velocityY);
            return result;
        }
    }
}
