using UnityEngine;

public class saver_left : MonoBehaviour
{
    public int power = -1 * 400;
    public int backpower = 200;
    private HingeJoint2D hg;
    private Rigidbody2D rb;
    private JointMotor2D motor2D;
    private JointMotor2D motorRef;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      hg = GetComponent<HingeJoint2D>();
      rb = GetComponent<Rigidbody2D>();
     
    }

    // Update is called once per frame
    void Update()
    {
        motorRef = hg.motor;

        if (Input.GetKey(KeyCode.Z) & Game_manager.Instance.inTable == true)
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
