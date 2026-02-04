using UnityEngine;

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem Instance { get; private set; }
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

    public int currentMoney = 0;
    int allTimemMoney = 0;
    int allTimeSpendedMoney = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addMoney(int amount)
    {
        currentMoney += amount;
        allTimemMoney += amount;

        UIManager.Instance.MoneyUpdateDisplay();

    }

    public void takeMoney(int amount)
    {
        currentMoney -= amount;
        allTimeSpendedMoney += amount;

        UIManager.Instance.MoneyUpdateDisplay();

    }

    public void resetMoney()
    {
        currentMoney = 0;
    }
}
