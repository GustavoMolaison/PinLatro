using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
public class Game_manager : MonoBehaviour
{


    //public Ball ballRef;

    public List<Ball> allBalls;

    public event Action upgradesRoundEnd;
    public static Game_manager Instance { get; private set; }

    public bool inShop = false;
    public bool inTable = true;




    void Awake()
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
    private void Start()
    {
        
       

        // One time events when player hits goal
        Score_system.Instance.onGoalAchiev += GoalCleared;


        
// Events when timer hits 0:00
        Timer.Instance.onTimerEnd += ResetSystem;
        
    }

    void OnDestroy()
    {

        

        Score_system.Instance.onGoalAchiev -= GoalCleared;

        

        Timer.Instance.onTimerEnd -= ResetSystem;
    }


    private void Update()
    {
        //Debug.Log(allBalls[0].ball_out_of_pit);
        if (Input.GetKeyDown(KeyCode.R) && Score_system.Instance.stagepassed == true)
        {
            //Debug.Log("RRR");
            allBalls[0].BallToSpawn();
            UIManager.Instance.HideCanvasSkip();
            ResetSystem();


        }

        if (Score_system.Instance.goalcleared)
        {
            OnGoalCleared();
        }

        
    }
    public void ResetSystem()
    {
        Debug.Log("Reset");
        allBalls[0].BallToSpawn();

        
        if (Score_system.Instance.stagepassed == false)
        {
            
            
            Debug.Log("stagepassed false");
            GameOver();
            Score_system.Instance.reset();
            upgradesRoundEnd.Invoke();
            Score_system.Instance.stagepassed = false;
        }

        if (Score_system.Instance.stagepassed == true)
        {
            Debug.Log("stagepassed true");
            MainShop.Instance.OpenShop();
            Score_system.Instance.reset();
            upgradesRoundEnd.Invoke();
            Score_system.Instance.stagepassed = false;
        }


        UIManager.Instance.HideCanvasSkipInfo();
    }

    

    void GameOver()
    {
        
        if (Score_system.Instance.stagepassed == false)
        {
            allBalls[0].ResetUpgrades();
            UIManager.Instance.ShowCanvasGameOver();
        }
        Timer.Instance.StopTimer();


    }

    

    void GoalCleared()
    {
        if (Score_system.Instance.goalcleared == true)
        {
            UIManager.Instance.ShowCanvasSkipInfo();
            Score_system.Instance.goalcleared = false;
        }
    }
   

    public void RestartButton()
    {



        UIManager.Instance.HideCanvasGameOver();

        Score_system.Instance.reset();

    }


    

    public void OnGoalCleared()
    {
        UIManager.Instance.ShowCanvasSkipInfo();
    }


    
}
