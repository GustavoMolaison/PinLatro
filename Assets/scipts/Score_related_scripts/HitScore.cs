using UnityEngine;

public class HitScore : MonoBehaviour
{
    public int points_to_give = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.CompareTag("Pinball"))
        {
            Score_system.Instance.Addpoint(points_to_give);

        }
    }
}
