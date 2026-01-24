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

    public Ball ballToUpgrade;
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
                    ballToUpgrade = ball;
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
            ballRef.BallToWaitingRoom();
        }
    }

    public void resetUpgradesAllBalls()
    {
        foreach (var ballRef in allBalls)
        {
            ballRef.ResetUpgrades();
        }
    }

}
   
  

