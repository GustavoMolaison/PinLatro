using UnityEngine;

public class saver_right : MonoBehaviour
{
    public int power = 400;
    public int backpower = -200;
    private HingeJoint2D hg;
    private JointMotor2D motorRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hg = GetComponent<HingeJoint2D>();
        

    }

    // Update is called once per frame
    void Update()
    {
        motorRef = hg.motor;

        if (Input.GetKey(KeyCode.X) & Game_manager.Instance.inTable == true)
        {
           
            motorRef.motorSpeed = power;

            //rb.AddForce(Vector2.down * power);
        }
        else
        {
            motorRef.motorSpeed = backpower;
        }

        hg.motor = motorRef;


    }
}
