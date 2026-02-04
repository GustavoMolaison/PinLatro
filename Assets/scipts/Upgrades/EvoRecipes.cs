using UnityEngine;

// Ten atrybut pozwoli Ci tworzyæ "pliki" przepisów w Unity (Prawy przycisk -> Create -> ...)
[CreateAssetMenu(fileName = "NowaEwolucja", menuName = "Pinball/Ewolucja")]
public class EvolutionRecipe : ScriptableObject
{
    public UpgradesSO ingredientA; // Przeci¹gasz plik np. "Fire"
    public UpgradesSO ingredientB; // Przeci¹gasz plik np. "Water"

    public UpgradesSO resultType;  // Przeci¹gasz plik np. "Steam"
    public GameObject resultPrefab;   // Fizyczny obiekt pary
}