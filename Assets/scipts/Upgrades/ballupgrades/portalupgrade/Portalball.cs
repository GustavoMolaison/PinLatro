using System;
using Unity.VisualScripting;
using UnityEngine;



public class Portalball: Upgrade
{

    

    int touchCount = 0;
    public int touchesPerPortal = 4;
    [HideInInspector]
    public Ball ballRef;
    public bool teleportingBlue { get; set; } = false;
    public bool teleportingOrange { get; set; } = false;
    public bool TeleportsPlaced { get; set; } = false;

    public GameObject BluePortal;
    public GameObject OrangePortal;
    public static Portalball Instance { get; private set; }

 

    private void PortalBallOnRoundEnd()
    {
        BluePortal.SetActive(false);
        OrangePortal.SetActive(false);
        TeleportsPlaced = false;
        
    }

    public override void apply(Ball ballReff)
    {
        
        Debug.Log("DodajeszPORTAL??!");
        ballRef = ballReff;
        ballReff.OnHitEvent += OnEvent;

        Game_manager.Instance.upgradesRoundEnd += PortalBallOnRoundEnd;

    }


    public void OnEvent(Collision2D Coll, Ball ballRef)
    {

        if (TeleportsPlaced == false && ballRef.ball_out_of_pit)
        {



            if (!teleportingBlue && !teleportingOrange)
            {
                touchCount += 1;
            }
            if (touchCount == 1)
            {
                if (BluePortal.activeSelf == false)
                {
                    BluePortal.SetActive(true);
                }

                ContactPoint2D contact = Coll.GetContact(0);
                BluePortal.transform.position = contact.point;
                float angle = Mathf.Atan2(contact.normal.y, contact.normal.x) * Mathf.Rad2Deg;
                BluePortal.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            }
            //Debug.Log(touchCount);

            if (touchCount == touchesPerPortal)
            {

                if (OrangePortal.activeSelf == false)
                {
                    OrangePortal.SetActive(true);
                }

                ContactPoint2D contact = Coll.GetContact(0);
                OrangePortal.transform.position = contact.point;
                float angle = Mathf.Atan2(contact.normal.y, contact.normal.x) * Mathf.Rad2Deg;
                OrangePortal.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

                touchCount = 0;
                TeleportsPlaced = true;
            }
        }
    }
    
}
