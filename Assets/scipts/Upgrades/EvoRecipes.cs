using UnityEngine;

// Ten atrybut pozwoli Ci tworzy� "pliki" przepis�w w Unity (Prawy przycisk -> Create -> ...)
[CreateAssetMenu(fileName = "NowaEwolucja", menuName = "Pinball/Ewolucja")]
public class EvoRecipes : ScriptableObject
{
    public UpgradesSO ingredientA; 
    public UpgradesSO ingredientB; 

    public UpgradesSO resultUpgrade;  
   
}