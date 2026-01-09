using UnityEngine;

public class LaunchPad : MonoBehaviour
{
    //public float forceAdded = 20f;
    [SerializeField] private float launchForce = 500f;
    [SerializeField] private float MulipierPower = 1.3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        ContactPoint2D contact = coll.GetContact(0);
        Vector2 normal = contact.normal;
        Rigidbody2D rb = coll.collider.GetComponent<Rigidbody2D>();

        Vector2 reflection = Vector2.Reflect(coll.relativeVelocity, normal);

        rb.linearVelocity = (reflection + (normal * transform.up * launchForce)) * MulipierPower;
        
        //Vector2 reflectionVector = coll.relativeVelocity +  new Vector2(forceAdded, forceAdded);
        //Rigidbody2D ballRb = coll.collider.GetComponent<Rigidbody2D>();
        //ballRb.linearVelocity = reflectionVector;

        // 1. Sprawdzasz czy to co uderzy³o ma Rigidbody2D (czy to pi³ka)


        //Rigidbody2D rb = coll.collider.GetComponent<Rigidbody2D>();

        //if (rb != null)
        //{
        //    // B£¥D LOGICZNY, KTÓREGO UNIKAMY: 
        //    // Nie u¿ywamy prêdkoœci pi³ki, bo chcemy j¹ "wystrzeliæ", a nie "odbiæ".

        //    // 2. Zerujemy obecn¹ prêdkoœæ, ¿eby si³a wystrza³u by³a zawsze taka sama

        //    //rb.linearVelocity = Vector2.zero;

        //    // 3. Nadajemy now¹ prêdkoœæ w kierunku "góry" pada
        //    // ForceMode2D.Impulse jest idealny do nag³ych wystrza³ów

        //    rb.AddForce(transform.up * launchForce, ForceMode2D.Impulse);
        //    Debug.Log("Wystrzelono obiekt: " + coll.gameObject.name);

    }
    }

