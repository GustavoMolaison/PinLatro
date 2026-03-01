using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Game_manager : MonoBehaviour
{


    

    

    public event Action upgradesRoundEnd;
    public static Game_manager Instance { get; private set; }

    public bool inShop = false;
    public bool inTable = true;




    void Awake()
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
    private void Start()
    {
        

        if( Score_system.Instance.stage == 1)
        {
            PinBallsManager.Instance.AddNewBallWithoutShop();
        }
       

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
        // Debug.Log($"allBalls.Count: {PinBallsManager.Instance.allBalls.Count}");
        if (Input.GetKeyDown(KeyCode.R) && Score_system.Instance.stagepassed == true)
        {
           
            
                
            UIManager.Instance.HideCanvasSkip();
            ResetSystem();


        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }

        

        if (Score_system.Instance.goalcleared)
        {
            OnGoalCleared();
        }

        
    }
    public void ResetSystem()
    {
       

        PinBallsManager.Instance.allBalsToWaitingRoom();


        if (Score_system.Instance.stagepassed == false)
        {



            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);

        }

        if (Score_system.Instance.stagepassed == true)
        {

            MainShop.Instance.OpenShop();
            Score_system.Instance.reset();
            upgradesRoundEnd?.Invoke();
            Score_system.Instance.stagepassed = false;
        }


        UIManager.Instance.HideCanvasSkipInfo();
    }

    

    void GameOver()
    {
        
        if (Score_system.Instance.stagepassed == false)
        {
            PinBallsManager.Instance.resetUpgradesAllBalls();
            UIManager.Instance.ShowCanvasGameOver();
            Score_system.Instance.reset();
            MoneySystem.Instance.resetMoney();
            upgradesRoundEnd.Invoke();
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
