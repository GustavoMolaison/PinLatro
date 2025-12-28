using System;
using Unity.VisualScripting;
using UnityEngine;


public class Portalball: MonoBehaviour
{
    int TouchCount = 0;
    public int TouchesPerPortal = 4; 
    public Ball Ball_ref;
    public bool TeleportingBlue { get; set; } = false;
    public bool TeleportingOrange { get; set; } = false;
    public bool TeleportsPlaced { get; set; } = false;

    public GameObject BluePortal;
    public GameObject OrangePortal;
    public static Portalball Instance { get; private set; }

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
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Start()
    {
        Game_manager.Instance.UpgradesRoundEnd += PortalBallOnRoundEnd;
        //Object<GetComponent> = 
    }

    private void PortalBallOnRoundEnd()
    {
        BluePortal.SetActive(false);
        OrangePortal.SetActive(false);
        TeleportsPlaced = false;
        
    }

    public void AddPortal(Ball ball_ref)
    {
        ball_ref.OnHitEvent += Portal;
        Ball_ref = ball_ref;
    }


    public void Portal(Collision2D Coll, Ball ball_ref)
    {

        if (TeleportsPlaced == false && Score_system.Instance.ball_out_of_pit)
        {



            if (!TeleportingBlue && !TeleportingOrange)
            {
                TouchCount += 1;
            }
            if (TouchCount == 1)
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
            //Debug.Log(TouchCount);

            if (TouchCount == TouchesPerPortal)
            {

                if (OrangePortal.activeSelf == false)
                {
                    OrangePortal.SetActive(true);
                }

                ContactPoint2D contact = Coll.GetContact(0);
                OrangePortal.transform.position = contact.point;
                float angle = Mathf.Atan2(contact.normal.y, contact.normal.x) * Mathf.Rad2Deg;
                OrangePortal.transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

                TouchCount = 0;
                TeleportsPlaced = true;
            }
        }
    }
    
}
