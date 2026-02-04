using UnityEngine;
// Evolution from Racer + Sliding
public class WallRacer : Upgrade
{
    Ball ballRef;
    private ParticleSystem SparklesParticle;


    public override void apply(Ball ballReff)
    {
        Debug.Log("DodajeszPORTAL??!");
        ballRef = ballReff;
        
        ballRef.WhileColliding += speedBoost;
        ballRef.NoMoreColliding += speedBoostOff;
        
    }


    public void speedBoost(Collision2D Coll, Ball ballRef)
    {
        Rigidbody2D Ballrb = ballRef.GetComponent<Rigidbody2D>();
        ContactPoint2D contact = Coll.GetContact(0);
        

        /////// particles logic ///////

        if (SparklesParticle.emission.enabled == false)
             {
                
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

        
    }

   
    

