using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NowyTyp", menuName = "Pinball/TypUpgrade")]
public class UpgradesSO : ScriptableObject
{
    public GameObject prefab;
    public Material upgradeMaterial;
    public Vector3 Color;
    public int IndexInSpriteMap;
    public Sprite upgradeSprite_B;
    public Sprite upgradeSprite_S;
    public Sprite upgradeSprite;
    public UpgradeType type;
    public int weight;
    public int cost;
    public Action<Ball> Effect;

    

    [SerializeField] public string _displayName; // Tutaj wpisujesz �adn� nazw� w Inspektorze
    public string Name => _displayName;

    public bool ApplyEffect(Ball ballRef)
    {
      if (!ballRef.Upgrades.Contains(this))
      {
        Upgrade upgradeScript;

        GameObject newUpgrade = Instantiate(prefab, ballRef.transform);

        upgradeScript = newUpgrade.GetComponent<Upgrade>();

        upgradeScript.apply(ballRef);

        ballRef.Upgrades.Add(this);

        ballRef.upgradeHolderUI.UpdateUpgradesSprites();

        SpriteRenderer spriteRenderer = ballRef.GetComponent<SpriteRenderer>();
         //spriteRenderer.sprite = upgradeSprite;
        
        

        ColorShaderMixer.Instance.PassDataToSG(upgradeSprite_B, upgradeSprite_S, ballRef.Upgrades.IndexOf(this), ballRef.GetComponent<SpriteRenderer>());

        ballRef.ballStatue.UpdateBallSprite();

        return true;
      }

      else
      {
        return false; 
      }
      
    }
}
