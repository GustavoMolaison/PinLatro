using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PinBallsManager : MonoBehaviour
{
    public List<Ball> allBalls;
    public List<Pinaballgate> pinaBallGates;

    public Transform spawnBalls;
    public Transform waitingRoom;

    //int openGatesNum;

    public List<Ball> ballsInQueue = new List<Ball>();
    public List<Ball> ballsInLaunchPad = new List<Ball>();

    public List<UpgradeHolderUI> upgradeHolderUIs = new List<UpgradeHolderUI>();

    public Ball ballToUpgrade;

    public bool oneBallBlooming;

    public GameObject newBallSpawn;
    public static PinBallsManager Instance { get; private set; }

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Update()
    {
        //Debug.Log($"ballsInLaunchPad{ballsInLaunchPad.Count}");
        //Debug.Log($"ballsInQueue{ballsInQueue.Count}");
        if (ballsInLaunchPad.Count == 0 && ballsInQueue.Count > 0 && Game_manager.Instance.inShop == false)
        {           
            ballsInQueue[0].BallToSpawn();
        }

        if (Game_manager.Instance.inShop)
        {
            foreach (Ball ball in allBalls)
            {
                if (ball.isBlooming)
                {
                    oneBallBlooming = true;
                    ballToUpgrade = ball;
                    break;
                }
                else
                {
                    oneBallBlooming = false;
                }
            }

            

        }
        else
            {
            ballToUpgrade = null;
            }


    }

    public void oneBallPicked(Ball newBloomer)
    {
        foreach(Ball ball in allBalls)
            {
              if (newBloomer != ball && ball.isBlooming == true)
            {
                ball.whenPickedBloom.SetActive(false);
                ball.isBlooming = false;
            }
              
            }
    }
    public void allBalsToWaitingRoom()
    {

        foreach (var ballRef in allBalls)
        {
            if (!ballRef.inWaitingRoom)
            {
                ballRef.BallToWaitingRoom();
            }
          
        }
    }

    public void resetUpgradesAllBalls()
    {
        foreach (var ballRef in allBalls)
        {
            ballRef.ResetUpgrades();
        }
    }


    public void AddNewBallThroughShop()
    {
        MoneySystem.Instance.takeMoney(MainShop.Instance.newSlotCost[allBalls.Count - 1]);

        GameObject newBallParent = Instantiate(MainShop.Instance.ballPrefab, newBallSpawn.transform.position, Quaternion.identity);
        Ball newBall = newBallParent.GetComponent<Ball>();
            
        allBalls.Add(newBall);
        newBall.BallToWaitingRoom();
        newBall.upgradeHolderUI = upgradeHolderUIs[allBalls.Count - 1];

        NewSlotHelper.Instance.costTmp.text = $"Cost: {MainShop.Instance.newSlotCost[Instance.allBalls.Count - 1]}";
    }

}
   
  

