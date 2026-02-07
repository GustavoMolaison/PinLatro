using System.Collections.Generic;
using UnityEngine;

public class UpgradeHolderUI : MonoBehaviour


{
    public GameObject[] HolderCords;
    public GameObject BallStuatue;
    
    public Ball EnrolledBall;
    public List<Sprite> UpgradeSprites;
    public TMPro.TMP_Text BallName;
    
    public static UpgradeHolderUI Instance { get; private set; }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        // Je�li instancja ju� istnieje (np. duplikat), niszczymy ten obiekt
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        HolderCords = GameObject.FindGameObjectsWithTag("UpgradeHoldPlace");


    }

    public void UpdateUpgradesSprites(List<UpgradesSO> Upgrades)
    {
        for(int i = 0; i < HolderCords.Length; i++)
        {
            // Spirte logic here
        }
    }

    public void SetBallName(string name)
    {
        BallName.text = name;
    }
}
