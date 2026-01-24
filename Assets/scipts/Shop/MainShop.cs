using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Xml;
using NUnit.Framework;
using TMPro;
using UnityEditor;
using UnityEngine;

public class MainShop : MonoBehaviour
{



    public int[] newSlotCost = new int[3];

    

   
    private UpgradeDefinition[] upgradeArray;

    [SerializeField] private List<ButtonHelper> upgradeButtonsList;

    public NewSlotHelper NewSlotHelper;

    public GameObject ballPrefab;
    

    public GameObject newBallSpawn;
  
    [SerializeField] private TMP_Text leaveShopButton;

    public static MainShop Instance { get; private set; }

    private void Awake()
    {
        // Jeœli instancja ju¿ istnieje (np. duplikat), niszczymy ten obiekt
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        upgradeArray = new UpgradeDefinition[upgradeButtonsList.Count];
        NewSlotHelper.costTmp.text = $"Cost: {newSlotCost[0]}";
    }


    private void DrawUpgrades()
    {
        
        for (int i = 0; i < upgradeArray.Length; i++)
        {
            
            upgradeArray[i] = Upgrade_system.Instance.GetRandomUpgrade();
            upgradeButtonsList[i].nameTmp.text = upgradeArray[i].Name;
            upgradeButtonsList[i].costTmp.text = $"Cost: {upgradeArray[i].Cost}";
        }
    }
    public void UpgradeButton(int buttonIndex)
    {
        
        if (MoneySystem.Instance.currentMoney >= upgradeArray[buttonIndex - 1].Cost)
        {
            upgradeArray[buttonIndex - 1].Effect(PinBallsManager.Instance.ballToUpgrade);
        }
        else
        {
            upgradeButtonsList[buttonIndex - 1].tooPoor();
        }


    }

    public void NewBallSlotButton()
        
    {
        Debug.Log(newSlotCost[PinBallsManager.Instance.allBalls.Count - 1]);
        if (PinBallsManager.Instance.allBalls.Count < 3 && MoneySystem.Instance.currentMoney >= newSlotCost[PinBallsManager.Instance.allBalls.Count - 1])
        {


            
            GameObject newBallParent = Instantiate(ballPrefab, newBallSpawn.transform.position, Quaternion.identity);
            Ball newBall = newBallParent.GetComponent<Ball>();
            newBall.BallToWaitingRoom();
            PinBallsManager.Instance.allBalls.Add(newBall);

            NewSlotHelper.costTmp.text = $"Cost: {newSlotCost[PinBallsManager.Instance.allBalls.Count - 1]}";
        }
        else
        {
            NewSlotHelper.tooPoor();
        }
    }

    public void OpenShop()
    {
        DrawUpgrades();
        CameraMover.Instance.GoToShop();
    }
}
