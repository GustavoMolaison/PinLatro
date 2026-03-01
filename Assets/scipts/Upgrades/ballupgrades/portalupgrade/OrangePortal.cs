using UnityEngine;

public class OrangePortal : MonoBehaviour
{
    public GameObject BluePortal;
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
            portalball.teleportingOrange = false;
        }

    }

    private void Teleport(GameObject go)
    {
        if (BluePortal.activeSelf == true && portalball.teleportingOrange == false)
        {
            float speed = portalball.ballRef.rb.linearVelocity.magnitude;
            go.transform.position = BluePortal.transform.position;
            portalball.teleportingOrange = true;
            portalball.teleportingBlue = true;

            portalball.ballRef.rb.linearVelocity = BluePortal.transform.up * speed;
        }
        else
        {
            portalball.teleportingOrange = true;
        }


    }
}



