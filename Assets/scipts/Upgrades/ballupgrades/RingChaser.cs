using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Android;

public class RingChaser : Upgrade
{
    private float chaseTimer = 0f;
    [SerializeField] private  float chaseCooldown = 5f;
    private float chaseBacktoBackTimer = 0f;
    [SerializeField] private float chaseBackToBackCooldown = 0.5f;
    private float lineRTimer = 0f;
    [SerializeField] private float lineRTimerCooldown = 0.5f;

    [SerializeField] private LayerMask ringMask;
  
    [SerializeField] private float chaseSpeed = 10f;
    [SerializeField] private float detectionRadius = 1f;
    [SerializeField] private int BtbChaseLimit = 3;


    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private LineRenderer lineRenderer;
   
    

    private float closestDistance = 10000f;

    private Collider2D chosenRing;
    private Collider2D chosenRingSave;
    private Ball ballRef2;


    private int BtbChaseCounter;

    Rigidbody2D ballRb;

    Vector2 ringDirection;
    Vector3 ringLocalCords;
    Vector3 ringLocalCordsSave;
    private void Awake()
    {
        this.gameObject.SetActive(false);

        //lineRenderer.positionCount = 2;
        //lineRenderer.SetPosition(0, Vector3.zero);
        //lineRenderer.SetPosition(1, Vector3.zero);


    }

   
    
    public override void apply(Ball ballRef)
    {

        this.gameObject.SetActive(true);
        ballRef.ball_mechanicsBallref += chaseRing;

        ballRef2 = ballRef;

        this.transform.localPosition = Vector3.zero;
       // Strange equation to make the indicator correct size i will forget why it looks like this in 5 min actually i dont really know even know it just fits so dont touch it i guess
        transform.localScale = new Vector3(detectionRadius * 4, detectionRadius * 4, 1);
    }
    void LateUpdate()
    {
        // To sprawi, ¿e Sprite zawsze bêdzie w tym samym miejscu œwiata, 
        // nawet jak rodzic nim "wywija"
        transform.rotation = Quaternion.identity;
    }
    public void chaseRing(Ball ballRef)
    {

        
        
        if (lineRTimer == 0 || lineRTimer > lineRTimerCooldown)
        {
            lineRenderer.SetPosition(1, Vector3.zero);
        }
        else
        {
            Debug.Log("spisz?");
            lineRTimer += Time.deltaTime;
            ringLocalCordsSave = lineRenderer.transform.InverseTransformPoint(chosenRingSave.gameObject.transform.position);
            lineRenderer.SetPosition(1, ringLocalCordsSave);
            if (lineRTimer > lineRTimerCooldown)
            {
                lineRTimer = 0;
            }
        }

        if ((chaseTimer == 0 || chaseTimer > chaseCooldown) && ballRef.ball_out_of_pit == true)
        {
            chaseTimer = 0;
            SpriteRenderer.enabled = true;

            if (chaseBacktoBackTimer == 0 || chaseBacktoBackTimer > chaseBackToBackCooldown)
            {


                chaseBacktoBackTimer = 0;

                ballRb = ballRef.GetComponent<Rigidbody2D>();

                Collider2D[] foundRings = Physics2D.OverlapCircleAll(ballRef.transform.position, detectionRadius, ringMask);
                closestDistance = 10000f;
                chosenRing = null;
                foreach (Collider2D ring in foundRings)
                {
                    //Debug.Log("Ring found");
                    float currentDistance = Vector2.Distance(transform.position, ring.transform.position);
                    if (currentDistance < closestDistance)
                    {
                        closestDistance = currentDistance;
                        chosenRing = ring;
                        chosenRingSave = ring;


                    }
                }

                if (chosenRing != null)
                {
                    ringDirection = chosenRing.gameObject.transform.position - ballRef.transform.position;

                    Debug.Log($"napieprza {BtbChaseCounter}");

                   
                    // Line rendered stuff 
                    ringLocalCords = lineRenderer.transform.InverseTransformPoint(chosenRing.gameObject.transform.position);
                    lineRenderer.SetPosition(1, ringLocalCords);
                    lineRTimer += Time.deltaTime;
                    // Line rendered stuff 

                    ballRb.AddForce(ringDirection * chaseSpeed);

                    chaseBacktoBackTimer += Time.deltaTime;
                    BtbChaseCounter += 1;

                    if (BtbChaseCounter == BtbChaseLimit)
                    {
                        BtbChaseCounter = 0;
                        chaseBacktoBackTimer = 0;
                        chaseTimer += Time.deltaTime;
                    }
                }
                else
                {
                    if (BtbChaseCounter != 0)
                    {
                        chaseTimer += Time.deltaTime;
                    }
                    BtbChaseCounter = 0;
                    chaseBacktoBackTimer = 0;

                }


            }

            else
            {
                chaseBacktoBackTimer += Time.deltaTime;
            }






        }
        else
        {
            chaseTimer += Time.deltaTime;
            SpriteRenderer.enabled = false;
        }

        

    }

    void OnDrawGizmosSelected()
    {
        // Ustawiamy kolor (¿ó³ty, ¿eby bi³ po oczach)
        Gizmos.color = Color.yellow;

        // Rysujemy druciane ko³o w miejscu kulki o zadanym promieniu
        Gizmos.DrawWireSphere(ballRef2.transform.position, detectionRadius);
    }
}
