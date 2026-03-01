using UnityEngine;

public class RespawnLine : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pinball"))
        {
            other.GetComponent<Ball>().BallToWaitingRoom();
        }
    }
}
