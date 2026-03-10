using UnityEngine;

public class ColorShaderMixer : MonoBehaviour
{
    public static ColorShaderMixer Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }


    }
    public void PassDataToSG(Sprite upgradeSprite, int upgradeIdx, SpriteRenderer sr)
    {
        // 1. Wysy³asz teksturê (atlas)
        sr.material.SetTexture($"_Upgr{upgradeIdx}Tex", upgradeSprite.texture);

        // 2. Wyliczasz Tiling i Offset na podstawie danych ze Sprite'a
        Vector4 st = new Vector4();

        // Tiling (Skala): Rozmiar klatki / Rozmiar ca³ego atlasu
        st.x = upgradeSprite.rect.width / upgradeSprite.texture.width;
        st.y = upgradeSprite.rect.height / upgradeSprite.texture.height;

        // Offset (Pozycja): Lewy dolny róg klatki / Rozmiar ca³ego atlasu
        st.z = upgradeSprite.rect.x / upgradeSprite.texture.width;
        st.w = upgradeSprite.rect.y / upgradeSprite.texture.height;

        // 3. Wysy³asz to do Shadera
        sr.material.SetVector($"_Upgr{upgradeIdx}_ST", st);
    }
}
