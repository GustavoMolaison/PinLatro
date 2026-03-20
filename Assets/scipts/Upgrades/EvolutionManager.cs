using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
public class EvolutionManager : MonoBehaviour

{
    
    public List<EvoRecipes> recipes;
    public static EvolutionManager Instance { get; private set; }

    private Dictionary<(int, int), UpgradesSO> recipeLookup = new Dictionary<(int, int), UpgradesSO>();
    private void Awake()
{
    
    
    if (Instance == null)
    {
        Instance = this;
        // Opcjonalnie: DontDestroyOnLoad(gameObject); // Jeśli ma żyć między scenami
    }
    else
    {
        Destroy(gameObject); // Zapobiega duplikatom managera
    }

    InitializeRecipes();
}
    
    private void InitializeRecipes()
    {
        foreach (var recipe in recipes)
        {
            if (recipe.ingredientA == null || recipe.ingredientB == null) continue;

            // Sortujemy ID, żeby (A, B) zawsze dawało ten sam klucz co (B, A)
            int id1 = recipe.ingredientA.GetInstanceID();
            int id2 = recipe.ingredientB.GetInstanceID();

            var key = id1 < id2 ? (id1, id2) : (id2, id1);

            if (!recipeLookup.ContainsKey(key))
            {
                recipeLookup.Add(key, recipe.resultUpgrade);
            }
            else
            {
                Debug.LogWarning($"Dubel recepty dla: {recipe.ingredientA.name} + {recipe.ingredientB.name}");
            }
        }
    }

    public UpgradesSO GetEvolution(UpgradesSO a, UpgradesSO b)
    {
        int id1 = a.GetInstanceID();
        int id2 = b.GetInstanceID();
        var key = id1 < id2 ? (id1, id2) : (id2, id1);

        if (recipeLookup.TryGetValue(key, out var result))
        {
            return result;
        }

        return null;
    }
    public void GetEvolvedUpgrade(Ball ingredientA, Ball ingredientB)
    {
        List<UpgradesSO> foundEvos = new List<UpgradesSO>();
        List<UpgradesSO> leftUpgradesA = new List<UpgradesSO>(ingredientA.Upgrades);
        List<UpgradesSO> leftUpgradesB = new List<UpgradesSO>(ingredientB.Upgrades);

        foreach (var upgradeA in ingredientA.Upgrades)
        {
            foreach (var upgradeB in ingredientB.Upgrades)
            {
                UpgradesSO Evo = GetEvolution(upgradeA, upgradeB);
                if (Evo != null && !foundEvos.Contains(Evo)) // Sprawdzamy, czy już nie znaleźliśmy tej ewolucji
                {
                    foundEvos.Add(Evo);
                    leftUpgradesA.Remove(upgradeA);
                    leftUpgradesB.Remove(upgradeB);
                    Debug.Log($"Znaleziono ewolucję: {upgradeA.name} + {upgradeB.name} = {Evo.name}");
                    // Tutaj moesz doda logik do zastosowania nowego ulepszenia
                    
                }
            }
        }
        if (foundEvos.Count > 0)
        {
            // Debug.Log($"Znaleziono {foundEvos.Count} ewolucji dla tych kul!");
            List<UpgradesSO> upgradesForEvoBall = new List<UpgradesSO>();

            foreach (UpgradesSO evo in foundEvos)
            {
                upgradesForEvoBall.Add(evo);
            }
            
            int upgradeSlots;
            if (ingredientA.upgradeSlots > ingredientB.upgradeSlots)
            {
                upgradeSlots = ingredientA.upgradeSlots;
            }
            else
            {
                upgradeSlots = ingredientB.upgradeSlots;
            }


            for (int i = 0; i < upgradeSlots - foundEvos.Count; i++)
            {
                if (leftUpgradesA.Count > i && leftUpgradesA[i] != null)
                {
                    upgradesForEvoBall.Add(leftUpgradesA[i]);
                } 
                if (leftUpgradesB.Count > i && leftUpgradesB[i] != null)
                {
                    upgradesForEvoBall.Add(leftUpgradesB[i]);
                } 
                
                if  (upgradesForEvoBall.Count >= upgradeSlots) break;
            }

            ingredientA.DestroyBallCleanUp();
            ingredientB.DestroyBallCleanUp();
           
            PinBallsManager.Instance.AddNewBallThroughEvolution(upgradesForEvoBall);
        }
        else
        {
            Debug.Log("Brak ewolucji dla tych ulepszeń.");
        }
      
           
        
               
    }

 }
        

