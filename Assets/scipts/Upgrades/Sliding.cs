using System;
using Unity.VisualScripting;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    
    public LayerMask wallsLayers;
   
    public float PointsPerDeltaTime = 2;

    public float sliding_bonus_f = 0;
    public int sliding_bonus = 0;


    private ParticleSystem SparklesParticle;
    public static Sliding Instance { get; private set; }

    private void Awake()
    {
        // Jeœli instancja ju¿ istnieje (np. duplikat), niszczymy ten obiekt
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        SparklesParticle = GetComponent<ParticleSystem>();
        SparklesParticle.Stop();
        
        this.gameObject.SetActive(false);

    }

    public void Add_Sliding(Ball Ball_ref)
    {
        //SparklesParticle.transform.localScale = Ball_ref.transform.localScale;
        this.gameObject.SetActive(true);
    
        Ball_ref.WhileColliding += Upgrade_SlidingOn;
        Ball_ref.NoMoreColliding += Upgrade_SlidingOff;
    }



        public void Upgrade_SlidingOn(Collision2D Coll, Ball Ball_ref )
        {

        Rigidbody2D Ballrb = Ball_ref.GetComponent<Rigidbody2D>();
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

        public void Upgrade_SlidingOff(Collision2D Coll, Ball Ball_ref)
        {

         if (SparklesParticle.emission.enabled == true)
           {
            ParticlesOnOf(false);
           }

          Score_system.Instance.AddpointUpgrades(sliding_bonus, Ball_ref);

          sliding_bonus = 0;
          sliding_bonus_f = 0;
        }



        private void ParticlesOnOf(bool active)
    {
        var emission = SparklesParticle.emission;
        emission.enabled = active;
    }
}

