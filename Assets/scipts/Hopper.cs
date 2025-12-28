using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

public class Hopper : MonoBehaviour
{
    public float power = 3000f;
    public float maxspringpower = 10f;
    public float minspringpower = 2f;

    private Rigidbody2D rb;
    private SpringJoint2D spring;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      spring = GetComponent<SpringJoint2D>();

      spring.frequency = maxspringpower;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            spring.frequency = minspringpower;
            rb.AddForce(Vector2.down * power * Time.deltaTime);
        }
        else
        {
            spring.frequency = maxspringpower;
        }

    }
}
