using System;
using UnityEngine;
using System.Collections;

public class Racer : Upgrade
{
   

    private Ball BallUpgraded;
    private Rigidbody2D BallRb;

    private float BallVelocity;
     // Saves Velocity on the previous frame (Before Colission)
    private float[] VelocitySave = {0, 0};

    private float PointsToGive;
    private int PointsToGiveInt;

    private ParticleSystem RacerParticles;

    [SerializeField] private float PointsMultiplier = 0.1f;

    public static Racer Instance { get; private set; }
    private void Awake()
    {
        RacerParticles = GetComponent<ParticleSystem>();
        RacerParticles.Stop();

        this.gameObject.SetActive(false);
    }

    //private void Start()
    //{
    //    RacerParticles = GetComponent<ParticleSystem>();
    //    RacerParticles.Stop();

    //    this.gameObject.SetActive(false);

    

    // Update is called once per frame
    void Update()
       {
        if(BallUpgraded != null)
        {
            if (BallRb == null)
            {
                BallRb = BallUpgraded.GetComponent<Rigidbody2D>();
            }

            BallVelocity = BallRb.linearVelocity.magnitude;
            VelocitySave[1] = VelocitySave[0];
            VelocitySave[0] = BallVelocity;

        }
        
        
       }


    public override void apply(Ball ballRef)
    {
        this.gameObject.SetActive(true);
        ParticlesOnOf(false);
        BallUpgraded = ballRef;
     

        ballRef.OnHitEventNoParam += UpgradeRacer;
    }


    public void UpgradeRacer()
    {

        

        if (VelocitySave[1] > 10)
        {
            PointsToGive = VelocitySave[0] * PointsMultiplier;
            PointsToGiveInt = (int)PointsToGive;
        }

        
        Score_system.Instance.AddpointUpgrades(PointsToGiveInt, BallUpgraded);

        RacerParticles.transform.position = BallUpgraded.transform.position;

        // Poprawka CS0618: u�yj main.startSpeed zamiast przestarza�ego startSpeed
        var mainModule = RacerParticles.main;
        mainModule.startSpeed = VelocitySave[1];



        if (VelocitySave[0] < 70)
        {
            //Debug.Log((int)VelocitySave[1] / 7);
            RacerParticles.Emit((int)VelocitySave[1] / 5);
        }




       
    }

    private void ParticlesOnOf(bool active)
    {
        var emission = RacerParticles.emission;
        emission.enabled = active;
    }

    //private IEnumerator DisableParticlesWithDelay(float delay)
    //{

    //    yield return new WaitForSeconds(delay);


    //    ParticlesOnOf(false);
    //}
}
