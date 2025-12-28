using UnityEngine;

public class Triger_pass_score : MonoBehaviour
{
    public int points_to_give = 30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pinball"))
        {
            Score_system.Instance.Addpoint(points_to_give);

        }
    }
}
