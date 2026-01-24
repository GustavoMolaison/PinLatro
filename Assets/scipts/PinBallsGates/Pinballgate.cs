using UnityEngine;

public class Pinaballgate : MonoBehaviour
{
    public bool open = false;


    public void OpenGate()
    {

        Debug.Log("OPENING GATE");
        this.gameObject.SetActive(false);
        open = true;
    }

    void CloseGate()
    {
        this.gameObject.SetActive(true);
        open = false;
    }
}
