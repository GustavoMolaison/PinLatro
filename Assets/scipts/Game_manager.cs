using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Game_manager : MonoBehaviour
{


    //public Ball ball_ref;

    public List<Ball> allBalls;
    public static Game_manager Instance { get; private set; }
    public event Action UpgradesRoundEnd;


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
        UpgradesRoundEnd += Upgrade_system.Instance.ResetBase;


        Score_system.Instance.onGoalAchiev += GoalCleared;
       

        Upgrade_system.Instance.on_upgrade_cap_hit += Up_cap_achiev_manager;

        Timer.Instance.OnTimerEnd += ResetSystem;
    }

    void OnDestroy()
    {

        //Score_system_ref.OnTimerEnd -= ResetSystem;
        Score_system.Instance.onGoalAchiev -= GoalCleared;

        Upgrade_system.Instance.on_upgrade_cap_hit -= Up_cap_achiev_manager;


            Timer.Instance.OnTimerEnd -= ResetSystem;
    }


    private void Update()
    {
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
        allBalls[0].BallToSpawn();
        Debug.Log("TYLKO RAZ");

        if (Score_system.Instance.stagepassed == false)
        {
            Debug.Log("stagepassed false");
            GameOver();
            Score_system.Instance.reset();
            UpgradesRoundEnd.Invoke();
            Score_system.Instance.stagepassed = false;
        }

        if (Score_system.Instance.stagepassed == true)
        {
            Debug.Log("stagepassed true");
            Score_system.Instance.reset();
            UpgradesRoundEnd.Invoke();
            Score_system.Instance.stagepassed = false;
        }


        UIManager.Instance.HideCanvasSkipInfo();
    }

    void GameOver()
    {
        //Debug.Log("No gameover nie");
        //Debug.Log(Score_system_ref.goalcleared);
        if (Score_system.Instance.goalcleared == false)
        {
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


    public void Up_cap_achiev_manager()
    {

        UIManager.Instance.ShowCanvasUpgrade();
        Upgrade_system.Instance.upgrade_cap += Upgrade_system.Instance.UpgradeCapDiff;
        UIManager.Instance.UpdateUpgradeScoreDisplay();
        Debug.Log("Pauza?");
        Time.timeScale = 0f;
    }

    public void OnGoalCleared()
    {
        UIManager.Instance.ShowCanvasSkipInfo();
    }


             
}
