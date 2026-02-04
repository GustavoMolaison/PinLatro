using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using System.Linq;
using System.Runtime.CompilerServices;


// 1. Definicja typów ulepszeñ
public enum UpgradeType
{
    Portal,
    Sliding,
    Racer
}

//[Serializable]
//public struct UpgradeMapping
//{
//    [SerializeField]  public GameObject prefab;
//    [SerializeField]  public UpgradeType type;
//    [SerializeField]  public int weight;
//    [SerializeField]  public int cost;

//}




[Serializable]
public class UpgradeDefinition 
{
    //[HideInInspector] public string Name;
    
    public GameObject Prefab;
    public UpgradeType Type;
    public UpgradesSO upgradesSO;
    public int Weight;              // Szansa na wylosowanie (wy¿sza liczba = czêœciej)
    public int Cost;
    public Action<Ball> Effect;

    public string Name => Type.ToString();






    public UpgradeDefinition(UpgradeType type, int weight, int cost)
    {
        Type = type;
        Weight = weight;
        //Effect = (ballRef) => InitializeUpgrade(type, ballRef);
        Cost = cost;
        
    }

    
}
  

public class Upgrade_system : MonoBehaviour
{
    
    public Dictionary<UpgradeType, GameObject> upgradesDict = new Dictionary<UpgradeType, GameObject>();


   
    
    public static Upgrade_system Instance { get; private set; }


  
    public List<UpgradeDefinition> upgrades;

    private Upgrade upgradeScript;
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

         
        foreach (var upgrade in upgrades)
        {

            upgradesDict.Add(upgrade.Type, upgrade.Prefab);


            UpgradeType currentType = upgrade.Type;
            upgrade.Effect = (ballRef) => InitializeUpgrade(currentType, ballRef);
          
        }

        CalculateWeights();
     
    }



    public void InitializeUpgrade(UpgradeType upgradeName, Ball ballRef)
    {
        if (!upgradesDict.ContainsKey(upgradeName))
        {
            Debug.LogError($"Brak prefaba dla {upgradeName} w s³owniku!");
            return;
        }
        if (!ballRef.Upgrades.Contains(upgradeName))
        {
            ballRef.Upgrades.Add(upgradeName);
            GameObject newUpgrade = Instantiate(upgradesDict[upgradeName], ballRef.transform);
            upgradeScript = newUpgrade.GetComponent<Upgrade>();
            upgradeScript.apply(ballRef);
        }

        
    }


    private int _totalWeight;

   

   

    private void CalculateWeights()
    {
        _totalWeight = 0;
        foreach (var upgrade in upgrades)
        {
            _totalWeight += upgrade.Weight;
        }
    }

    /// <summary>
    /// Zwraca losowe ulepszenie z uwzglêdnieniem wag.
    /// To jest standard bran¿owy dla systemów lootu/ulepszeñ.
    /// </summary>
    public UpgradeDefinition GetRandomUpgrade()
    {
       
        if (upgrades == null || upgrades.Count == 0)
        {
            Debug.LogError("Brak zdefiniowanych ulepszeñ!");
            return null;
        }

        // Algorytm losowania wa¿onego (Weighted Random Choice)
        int randomValue = UnityEngine.Random.Range(0, _totalWeight);
        int currentSum = 0;


        foreach (var upgrade in upgrades)
        {
            currentSum += upgrade.Weight;
            if (randomValue < currentSum)
            {
                return upgrade;
            }
        }

        // Fallback (nie powinien wyst¹piæ, jeœli matematyka jest poprawna
        return null;
    }

    // Metoda pomocnicza, jeœli potrzebujesz "zwyk³ego" losowania bez wag
    public UpgradeDefinition GetUniformRandomUpgrade()
    {
        if (upgrades.Count == 0) return null;
        return upgrades[UnityEngine.Random.Range(0, upgrades.Count)];
    }




}
