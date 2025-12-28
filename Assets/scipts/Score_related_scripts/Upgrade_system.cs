using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public enum upgrade_type
{
    A,
    B,
    C
}

public class Upgrade_system : MonoBehaviour
{
    //public Ball Ball_ref;
    private List<Ball> AllBalls;
    public Score_system Score_system_ref;

    public LayerMask wallsLayers;
    // Definicja s³ownika: Kluczem jest string, wartoœci¹ jest funkcja (Action)
    private Dictionary<upgrade_type, Action<Ball>> upgrades_listed;

    [SerializeField] public CanvasGroup upgrade_canvas_group;
    [SerializeField] private TMP_Text Upgrade_score_info;
    [SerializeField] private TMP_Text upgrade_1_txt;
    [SerializeField] private TMP_Text upgrade_2_txt;
    [SerializeField] private TMP_Text upgrade_3_txt;

    public float upgrade_cap_base = 100;
    public float UpgradeCapModifier = 0.05f;
    public float upgrade_cap_start { get; set; }
    public float upgrade_cap;
    public float UpgradeCapDiff = 100;
    public float UpgradeDiffModifier = 0.02f;

    public event Action on_upgrade_cap_hit;
    

    // UPGRADES VARIABLES ///////////
    /////////////////////////////////
    //       SLIDING      ///////////
    public float sliding_bonus_f = 0;
    public int sliding_bonus = 0;//
    /// /////////////////////////////
    //


    public static Upgrade_system Instance { get; private set; }
    


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

        AllBalls = Game_manager.Instance.allBalls;

        upgrade_cap_start = upgrade_cap_base;
        upgrade_cap = upgrade_cap_base;
        UIManager.Instance.UpdateUpgradeScoreDisplay();
        upgrade_1_txt.text = "A";
        upgrade_2_txt.text = "B";
        upgrade_3_txt.text = "C";
        //upgrade_canvas_group = GetComponent<CanvasGroup>();

        Score_system.Instance.OnScoreChanged += ScoreChangedUpgrades;
        

        upgrades_listed = new Dictionary<upgrade_type, Action<Ball>>
        {
                {upgrade_type.A, (value) => Portalball.Instance.AddPortal(value)},
                {upgrade_type.B, (value) => Sliding.Instance.Add_Sliding(value)}
                //{upgrade_type.B, () => Sliding.Instance.Upgrade_Sliding()}
        };

    }

    void OnDestroy()
    {
        if (Score_system.Instance != null)
            Score_system.Instance.OnScoreChanged -= ScoreChangedUpgrades;
    }


    public void ScoreChangedUpgrades()
    {
        if (Score_system_ref.currentscore >= upgrade_cap)
        {
            on_upgrade_cap_hit.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void ResetBase()
    {
        if (Score_system_ref.stagepassed)
        {
            //upgrade_cap = upgrade_cap_base;
            upgrade_cap_start += upgrade_cap_start * UpgradeCapModifier; // Cap at the begging of stage
            upgrade_cap = upgrade_cap_start; // Displayed Cap durning stage 
            UpgradeCapDiff += UpgradeCapDiff * UpgradeDiffModifier; // Diff between next upgrades in stage
            UIManager.Instance.UpdateUpgradeScoreDisplay();
        }
    }

    

    //void Upgrade_Sliding()
    //{
        
       

        //Ball_ref.AddMechanic(()) =>
    //    {
    //        //Debug.Log("UPGRADE B DZIALA");
    //        if (Ball_ref.Collider.IsTouchingLayers(wallsLayers) && Score_system_ref.ball_out_of_pit)
    //        {
    //            //Debug.Log("naliczamy bonus");
    //            sliding_bonus_f += 2f * Time.deltaTime;
    //            sliding_bonus = (int)sliding_bonus_f;
    //        }
    //        else
    //        {
    //            //Debug.Log("Dodajemy bonus");
    //            Score_system_ref.Addpoint(sliding_bonus);
    //            //Score_system_ref.currentscore += sliding_bonus;
    //            sliding_bonus = 0;
    //            sliding_bonus_f = 0;
    //        }
    //    });
    //}
    void Upgrade_A()
    {
        AllBalls[0].ModifyBall(Mass: 50f);
        
    }

    public void UpgradeButton1()
    {
        upgrades_listed[upgrade_type.A](AllBalls[0]);
        UIManager.Instance.HideCanvasGroup(upgrade_canvas_group);
        Time.timeScale = 1f;
        Debug.Log("noklikam1");
    }
    public void UpgradeButton2()
    {
        //Action functionToAdd upgrades_listed[upgrade_type.B];
        upgrades_listed[upgrade_type.B](AllBalls[0]);
        //Ball_ref.ball_mechanics += functionToAdd;
        UIManager.Instance.HideCanvasGroup(upgrade_canvas_group);
        Time.timeScale = 1f;
        Debug.Log("noklikam2");
    }

    public void UpgradeButton3()
    {
        //upgrades_listed[upgrade_type.A]();
        UIManager.Instance.HideCanvasGroup(upgrade_canvas_group);
        Time.timeScale = 1f;
        Debug.Log("noklikam3");
    }


  

}
