using System;
using UnityEngine;

public class Racer : MonoBehaviour
{
    private Ball BallUpgraded;
    private Rigidbody2D BallRb;

    private float BallVelocity;
     // Saves Velocity on the previous frame (Before Colission)
    private float[] VelocitySave = {0, 0};

    private float PointsToGive;
    private int PointsToGiveInt;

    [SerializeField] private float PointsMultiplier = 0.1f;
    public static Racer Instance { get; private set; }

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


    public void AddRacer(Ball Ball_ref)
    {
        BallUpgraded = Ball_ref;
        //SparklesParticle.transform.localScale = Ball_ref.transform.localScale;
        //this.gameObject.SetActive(true);

        Ball_ref.OnHitEventNoParam += UpgradeRacer;
    }


    public void UpgradeRacer()
    {
        if (VelocitySave[1] > 10)
        {
            PointsToGive = VelocitySave[0] * PointsMultiplier;
            PointsToGiveInt = (int)PointsToGive;
        }
        Score_system.Instance.AddpointUpgrades(PointsToGiveInt, BallUpgraded);
        Debug.Log("Dodano punkty Racer");
    }
}
