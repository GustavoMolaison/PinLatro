using System.Collections.Generic;
using UnityEngine;

public class Gambler : Upgrade
{
    GameObject collisionObject;
    int[] randomNumbers = new int[3];
    int counter = 0;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }
    public override void apply(Ball ballRef)
    {
        //SparklesParticle.transform.localScale = ballRef.transform.localScale;
        this.gameObject.SetActive(true);
        ballRef.activateRings = false;
        ballRef.OnHitEvent += letsGamble;
    }

    public void letsGamble(Collision2D collision, Ball ballRef)
    {
        collisionObject = collision.gameObject;
        if(collisionObject.tag == "PointRing")
        {
            int randomNum = Random.Range(0, 11);
            Score_system.Instance.AddpointUpgrades(randomNum, ballRef);
            randomNumbers[counter] = randomNum;
            counter++;
            if(counter == 3)
            {
                counter = 0;
                if(randomNumbers[0] == 7 && randomNumbers[1] == 7 && randomNumbers[2] == 7)
                {
                    Score_system.Instance.AddpointUpgrades(777, ballRef);
                }
                randomNumbers = new int[3];
            }
            
        }
        
        
    }



}
