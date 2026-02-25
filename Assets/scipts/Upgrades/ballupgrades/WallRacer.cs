using UnityEngine;
// Evolution from Racer + Sliding
public class WallRacer : Upgrade
{
    Ball ballRef;
    Rigidbody2D BallRb;
    private ParticleSystem SparklesParticle;

    private float BallVelocity;
    private float[] VelocitySave = {0, 0};

    private float PointsToGive;
    private int PointsToGiveInt;
    [SerializeField] private float PointsMultiplier = 0.1f;
    
   
    private void Awake()
    {
        SparklesParticle = GetComponent<ParticleSystem>();
        SparklesParticle.Stop();

        this.gameObject.SetActive(false);
    }
    public override void apply(Ball ballReff) 
    {
        
        this.gameObject.SetActive(true);
        ballRef = ballReff;
        
        ballRef.WhileColliding += speedBoost;
        ballRef.NoMoreColliding += speedBoostOff;

        ballRef.OnHitEventNoParam += racerEffect;
        
    }

    void Update()
       {
        if(ballRef != null)
        {
            if (BallRb == null)
            {
                BallRb = ballRef.GetComponent<Rigidbody2D>();
            }

            BallVelocity = BallRb.linearVelocity.magnitude;
            VelocitySave[1] = VelocitySave[0];
            VelocitySave[0] = BallVelocity;

        }
        
        
       }


    public void speedBoost(Collision2D Coll, Ball ballRef)
    {
        Rigidbody2D Ballrb = ballRef.GetComponent<Rigidbody2D>();
        ContactPoint2D contact = Coll.GetContact(0);
        

        /////// particles logic ///////

        if (SparklesParticle.emission.enabled == false)
             {
                Debug.Log("PARTICLES ON");
                ParticlesOnOf(true);
             }

      

        SparklesParticle.transform.position = contact.point;
        float angle = Mathf.Atan2(contact.normal.y, contact.normal.x) * Mathf.Rad2Deg;
        float horizontalSpeed = Ballrb.linearVelocity.x;
        float speed = Ballrb.linearVelocity.magnitude;

       

        var shapeModule = SparklesParticle.shape;
        var velocity = SparklesParticle.velocityOverLifetime;
        var emission = SparklesParticle.emission;

        shapeModule.rotation = new Vector3(0, 0, angle - 90f);
        emission.rateOverTime = speed;
        
       ///////// Boost logcic //////////

        Ballrb.AddForce(Ballrb.linearVelocity.normalized * 5f * Ballrb.mass, ForceMode2D.Force);
        


               
                
                

        }

        public void speedBoostOff(Collision2D Coll, Ball ballRef)
        {
         if (SparklesParticle.emission.enabled == true)
           {
            ParticlesOnOf(false);
           }
        }

        private void ParticlesOnOf(bool active)
       {
        var emission = SparklesParticle.emission;
        emission.enabled = active;
       }

        
    

    public void racerEffect()
    {
     
    

        

        if (VelocitySave[1] > 10)
        {
            PointsToGive = VelocitySave[0] * PointsMultiplier;
            PointsToGiveInt = (int)PointsToGive;
        }

        
        Score_system.Instance.AddpointUpgrades(PointsToGiveInt, ballRef);
    }

}



   
    

