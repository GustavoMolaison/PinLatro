using System.Runtime.CompilerServices;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;


public class Ball : MonoBehaviour
{



    [Header("Settings")]
    [Tooltip("RespawnPoint")]
    public Transform targetDestination;

    [Tooltip("Respawn line trigger")]

    public CircleCollider2D Collider { get; set; }
    public Rigidbody2D Rb;
    //List<Action> ball_mechanics = new List<Action>();
    public Action ball_mechanics;
    public event Action<Collision2D, Ball> OnHitEvent;
    public event Action OnHitEventNoParam;
    public event Action<Collision2D, Ball> WhileColliding;
    public event Action<Collision2D, Ball> NoMoreColliding;


    public bool ball_out_of_pit = false;
    public static Ball Instance { get; private set; }

    void Start()
    {
        Collider = GetComponent<CircleCollider2D>();
        Rb = GetComponent<Rigidbody2D>();

    }


    // Update is called once per frame
    void Update()
    {
        ball_mechanics?.Invoke();

        //Debug.Log(Rb.mass);
    }

    public void AddMechanic(Action mechanic)
        {
          ball_mechanics += mechanic;
        }
    public void ModifyBall(float? Mass = null,
                    float? LinearDamping = null,
                    float? AngularDampling = null,
                    float? GravityScale = null
                    )
    {
        Rb.mass = Mass ?? Rb.mass;

        Rb.linearDamping = LinearDamping ?? Rb.linearDamping;
       
        Rb.angularDamping = AngularDampling ?? Rb.angularDamping;
        
        Rb.gravityScale = GravityScale ?? Rb.gravityScale;
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("RespawnLine"))
        {
            BallToSpawn();
        }
        if (other.CompareTag("TimerStart"))
        {
            
            
            ball_out_of_pit = !ball_out_of_pit;
           
        }
    }

    public void OnCollisionEnter2D(Collision2D collision) 
    {
        OnHitEvent?.Invoke(collision, this);
        OnHitEventNoParam?.Invoke();

    }
    public void OnCollisionStay2D(Collision2D collision)
    {
        WhileColliding?.Invoke(collision, this);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        NoMoreColliding?.Invoke(collision, this);
    }

    public void BallToSpawn() {
        transform.localPosition = new Vector2(0, 0);
        ball_out_of_pit = false;
    }

}


    
 

