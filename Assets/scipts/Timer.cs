using TMPro;
using UnityEngine;
using System;
public class Timer : MonoBehaviour

    

    
{
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

        if (Score_system.Instance.isRunning)
        {
            ClockFrameRun();
        }

        else
        {
            if (Score_system.Instance.ball_out_of_pit)
            {
                StartTimer();
            }
        }

    }

    public void StartTimer()
    {

        Score_system.Instance.isRunning = true;
    }

    public void EndTimer()
    {
        //Debug.Log("xd czas xd");
        OnTimerEnd?.Invoke();

    }

    public void StopTimer()
    {
        Score_system.Instance.isRunning = false;
        Score_system.Instance.ball_out_of_pit = false;
    }

    public void ClockFrameRun()
    {
        if (Score_system.Instance.isRunning)
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
                Score_system.Instance.isRunning = false;
                EndTimer();

            }
            UIManager.Instance.TimerUpdateDisplay();
        }
    }


  
    }

