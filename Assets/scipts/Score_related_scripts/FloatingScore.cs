using UnityEngine;
using TMPro;

public class FloatingScore : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1.2f;
    public float fadeSpeed = 1.0f;
    public Vector3 moveDirection = new Vector3(0.5f, 1f, 0f); // Góra + lekki skos

    private TextMeshProUGUI textElement;
    private Color textColor;

    void Awake()
    {
        textElement = GetComponentInChildren<TextMeshProUGUI>();
        if (textElement == null)
        {
            
            return;
        }
        textColor = textElement.color;
    }

    void Update()
    {
        // 1. Ruch: pozycja = pozycja + (kierunek * prêdkoœæ * czas)
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        // 2. Zanikanie (Alpha)
        textColor.a -= fadeSpeed * Time.deltaTime;
        textElement.color = textColor;

        // 3. Samozniszczenie gdy tekst jest niewidoczny
        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}