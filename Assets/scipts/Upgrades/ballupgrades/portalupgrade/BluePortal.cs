using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject OrangePortal;
    public Portalball portalball;
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdzamy czy to co wlecia�o to pi�ka (u�yj Tagu albo sprawdzania komponentu)
        if (other.CompareTag("Pinball"))
        {
            Teleport(other.gameObject);
        }



    }

    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.CompareTag("Pinball"))
        {
            portalball.teleportingBlue = false;
        }

    }

    private void Teleport(GameObject go)
    {
        if (OrangePortal.activeSelf == true && portalball.teleportingBlue == false)
        {
            float speed = portalball.ballRef.rb.linearVelocity.magnitude;
            go.transform.position = OrangePortal.transform.position;
            portalball.teleportingBlue = true;
            portalball.teleportingOrange = true;

            portalball.ballRef.rb.linearVelocity = OrangePortal.transform.up * speed;
        }

        else
        {
            if (portalball.teleportingBlue == false)
            {
                portalball.teleportingBlue = true;
            }
        }

    }
}
