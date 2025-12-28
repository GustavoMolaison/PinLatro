using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject OrangePortal;
    public Portalball Portalball;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy czy to co wlecia³o to pi³ka (u¿yj Tagu albo sprawdzania komponentu)
        if (other.CompareTag("Pinball"))
        {
            Teleport(other.gameObject);
        }



    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Pinball"))
        {
            Portalball.TeleportingBlue = false;
        }

    }

    private void Teleport(GameObject go)
    {
        if (OrangePortal.activeSelf == true && Portalball.TeleportingBlue == false)
        {
            float speed = Portalball.Ball_ref.Rb.linearVelocity.magnitude;
            go.transform.position = OrangePortal.transform.position;
            Portalball.TeleportingBlue = true;
            Portalball.TeleportingOrange = true;

            Portalball.Ball_ref.Rb.linearVelocity = OrangePortal.transform.up * speed;
        }

        else
        {
            if (Portalball.TeleportingBlue == false)
            {
                Portalball.TeleportingBlue = true;
            }
        }

    }
}
