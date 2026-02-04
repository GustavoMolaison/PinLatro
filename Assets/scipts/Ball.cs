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
    public Rigidbody2D rb;
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
    public bool isDragging;

    public GameObject BallToMerge;
    private float oldGravityScale;
    private Vector3 currentVelocity;
    private Vector3 lastMousePos = Vector3.zero;
    private Vector3 currentPos;
    public List<UpgradesSO> Upgrades;

    public float OverlapPercentage;
    public static Ball Instance { get; private set; }

    void Start()
    {
        Collider = GetComponent<CircleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        

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


    void OnMouseDown()
    {
        isDragging = true;
        oldGravityScale = rb.gravityScale;
        rb.gravityScale = 0; // Wyłączamy grawitację, żeby kulka nie uciekała w dół podczas trzymania
        rb.linearVelocity = Vector2.zero; // Zatrzymujemy ją w miejscu
        Collider.isTrigger = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        rb.gravityScale = oldGravityScale; // Przywracamy grawitację
        Collider.isTrigger = false;

        if (OverlapPercentage > 50f)
        {
            
            Debug.Log("50% overlap! You can pick this ball for upgrade.");
            // 1. Sprawdź czy manager w ogóle żyje
            if (EvolutionManager.Instance == null) {
            Debug.LogError("Mordo, zapomniałeś wrzucić EvolutionManager na scenę!");
             return;
            }

// 2. Sprawdź czy masz w co uderzyć
                if (BallToMerge == null) {
               Debug.LogWarning("Puszczono myszkę, ale BallToMerge jest nullem - brak celu ewolucji.");
                return;
                    }

// 3. Sprawdź czy cel ma skrypt Ball
                Ball otherBall = BallToMerge.GetComponent<Ball>();
                if (otherBall != null) {
                      EvolutionManager.Instance.GetEvolvedUpgrade(this, otherBall);
          // ... reszta logiki
                } else {
                      Debug.LogError("Uderzyłeś w obiekt z tagiem, ale bez skryptu Ball!");
                        }
            EvolutionManager.Instance.GetEvolvedUpgrade(this, BallToMerge.GetComponent<Ball>());
        }

        // rb.linearVelocity = currentVelocity;
        
    }

    
    void Update()
    {
        ball_mechanics?.Invoke();
        //Debug.Log(isPressed);

        // if(isPressed && Game_manager.Instance.inShop)
        // {
        //     Debug.Log("WCISKAM");
           
        // }
       
        if (isDragging && Game_manager.Instance.inShop)
        {
            Camera mainCam = Camera.main;
            // Calculate the world position correctly
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(mainCam.transform.position.z); // Distance from cam to 2D plane
            Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);

            // currentVelocity = (currentPos - lastMousePos) / Time.deltaTime;
            // lastMousePos = currentPos;

           // Direct position set is often better for UI/Shop dragging 
           // unless you NEED collisions during the drag.
           rb.interpolation = RigidbodyInterpolation2D.Interpolate;
           rb.MovePosition(new Vector2(worldPos.x, worldPos.y));
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
        rb.mass = Mass ?? rb.mass;

        rb.linearDamping = LinearDamping ?? rb.linearDamping;
       
        rb.angularDamping = AngularDampling ?? rb.angularDamping;
        
        rb.gravityScale = GravityScale ?? rb.gravityScale;
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
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Pinball") && Game_manager.Instance.inShop && isDragging == true)
        {
            BallToMerge = other.gameObject;
            if (BallToMerge == null)
            {
                Debug.Log($"nIGGA");
            }
            
            CircleCollider2D otherCircle = other as CircleCollider2D;
           
            OverlapPercentage = GetCircleOverlapPercentage(Collider, otherCircle);
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

     
    public float GetCircleOverlapPercentage(CircleCollider2D colA, CircleCollider2D colB)
{
    float r1 = colA.radius * colA.transform.lossyScale.x; // Uwzględnij skalę obiektu!
    float r2 = colB.radius * colB.transform.lossyScale.x;
    float d = Vector2.Distance(colA.transform.position, colB.transform.position);

    // 1. Brak kontaktu
    if (d >= r1 + r2) return 0f;

    // 2. Jedno koło całkowicie wewnątrz drugiego
    if (d <= Mathf.Abs(r1 - r2))
    {
        float areaA = Mathf.PI * r1 * r1;
        float areaB = Mathf.PI * r2 * r2;
        float intersectionArea = Mathf.Min(areaA, areaB);
        return (intersectionArea / areaA) * 100f;
    }

    // 3. Częściowe pokrycie - Matma soczewki
    float part1 = r1 * r1 * Mathf.Acos((d * d + r1 * r1 - r2 * r2) / (2 * d * r1));
    float part2 = r2 * r2 * Mathf.Acos((d * d + r2 * r2 - r1 * r1) / (2 * d * r2));
    float part3 = 0.5f * Mathf.Sqrt((-d + r1 + r2) * (d + r1 - r2) * (d - r1 + r2) * (d + r1 + r2));

    float totalIntersectionArea = part1 + part2 - part3;
    float areaOfA = Mathf.PI * r1 * r1;

    return (totalIntersectionArea / areaOfA) * 100f;
}
}


    
 

