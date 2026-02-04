using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;


// 1. Definicja typ�w ulepsze�
public enum UpgradeType
{
    Portal,
    Sliding,
    Racer,

    WallRacer
}





    

  

public class Upgrade_system : MonoBehaviour
{
    
    public Dictionary<UpgradeType, GameObject> upgradesDict = new Dictionary<UpgradeType, GameObject>();

    public List<UpgradesSO> upgrades;


    public static Upgrade_system Instance { get; private set; }

    private Upgrade upgradeScript;
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


        CalculateWeights();
     
    }



   


    private int _totalWeight;

   

   

    private void CalculateWeights()
    {
        _totalWeight = 0;
        foreach (var upgrade in upgrades)
        {
            _totalWeight += upgrade.weight;
        }
    }

   
    public UpgradesSO GetRandomUpgrade()
    {
       
        if (upgrades == null || upgrades.Count == 0)
        {
            
            Debug.LogError("Brak zdefiniowanych ulepsze�!");
            return null;
        }

        // Algorytm losowania wa�onego (Weighted Random Choice)
        int randomValue = UnityEngine.Random.Range(0, _totalWeight);
        int currentSum = 0;

      
        if (_totalWeight == 0)
        {
            Debug.LogError("Define upgrades weights! Its equal to 0 now.");
        }
        foreach (var upgrade in upgrades)
        {
            currentSum += upgrade.weight;
            if (randomValue < currentSum)
            {
                return upgrade;
            }
        }

        // Fallback (nie powinien wyst�pi�, je�li matematyka jest poprawna
        return null;
    }

    // Metoda pomocnicza, je�li potrzebujesz "zwyk�ego" losowania bez wag
    public UpgradesSO GetUniformRandomUpgrade()
    {
        if (upgrades.Count == 0) return null;
        return upgrades[UnityEngine.Random.Range(0, upgrades.Count)];
    }




}
