using UnityEngine;
using System.Collections.Generic;
public class EvolutionManager : MonoBehaviour

{
    public List<EvoRecipes> recipes;
    public static EvolutionManager Instance { get; private set; }
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
}
    public void GetEvolvedUpgrade(Ball ingredientA, Ball ingredientB)
    {
       
        foreach (var upgradeA in ingredientA.Upgrades)
        {
            foreach (var upgradeB in ingredientB.Upgrades)
            {
                foreach (var recipe in recipes)
                {
                    if ((recipe.ingredientA == upgradeA && recipe.ingredientB == upgradeB) ||
                        (recipe.ingredientA == upgradeB && recipe.ingredientB == upgradeA))
                    {
                        Debug.Log($"Evolved Upgrade Found: {recipe.resultUpgrade.Name}");
                        // Tutaj mo�esz doda� logik� do zastosowania nowego ulepszenia
                        return;
                    }
                }
           
               
            }
        }
        
    }
}
