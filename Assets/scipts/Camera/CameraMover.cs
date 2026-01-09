using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public Transform shopPosition;   // Pusty obiekt ustawiony tam, gdzie ma byæ kamera w sklepie
    public Transform gamePosition;   // Pusty obiekt ustawiony tam, gdzie jest kamera gry
    public float speed = 5f;

    private Vector3 targetPosition;

    public static CameraMover Instance { get; private set; }

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

    void Start()
    {
        // Na start kamera jest na pozycji gry
        targetPosition = transform.position;
    }

    void Update()
    {
        // To jest ta magia - p³ynne d¹¿enie do celu
        // Lerp sprawia, ¿e kamera zwalnia pod koniec ruchu (³adny efekt)
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);

        // Wa¿ne! Upewnij siê, ¿e Z siê nie zmienia (w 2D to zazwyczaj -10)
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }

    // Podepnij to pod przycisk "Sklep"
    public void GoToShop()
    {
        Game_manager.Instance.inShop = true;
        Game_manager.Instance.inTable = false;
        targetPosition = shopPosition.position;
    }

    // Podepnij to pod przycisk "Powrót"
    public void GoToGame()
    {
        Game_manager.Instance.inShop = false;
        Game_manager.Instance.inTable = true;
        targetPosition = gamePosition.position;
    }
}