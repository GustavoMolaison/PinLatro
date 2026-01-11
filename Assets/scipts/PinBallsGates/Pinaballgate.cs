using UnityEngine;

public class Pinaballgate : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OpenGate()
    {
        bool offOrOn = this.isActiveAndEnabled;
        offOrOn = false;
    }

    void CloseGate()
    {
        bool offOrOn = this.isActiveAndEnabled;
        offOrOn = true;
    }
}
