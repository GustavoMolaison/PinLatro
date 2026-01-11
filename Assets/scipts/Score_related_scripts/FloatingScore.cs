using UnityEngine;
using TMPro;

public class FloatingScore : MonoBehaviour
{
    [Header("Settings")]
    public float moveSpeed = 1.2f;
    public float fadeSpeed = 1.0f;
    public Vector3 moveDirection = new Vector3(0.5f, 1f, 0f); 

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
        
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        textColor.a -= fadeSpeed * Time.deltaTime;
        textElement.color = textColor;

        
        if (textColor.a <= 0)
        {
            Destroy(gameObject);
        }
    }
}