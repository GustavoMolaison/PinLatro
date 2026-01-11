using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using UnityEngine;

public class MainShop : MonoBehaviour
{
    
    [SerializeField] private TMP_Text uButton1;
    [SerializeField] private TMP_Text uButton2;
    [SerializeField] private TMP_Text uButton3;
    [SerializeField] private TMP_Text uButton4;
    [SerializeField] private TMP_Text uButton5;
    [SerializeField] private TMP_Text uButton6;

    private UpgradeDefinition upgrade1;
    private UpgradeDefinition upgrade2;
    private UpgradeDefinition upgrade3;
    private UpgradeDefinition upgrade4;
    private UpgradeDefinition upgrade5;
    private UpgradeDefinition upgrade6;

    

    private List<UpgradeDefinition> upgradeList;
    private List<TMP_Text> txtList;

    public GameObject ballPrefab;

    public GameObject newBallSpawn1;
    public GameObject newBallSpawn2;
    public GameObject newBallSpawn3;

    private List<GameObject> spawnList;

    [SerializeField] private TMP_Text leaveShopButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public static MainShop Instance { get; private set; }

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
    void Start()
    {
        upgradeList = new List<UpgradeDefinition> { upgrade1, upgrade2, upgrade3, upgrade4, upgrade5, upgrade6 };
        txtList = new List<TMP_Text> { uButton1, uButton2, uButton3, uButton4, uButton5, uButton6 };
        spawnList = new List<GameObject> { newBallSpawn1, newBallSpawn2, newBallSpawn3 };
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void DrawUpgrades()
    {
        for (int i = 0; i < upgradeList.Count; i++)
        {
            //Debug.Log("INDEX");
            //Debug.Log(i);
            upgradeList[i] = Upgrade_system.Instance.GetRandomUpgrade();
            txtList[i].text = upgradeList[i].Name;
        }
    }
    public void UpgradeButton(int buttonIndex)
    {

        upgradeList[buttonIndex - 1].Effect(Game_manager.Instance.allBalls[0]);

    }

    public void NewBallSlotButton()
    {
        int currentBallNum = Game_manager.Instance.allBalls.Count;
        GameObject newBall = Instantiate(ballPrefab, spawnList[currentBallNum - 1].transform.position, Quaternion.identity);
        Ball ballScript = newBall.GetComponent<Ball>();
        Game_manager.Instance.allBalls.Add(ballScript);
    }

    public void OpenShop()
    {
        DrawUpgrades();
        CameraMover.Instance.GoToShop();
    }
}
