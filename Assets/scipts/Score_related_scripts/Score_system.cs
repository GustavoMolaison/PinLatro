using UnityEngine;
using TMPro;
using System;

 
public class Score_system : MonoBehaviour
{

    //public Ball ball_ref;
    //public Game_manager Game_manager_ref;

    public event Action OnTimerEnd;
    public event Action onGoalAchiev;
    public event Action OnScoreChanged;

    public int duration = 10; // Cintzas startowy
    public float timeRemaining;
    
    


    public float currentscore = 0;
    public float alltimescore { get; set; } = 0;
    public float goalscore_custom = 100;
    public float GoalscoreModifier = 2;
    public float goalscore;


    public int stage = 1;

     // booleans changed in other scripts
    public bool goalcleared { get; set; }
    
    
    public bool stagepassed { get; set; } = false;



    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // --- POCZ¥TEK SINGLETONA ---
    // Statyczna zmienna dostêpna zewsz¹d
    public static Score_system Instance { get; private set; }

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
    // --- KONIEC SINGLETONA ---
    void Start()
    {
     goalcleared = false;
     Timer.Instance.isRunning = false;

     timeRemaining = duration;

     currentscore = 0;
     goalscore = goalscore_custom;
       

     UIManager.Instance.ScoreUpdateDisplay();
     UIManager.Instance.GoalUpdateDisplay();
     UIManager.Instance.TimerUpdateDisplay();
     UIManager.Instance.StageUpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void reset()
    {
        if (stagepassed)
        {

            goalcleared = false;
            //stagepassed = false;

            Timer.Instance.isRunning = false;

            foreach (var ball in Game_manager.Instance.allBalls)
            {
                ball.ball_out_of_pit = false;
            }

            currentscore = 0;
            goalscore += goalscore * GoalscoreModifier;
            stage += 1;
           



            timeRemaining += duration;

            UIManager.Instance.ChangeGoalTextColor(Color.white);


        }
        else
        {

          

            goalcleared = false;
            //stagepassed = false;

            Timer.Instance.isRunning = false;

            foreach (var ball in Game_manager.Instance.allBalls)
            {
                ball.ball_out_of_pit = false;
            }

            currentscore = 0;
            goalscore = goalscore_custom;
            timeRemaining = duration;

            stage = 1;
        }

        
        UIManager.Instance.ScoreUpdateDisplay();
        UIManager.Instance.GoalUpdateDisplay();
        UIManager.Instance.TimerUpdateDisplay();
        UIManager.Instance.StageUpdateDisplay();

    }
    public void Addpoint(int amount)
    {
        currentscore += amount;
        alltimescore += amount;
       
        UIManager.Instance.ScoreUpdateDisplay();

        
         OnScoreChanged.Invoke();
        

        if (currentscore >= goalscore && stagepassed == false) 
        {
            UIManager.Instance.ChangeGoalTextColor(Color.green);
            
            goalcleared = true;
            stagepassed = true;

            
        }

    }

   public void AddpointUpgrades(int amount, Ball BallRef)
    {
        
        if (BallRef.ball_out_of_pit)
        {

            
            currentscore += amount;
            alltimescore += amount;
            UIManager.Instance.ScoreUpdateDisplay();


            OnScoreChanged.Invoke();


            if (currentscore >= goalscore && stagepassed == false)
            {
                UIManager.Instance.ChangeGoalTextColor(Color.green);

                goalcleared = true;
                stagepassed = true;


            }
            if( amount > 1f)
            {
                UIManager.Instance.ShowAddedPointsUpgrades(amount, BallRef);
            }
            
        }
    }

    

    

 
   
    

}
