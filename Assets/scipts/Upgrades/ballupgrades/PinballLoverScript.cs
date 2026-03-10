using UnityEngine;
using UnityEngine.UIElements;

public class PinballLoverScript : Upgrade
{
    [SerializeField] private float detectionRadius = 2f;
    [SerializeField] private LayerMask pinballMask;
    [SerializeField] private int pointsPerPinball = 1;

    private float timer;
    [SerializeField] private float Cooldown = 0.5f;

    [SerializeField] SpriteRenderer MSpriteRenderer;

    Ball ballRef2;
    private void Awake()
    {
        this.gameObject.SetActive(false);
        transform.localScale =  new Vector2(detectionRadius * 4, detectionRadius * 4);
        transform.localPosition = Vector2.zero;
    }



    public override void apply(Ball ballRef)
    {

        this.gameObject.SetActive(true);
        ballRef.ball_mechanicsBallref += lookForPinBallFriends;
        ballRef2 = ballRef;
  
    }

    public void lookForPinBallFriends(Ball ballRef)
    {
        if (ballRef.ball_out_of_pit)
        {
            MSpriteRenderer.enabled = true;
        }
        else
        {
            MSpriteRenderer.enabled = false;
        }

        timer += Time.deltaTime;
        if (timer > Cooldown)
        {
            Collider2D[] foundPinballs = Physics2D.OverlapCircleAll(ballRef.transform.position, detectionRadius, pinballMask);
            int PinballsCount = foundPinballs.Length;
            if ((PinballsCount - 1) > 0)
            {
                Score_system.Instance.AddpointUpgrades(PinballsCount * pointsPerPinball, ballRef);
            }
            timer = 0;
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
