using TMPro;
using UnityEngine;
using System;
public class Timer : MonoBehaviour

    

    
{

    public bool isRunning { get; set; } = false;
    public static Timer Instance { get; private set; }

    public event Action OnTimerEnd;

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (isRunning)
        {
            ClockFrameRun();
        }

        else
        {
            foreach (var ball in Game_manager.Instance.allBalls)
            {
                if (ball.ball_out_of_pit)
                {
                    
                    StartTimer();
                    break; 
                }
            }
            
        }

    }

    public void StartTimer()
    {

        isRunning = true;
    }

    public void EndTimer()
    {
        Debug.Log("xd czas xdXDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD");
        OnTimerEnd?.Invoke();

    }

    public void StopTimer()
    {
        isRunning = false;
        foreach (var ball in Game_manager.Instance.allBalls)
        {
            ball.ball_out_of_pit = false;
        }
    }

    public void ClockFrameRun()
    {
        if (isRunning)
        {
            if (Score_system.Instance.timeRemaining > 0)
            {
                // Time.deltaTime to czas, jaki min¹³ od ostatniej klatki.
                // Dziêki temu odliczanie jest w sekundach, a nie w klatkach.
                Score_system.Instance.timeRemaining -= Time.deltaTime;


                // Tutaj mo¿esz aktualizowaæ UI, np.:
                // timerText.text = timeRemaining.ToString("F2");
            }
            else
            {
                Score_system.Instance.timeRemaining = 0;
                isRunning = false;
                EndTimer();

            }
            UIManager.Instance.TimerUpdateDisplay();
        }
    }


  
    }

