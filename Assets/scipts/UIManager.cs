using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text GoalText;
    [SerializeField] private TMP_Text time_text;
    [SerializeField] private TMP_Text StageText;
    [SerializeField] private TMP_Text Upgrade_score_info;
    
    [SerializeField] private CanvasGroup gameOverCanvasGroup;
    [SerializeField] public CanvasGroup skipToNextRoundGroup;
    [SerializeField] private CanvasGroup upgrade_canvas_group;

    public GameObject AddedPointsUpgradeText;

    private int minutes_text;
    private int seconds_text;

    public static UIManager Instance { get; private set; }

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
        
    }

    public void ChangeGoalTextColor(Color col)
    {
        
         GoalText.color = col;
        
        
    }

    public void ShowCanvasGroup(CanvasGroup canv_group)
    {
        canv_group.alpha = 1; // 1 = Visible, 0 = Invisible
        canv_group.interactable = true; // Can click buttons
        canv_group.blocksRaycasts = true; // Blocks clicks going through to the game
    }

    public void HideCanvasGroup(CanvasGroup canv_group)
    {
        canv_group.alpha = 0; // 1 = Visible, 0 = Invisible
        canv_group.interactable = false; // Can click buttons
        canv_group.blocksRaycasts = false; // Blocks clicks going through to the game
    }

    public void HideCanvasSkip()
    {
        skipToNextRoundGroup.alpha = 0; // 1 = Visible, 0 = Invisible
        skipToNextRoundGroup.interactable = false; // Can click buttons
        skipToNextRoundGroup.blocksRaycasts = false; // Blocks clicks going through to the game
    }

    public void HideCanvasGameOver()
    {
        gameOverCanvasGroup.alpha = 0; // 1 = Visible, 0 = Invisible
        gameOverCanvasGroup.interactable = false; // Can click buttons
        gameOverCanvasGroup.blocksRaycasts = false; // Blocks clicks going through to the game
    }

    public void ShowCanvasGameOver()
    {
        gameOverCanvasGroup.alpha = 1; // 1 = Visible, 0 = Invisible
        gameOverCanvasGroup.interactable = true; // Can click buttons
        gameOverCanvasGroup.blocksRaycasts = true; // Blocks clicks going through to the game
    }

    public void HideCanvasSkipInfo()
    {
        skipToNextRoundGroup.alpha = 0; // 1 = Visible, 0 = Invisible
        skipToNextRoundGroup.interactable = false; // Can click buttons
        skipToNextRoundGroup.blocksRaycasts = false; // Blocks clicks going through to the game
    }

    public void ShowCanvasSkipInfo()
    {
        skipToNextRoundGroup.alpha = 1; // 1 = Visible, 0 = Invisible
        skipToNextRoundGroup.interactable = true; // Can click buttons
        skipToNextRoundGroup.blocksRaycasts = true; // Blocks clicks going through to the game
    }

    public void ShowCanvasUpgrade()
    {
        upgrade_canvas_group.alpha = 1; // 1 = Visible, 0 = Invisible
        upgrade_canvas_group.interactable = true; // Can click buttons
        upgrade_canvas_group.blocksRaycasts = true; // Blocks clicks going through to the game
    }

    public void HideCanvasUpgrade()
    {
        upgrade_canvas_group.alpha = 0; // 1 = Visible, 0 = Invisible
        upgrade_canvas_group.interactable = false; // Can click buttons
        upgrade_canvas_group.blocksRaycasts = false; // Blocks clicks going through to the game
    }


    public void ScoreUpdateDisplay()
    {
        //scoreText.text = "Score: " + Score_system.Instance.currentscore.ToString();
        scoreText.SetText("Score: {0}", Score_system.Instance.currentscore);
    }


    public void GoalUpdateDisplay()
    {
        //GoalText.text = "Goal: " + goalscore.ToString();
        GoalText.SetText("Goal: {0}", Score_system.Instance.goalscore);
    }

    public void TimerUpdateDisplay()
    {
        int timeRemaining_int = (int)Score_system.Instance.timeRemaining;
        minutes_text = timeRemaining_int / 60;
        seconds_text = timeRemaining_int % 60;
        //time_text.text = minutes_text.ToString() + ":" + seconds_text.ToString();
        time_text.SetText("{0}:{1:00}", minutes_text, seconds_text);
    }

    public void StageUpdateDisplay()
    {
        //StageText.text = "Stage: " + stage.ToString();
        StageText.SetText("Stage: {0}", Score_system.Instance.stage);
    }

    
    public void ShowAddedPointsUpgrades(int addedPoints, Ball ball)
    {
        Vector2 BallCords = ball.transform.position;
        BallCords.y += 2f;
        BallCords.x += 1.5f;

        GameObject newText = Instantiate(AddedPointsUpgradeText, BallCords, Quaternion.identity);

        var tmp = newText.GetComponentInChildren<TMPro.TextMeshProUGUI>();
        Debug.Log("Kurwa co jest");


        if (tmp != null)
        {
            Debug.Log("Kurwa co jest xddd");
            tmp.transform.position = BallCords;
            tmp.SetText($"+{addedPoints}");
        }


        Destroy(newText, 1f);
        
    }
}

