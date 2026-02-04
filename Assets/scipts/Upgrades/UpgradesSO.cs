using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NowyTyp", menuName = "Pinball/TypUpgrade")]
public class UpgradesSO : ScriptableObject
{
    public GameObject prefab;
    public UpgradeType type;
    public int weight;
    public int cost;
    public Action<Ball> Effect;

    [SerializeField] public string _displayName; // Tutaj wpisujesz ³adn¹ nazwê w Inspektorze
    public string Name => _displayName;

    public void ApplyEffect(Ball ballRef)
    {
        Upgrade upgradeScript;

        GameObject newUpgrade = Instantiate(prefab, ballRef.transform);

        upgradeScript = newUpgrade.GetComponent<Upgrade>();

        upgradeScript.apply(ballRef);

        ballRef.Upgrades.Add(this);
        
    }
}
