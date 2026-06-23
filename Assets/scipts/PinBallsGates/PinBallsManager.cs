using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using UnityEngine;

public class PinBallsManager : MonoBehaviour
{
    public List<Ball> allBalls;
    

    public Transform spawnBalls;
    public Transform waitingRoom;

    //int openGatesNum;

    public List<Ball> ballsInQueue = new List<Ball>();
    public List<Ball> ballsInLaunchPad = new List<Ball>();

    public List<UpgradeHolderUI> upgradeHolderUIs = new List<UpgradeHolderUI>();

    public List<BallStatue> ballStatues = new List<BallStatue>();

    [HideInInspector] public Ball ballToUpgrade;

    [HideInInspector] public bool oneBallBlooming;

    public GameObject newBallSpawn;

    public GameObject ballPrefab;
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

    public void DestroyBallCleanUp(Ball ball)
    {
        allBalls.Remove(ball);
        ballsInLaunchPad.Remove(ball);
        ballsInQueue.Remove(ball);

        allBalls.RemoveAll(item => item == null);
        ballsInLaunchPad.RemoveAll(item => item == null);
        ballsInQueue.RemoveAll(item => item == null);
        
    }

    public void oneBallPicked(Ball newBloomer)
    {
        foreach(Ball ball in allBalls)
            {
              if (newBloomer != ball && ball.isBlooming == true)
            {
                ball.whenPickedBloom.SetActive(false);
                ball.isBlooming = false;
                ball.ballStatue.whenPickedBloom.SetActive(false);
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

        GameObject newBallParent = Instantiate(ballPrefab, newBallSpawn.transform.position, Quaternion.identity);
        Ball newBall = newBallParent.GetComponent<Ball>();
            
        allBalls.Add(newBall);
        newBall.BallToWaitingRoom();
        
        newBall.upgradeHolderUI = upgradeHolderUIs[allBalls.Count - 1];
        newBall.ballStatue = ballStatues[allBalls.Count - 1];

        upgradeHolderUIs[allBalls.Count - 1].EnrolledBall = newBall;
        ballStatues[allBalls.Count - 1].EnrolledBall = newBall;

        NewSlotHelper.Instance.costTmp.text = $"Cost: {MainShop.Instance.newSlotCost[Instance.allBalls.Count - 1]}";
    }

    public void AddNewBallThroughEvolution(List<UpgradesSO> upgrades)
    {
        
        

        GameObject newBallParent = Instantiate(ballPrefab, newBallSpawn.transform.position, Quaternion.identity);
        Ball newBall = newBallParent.GetComponent<Ball>();
        
        

        allBalls.Add(newBall);
        newBall.BallToWaitingRoom();
        Debug.Log($"allBalls.Count: {allBalls.Count}");
        
        // if (allBalls.Count == 0)
        // {
        //     newBall.upgradeHolderUI = upgradeHolderUIs[0];
        //     newBall.ballStatue = ballStatues[0];

        //     upgradeHolderUIs[0].EnrolledBall = newBall;
        //     ballStatues[0].EnrolledBall = newBall;
        // }
        // else
        // {
            newBall.upgradeHolderUI = upgradeHolderUIs[allBalls.Count - 1];
            newBall.ballStatue = ballStatues[allBalls.Count - 1];

            upgradeHolderUIs[allBalls.Count - 1].EnrolledBall = newBall;
            ballStatues[allBalls.Count - 1].EnrolledBall = newBall;

        // }
        
        Debug.Log($"Znaleziono {upgrades.Count} upgrades dla tych kul hellyea!");
        foreach(UpgradesSO upgrade in upgrades)
        {
            newBall.AddUpgrade(upgrade);
        }

        
    }

    public void AddNewBallWithoutShop()
    {
        
        GameObject newBallParent = Instantiate(ballPrefab, newBallSpawn.transform.position, Quaternion.identity);

        Ball newBall = newBallParent.GetComponent<Ball>();
            
        allBalls.Add(newBall);
        newBall.BallToWaitingRoom();
        
        newBall.upgradeHolderUI = upgradeHolderUIs[allBalls.Count - 1];
        newBall.ballStatue = ballStatues[allBalls.Count - 1];

        upgradeHolderUIs[allBalls.Count - 1].EnrolledBall = newBall;
        ballStatues[allBalls.Count - 1].EnrolledBall = newBall;

       
    }

}
   
  

