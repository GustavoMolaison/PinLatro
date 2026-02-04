using System;
using Unity.VisualScripting;
using UnityEngine;

public class Sliding : Upgrade
{
    public override string UpgradeName => "Sliding";

    public LayerMask wallsLayers;
   
    public float PointsPerDeltaTime = 2;

    public float sliding_bonus_f = 0;
    public int sliding_bonus = 0;


    private ParticleSystem SparklesParticle;

    public static Sliding Instance { get; private set; }
    private void Awake()
    {
        SparklesParticle = GetComponent<ParticleSystem>();
        SparklesParticle.Stop();

        this.gameObject.SetActive(false);
    }

    //private void Start()
    //{
    //    SparklesParticle = GetComponent<ParticleSystem>();
    //    SparklesParticle.Stop();
        
    //    this.gameObject.SetActive(false);

    //}

    public override void apply(Ball ballRef)
    {
        //SparklesParticle.transform.localScale = ballRef.transform.localScale;
        this.gameObject.SetActive(true);
    
        ballRef.WhileColliding += Upgrade_SlidingOn;
        ballRef.NoMoreColliding += Upgrade_SlidingOff;
    }



        public void Upgrade_SlidingOn(Collision2D Coll, Ball ballRef )
        {

        Rigidbody2D Ballrb = ballRef.GetComponent<Rigidbody2D>();
        ContactPoint2D contact = Coll.GetContact(0);
        



        if (SparklesParticle.emission.enabled == false)
             {
                //SparklesParticle.Play();
                ParticlesOnOf(true);
             }

      

        SparklesParticle.transform.position = contact.point;
        float angle = Mathf.Atan2(contact.normal.y, contact.normal.x) * Mathf.Rad2Deg;
        float horizontalSpeed = Ballrb.linearVelocity.x;
        float speed = Ballrb.linearVelocity.magnitude;

        //Debug.Log(speed);

        var shapeModule = SparklesParticle.shape;
        var velocity = SparklesParticle.velocityOverLifetime;
        var emission = SparklesParticle.emission;
        //shapeModule.sprite = BallSprite;
        shapeModule.rotation = new Vector3(0, 0, angle - 90f);

        emission.rateOverTime = speed;
        

        if (horizontalSpeed > 0)
        {
            velocity.x = -speed * 0.5f;
        }
        else
        {
            if (horizontalSpeed < 0)
            {
                velocity.x = speed * 0.5f;
            }
        }



                //Debug.Log("naliczamy bonus");
                sliding_bonus_f += PointsPerDeltaTime * Time.deltaTime;
                sliding_bonus = (int)sliding_bonus_f;

            }

        public void Upgrade_SlidingOff(Collision2D Coll, Ball ballRef)
        {

         if (SparklesParticle.emission.enabled == true)
           {
            ParticlesOnOf(false);
           }

          Score_system.Instance.AddpointUpgrades(sliding_bonus, ballRef);

          sliding_bonus = 0;
          sliding_bonus_f = 0;
        }



        private void ParticlesOnOf(bool active)
    {
        var emission = SparklesParticle.emission;
        emission.enabled = active;
    }
}

