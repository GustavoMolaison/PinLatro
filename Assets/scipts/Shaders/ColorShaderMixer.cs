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
// SPRITE MAP VERSION
    //public void PassDataToSG(Sprite upgradeSprite, int upgradeIdx, SpriteRenderer sr)
    //{
    //    // 1. Wysy³asz teksturê (atlas)
    //    sr.material.SetTexture($"_Upgr{upgradeIdx}Tex", upgradeSprite.texture);

    //    // 2. Wyliczasz Tiling i Offset na podstawie danych ze Sprite'a
    //    Vector4 st = new Vector4();

    //    // Tiling (Skala): Rozmiar klatki / Rozmiar ca³ego atlasu
    //    st.x = upgradeSprite.rect.width / upgradeSprite.texture.width;
    //    st.y = upgradeSprite.rect.height / upgradeSprite.texture.height;

    //    // Offset (Pozycja): Lewy dolny róg klatki / Rozmiar ca³ego atlasu
    //    st.z = upgradeSprite.rect.x / upgradeSprite.texture.width;
    //    st.w = upgradeSprite.rect.y / upgradeSprite.texture.height;


    //    // 3. Wysy³asz to do Shadera
    //    sr.material.SetVector($"_Upgr{upgradeIdx}_ST", st);
    //    sr.material.SetFloat($"_Upgr{upgradeIdx}Intensity", 1);

    //    Debug.Log($"st.x{st.x}, st.y{st.y}, st.z{st.z}, st.w{st.w}");
    //}

    public void PassDataToSG(Sprite upgradeSprite_B, Sprite upgradeSprite_S, int upgradeIdx, SpriteRenderer sr)
    {
       
        sr.material.SetTexture($"_TexBase{upgradeIdx}", upgradeSprite_B.texture);
        sr.material.SetTexture($"_TexSymbol{upgradeIdx}", upgradeSprite_S.texture);

        Debug.Log("XD?");
        Debug.Log(upgradeIdx);



        sr.material.SetFloat($"Intensity{upgradeIdx}", 1);

        
    }
}
