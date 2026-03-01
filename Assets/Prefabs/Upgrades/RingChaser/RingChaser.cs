using UnityEditor.Callbacks;
using UnityEngine;

public class RingChaser : Upgrade
{
    float chaseTimer = 0;
    float chasecooldown = 5f;

    [SerializeField] float chaseSpeed = 10f;

    Rigidbody2D ballRb;

    Vector2 ringDirection;
    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public override void apply(Ball ballRef)
    {
        
        this.gameObject.SetActive(true);
        ballRef.activateRings = false;
        ballRef.OnHitEvent += chaseRing;
    }

    public void chaseRing(Collision2D collision, Ball ballRef)
    {
        ballRb = ballRef.GetComponent<Rigidbody2D>();
        if(collision.gameObject.tag == "PointRing")
        {
            ringDirection = collision.gameObject.transform.position - ballRef.transform.position;
            ballRb.linearVelocity = ringDirection.normalized * 10f;

        }
    }

}
