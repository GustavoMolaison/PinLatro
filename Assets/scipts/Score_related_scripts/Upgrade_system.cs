using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using System.Linq;


// 1. Definicja typów ulepszeñ
public enum UpgradeType
{
    Portal,
    Sliding,
    Racer
}

// 2. Klasa lub struktura reprezentuj¹ca pojedyncze ulepszenie.
// [Serializable] pozwala edytowaæ listê w Inspektorze Unity, co jest du¿o wygodniejsze ni¿ kodowanie tego na sztywno.
[Serializable]
public class UpgradeDefinition
{
    public string Name;             // Dla czytelnoœci w debugowaniu
    public UpgradeType Type;
    public int Weight;              // Szansa na wylosowanie (wy¿sza liczba = czêœciej)
    public int Cost;
    // U¿ywamy Action, tak jak chcia³eœ, ale lepiej by³oby to wydzieliæ do oddzielnych klas logiki.
    // [HideInInspector] ukrywa to pole w Unity, bo delegatów nie da siê serializowaæ w edytorze.
    public Action<Ball> Effect;

    public UpgradeDefinition(UpgradeType type, int weight, Action<Ball> effect, int cost, string name = "Upgrade")
    {
        Type = type;
        Weight = weight;
        Effect = effect;
        Name = name;
        Cost = cost;
    }
}

public class Upgrade_system : MonoBehaviour
{
    // Singleton dla ³atwego dostêpu (skoro ju¿ u¿ywasz singletonów w swoim kodzie)
    public static Upgrade_system Instance { get; private set; }

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

        InitializeUpgrades();
    }


        // Lista jest kluczowa - pozwala na indeksowanie i ³atwe losowanie.
    [SerializeField] // Dziêki temu podejrzysz stan listy w edytorze (ale delegatów tam nie ustawisz)
    private List<UpgradeDefinition> upgrades;

    private int _totalWeight;

   

    // Tutaj konfigurujesz swoje ulepszenia.
    // Krytyczna uwaga: Upewnij siê, ¿e Singletony (Portalball, Sliding) ju¿ istniej¹!
    // W przeciwnym razie przenieœ to do Start().
    private void InitializeUpgrades()
    {
        upgrades = new List<UpgradeDefinition>
        {
            // Przyk³ad: Portal jest rzadki (waga 10), Sliding czêsty (waga 50)
            new UpgradeDefinition(UpgradeType.Portal, 10, ball => Portalball.Instance.AddPortal(ball), 50, "Rare Portal"),
            new UpgradeDefinition(UpgradeType.Sliding, 10, ball => Sliding.Instance.Add_Sliding(ball), 25, "Common Sliding"),
            new UpgradeDefinition(UpgradeType.Racer,   10, ball => Racer.Instance.AddRacer(ball),      75,  "Uncommon Racer")
        };

        // Obliczamy sumê wag raz, ¿eby nie robiæ tego przy ka¿dym losowaniu (optymalizacja)
        CalculateWeights();
    }

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

        // Fallback (nie powinien wyst¹piæ, jeœli matematyka jest poprawna)
        return upgrades[0];
    }

    // Metoda pomocnicza, jeœli potrzebujesz "zwyk³ego" losowania bez wag
    public UpgradeDefinition GetUniformRandomUpgrade()
    {
        if (upgrades.Count == 0) return null;
        return upgrades[UnityEngine.Random.Range(0, upgrades.Count)];
    }




}
