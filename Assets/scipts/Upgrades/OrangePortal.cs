using UnityEngine;

public class OrangePortal : MonoBehaviour
{
    public GameObject BluePortal;
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
            Portalball.TeleportingOrange = false;
        }

    }

    private void Teleport(GameObject go)
    {
        if (BluePortal.activeSelf == true && Portalball.TeleportingOrange == false)
        {
            float speed = Portalball.Ball_ref.Rb.linearVelocity.magnitude;
            go.transform.position = BluePortal.transform.position;
            Portalball.TeleportingOrange = true;
            Portalball.TeleportingBlue = true;

            Portalball.Ball_ref.Rb.linearVelocity = BluePortal.transform.up * speed;
        }
        else
        {
            Portalball.TeleportingOrange = true;
        }


    }
}



