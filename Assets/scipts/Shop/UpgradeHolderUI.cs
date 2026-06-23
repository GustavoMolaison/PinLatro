using System.Collections.Generic;
using UnityEngine;

public class UpgradeHolderUI : MonoBehaviour


{
    public GameObject[] HolderCords;
    public GameObject BallStatueRef;
    
    public Ball EnrolledBall;
    public Sprite DeflaultSprite;
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

        


    }
    

    public void UpdateUpgradesSprites()
    {
        for(int i = 0; i < EnrolledBall.Upgrades.Count; i++)
        {
            Debug.Log($"Updating upgrade sprite for slot {i} with upgrade {EnrolledBall.Upgrades[i].name}");
            SpriteRenderer spriteRenderer = HolderCords[i].GetComponent<SpriteRenderer>();
            // spriteRenderer.material = EnrolledBall.Upgrades[i].upgradeMaterial;
            //SpriteRenderer sr_ball = EnrolledBall.GetComponent<SpriteRenderer>();
            Sprite sr_ball = EnrolledBall.Upgrades[i].upgradeSprite;
            spriteRenderer.sprite = sr_ball;
            //spriteRenderer.sprite = EnrolledBall.Upgrades[i].upgradeSprite;
            // Debug.Log(i);
            // GameObject newSprite = Instantiate(EnrolledBall.Upgrades[i].VisualUIprefab, HolderCords[i].transform.position, Quaternion.identity);
            
        }
    }

    public void ResetHolder()
    {
        for (int i = 0; i < HolderCords.Length; i++)
        {
            SpriteRenderer spriteRenderer = HolderCords[i].GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = DeflaultSprite;
        }

        EnrolledBall = null;
        BallStatueRef.GetComponent<BallStatue>().ResetStatue();
        
    }

    public void SetBallName(string name)
    {
        BallName.text = name;
    }
}
