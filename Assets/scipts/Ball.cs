using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;


public class Ball : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
{



    [Header("Settings")]
    [Tooltip("RespawnPoint")]
    

   

    public CircleCollider2D Collider { get; set; }
    public Rigidbody2D Rb;
    //List<Action> ball_mechanics = new List<Action>();
    public Action ball_mechanics;
    public event Action<Collision2D, Ball> OnHitEvent;
    public event Action OnHitEventNoParam;
    public event Action<Collision2D, Ball> WhileColliding;
    public event Action<Collision2D, Ball> NoMoreColliding;

    public GameObject whenPickedBloom;

    public bool ball_out_of_pit = false;
    public bool InLaunchPad;
    public bool inWaitingRoom;
    public bool isBlooming = false;
    public bool isPressed;

    public List<UpgradeType> Upgrades;
    public static Ball Instance { get; private set; }

    void Start()
    {
        Collider = GetComponent<CircleCollider2D>();
        Rb = GetComponent<Rigidbody2D>();

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        whenPickedBloom.SetActive(!whenPickedBloom.activeSelf);
        isBlooming = !isBlooming;
        PinBallsManager.Instance.oneBallPicked(this);
        

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
      
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
        
    }

    
    void Update()
    {
        ball_mechanics?.Invoke();
        //Debug.Log(isPressed);

        if(isPressed && Game_manager.Instance.inShop)
        {
            Debug.Log("WCISKAM");
           
        }
       

       
    }

    public void ResetUpgrades()
    {
        
        NoMoreColliding = null;
        WhileColliding = null;
        OnHitEventNoParam = null;
        OnHitEvent = null;
        ball_mechanics = null;

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
            
            BallToWaitingRoom();
        }
        if (other.CompareTag("TimerStart"))
        {
           
            ball_out_of_pit = !ball_out_of_pit;


            InLaunchPad = !InLaunchPad;
            if (InLaunchPad == true)
            {
                PinBallsManager.Instance.ballsInLaunchPad.Add(this);
            }
            else
            {
                PinBallsManager.Instance.ballsInLaunchPad.Remove(this);
            }



            
            
           
        }
    }

    public void BallToSpawn()
    {
        transform.position = PinBallsManager.Instance.spawnBalls.position;
        ball_out_of_pit = false;
        InLaunchPad = true;
        inWaitingRoom = false;



        this.transform.localScale *= 1 / 3f;
        
        if (this.transform.localScale.x != 0.5 || this.transform.localScale.y != 0.5)
        {
            this.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
        }

        if (!PinBallsManager.Instance.ballsInLaunchPad.Contains(this))
        {
            PinBallsManager.Instance.ballsInLaunchPad.Add(this);
        }


       

        if (PinBallsManager.Instance.ballsInQueue.Count > 0)
        {
            PinBallsManager.Instance.ballsInQueue.RemoveAt(0);
        }
    }

    public void BallToWaitingRoom()
    {
        transform.position = PinBallsManager.Instance.waitingRoom.position;
        ball_out_of_pit = false;
        InLaunchPad = false;
        inWaitingRoom = true;



        this.transform.localScale *= 3f;
       
        

        if (PinBallsManager.Instance.ballsInLaunchPad.Contains(this))
        {
            PinBallsManager.Instance.ballsInLaunchPad.Remove(this);
        }
        if (!PinBallsManager.Instance.ballsInQueue.Contains(this))
        {
            PinBallsManager.Instance.ballsInQueue.Add(this);
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

    

}


    
 

