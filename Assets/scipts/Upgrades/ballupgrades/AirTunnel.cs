
using UnityEngine;

public class AirTunnel : Upgrade
{

private EdgeCollider2D edgeCollider;
private TrailRenderer trailRenderer;
private Ball BallUpgraded;

bool earningPoints = false;
private float pointsPerSecond = 4f;
private float pointsTimer = 1f;

private bool isFading = false;
float currentAlpha = 1f;

[SerializeField] private float speedBoost = 10f;

[SerializeField] private float _updateInterval = 0.05f; // actualization per 0.05s
private float _colliderTimer;
private float _scoreTimer;


    private void Awake()
    {
        
        edgeCollider = GetComponent<EdgeCollider2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        
        
        edgeCollider.enabled = false;
        trailRenderer.enabled = false;

        this.gameObject.SetActive(false);
    }

   

    void Update()
    {
        
        trailRenderer.transform.rotation = Quaternion.identity;
        _colliderTimer += Time.deltaTime;
        _scoreTimer += Time.deltaTime;
        if (_colliderTimer >= _updateInterval)
        {
            UpdateCollider();
            _colliderTimer = 0f;
        }
        if (earningPoints && BallUpgraded != null && _scoreTimer >= pointsTimer)
        {
            Debug.Log("Adding points for Air Tunnel");
            Score_system.Instance.AddpointUpgrades((int)(pointsPerSecond * pointsTimer), BallUpgraded);
            _scoreTimer = 0f;
        }
        
        // 1. Wybierasz cel (0 dla fade, 1 dla widoczności)
        float targetAlpha = isFading ? 0f : 1f;

         // 2. Płynnie zmieniasz wartość ROBOCZĄ
        // currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * 5f);
        currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, Time.deltaTime * 1f);

    
        Gradient grad = trailRenderer.colorGradient;
        GradientAlphaKey[] alphas = grad.alphaKeys;

        
        
        
        alphas[0].alpha = currentAlpha; 
        

        grad.alphaKeys = alphas;
        trailRenderer.colorGradient = grad;

        earningPoints = false;
    }
    public override void apply(Ball ballRef)
    {
        this.gameObject.SetActive(true);
        
        BallUpgraded = ballRef;
     

        ballRef.OnEnterAir += airTunnelOn;
        ballRef.OnLand += airTunnelOff;

        
        trailRenderer.enabled = true;
        
        this.transform.localPosition = Vector3.zero;
        this.transform.localRotation = Quaternion.identity;
        // trailRenderer.transform.localPosition = Vector3.zero;
        // trailRenderer.transform.localRotation = Quaternion.identity;
        // edgeCollider.transform.localPosition = Vector3.zero;
        // edgeCollider.transform.localRotation = Quaternion.identity;
    }



    public void airTunnelOn()
    {
        edgeCollider.enabled = true;
        isFading = false;
        trailRenderer.colorGradient.alphaKeys[0].alpha = 1f;
    }

    public void airTunnelOff()
    {
        edgeCollider.enabled = false;
        isFading = true;
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Pinball")
        {
            
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            // if (rb.linearVelocity.magnitude > 0.01f)
            // {
            //    Debug.Log("Pinball in Air Tunnel");
               Vector2 direction = rb.linearVelocity.normalized;
               rb.AddForce(direction * speedBoost, ForceMode2D.Force);
               earningPoints = true;
            
            //  }
            //  else
            //  {
            //    earningPoints = false;
            //  }
        }
        else
        {
        earningPoints = false;
        }
    
    }
    void UpdateCollider()
    {
        Vector3[] positions = new Vector3[trailRenderer.positionCount];
        trailRenderer.GetPositions(positions);
    
        Vector2[] points = new Vector2[positions.Length];
        for (int i = 0; i < positions.Length; i++) {
         points[i] = transform.InverseTransformPoint(positions[i]);
        }

        edgeCollider.points = points;
    }
}
